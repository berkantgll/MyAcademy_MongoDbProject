using AutoMapper;
using MongoDB.Driver.Linq;
using MongoDB.Driver;
using Travel.Web.DTOs.ReservationDtos;
using Travel.Web.Entitites;
using Travel.Web.Settings;

namespace Travel.Web.Services.ReservationService
{
    public class ReservationService : IReservationService
    {
        private readonly IMongoCollection<Reservation> _reservationCollection;
        private readonly IMongoCollection<Tour> _tourCollection;
        private readonly IMapper _mapper;

        public ReservationService(IDatabaseSettings databaseSettings, IMapper mapper)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);

            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _reservationCollection = database.GetCollection<Reservation>(databaseSettings.ReservationCollectionName);

            _tourCollection = database.GetCollection<Tour>(databaseSettings.TourCollectionName);

            _mapper = mapper;
        }

        public async Task CreateAsync(CreateReservationDto createReservationDto)
        {
            var reservation = _mapper.Map<Reservation>(createReservationDto);

            var tour = await _tourCollection.Find(x => x.Id == createReservationDto.TourId).FirstOrDefaultAsync();

            if (tour == null)
            {
                throw new Exception("Tur Bulunamadı!");
            }

            var tourDate = tour.TourDates.FirstOrDefault(x => x.Date == createReservationDto.SelectedTourDate);

            if (tourDate == null)
            {
                throw new Exception("Seçilen tur tarihi bulunamadı!");
            }

            var personCount = reservation.AdultCount + reservation.ChildCount;

            if (tourDate.Capacity < personCount)
            {
                throw new Exception("Kapasite yetersiz!");
            }

            reservation.TotalPrice = (reservation.AdultCount * tour.Price) + (reservation.ChildCount * (tour.Price / 2));

            reservation.ReservationDate = DateTime.Now;

            reservation.Status = "Bekliyor";

            //Reservation.UserId bunu Identity gelince yapıcaz.

            tourDate.Capacity -= personCount;

            await _tourCollection.FindOneAndReplaceAsync(x => x.Id == tour.Id, tour);

            await _reservationCollection.InsertOneAsync(reservation);
        }

        public async Task DeleteAsync(string id)
        {
            await _reservationCollection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<List<ResultReservationDto>> GetAllAsync()
        {
            var reservation = await _reservationCollection.AsQueryable().ToListAsync();
            return _mapper.Map<List<ResultReservationDto>>(reservation);
        }

        public async Task<ResultReservationDto> GetByIdAsync(string id)
        {
            var reservation = await _reservationCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

            if (reservation == null)
            {
                throw new Exception("Rezervasyon bulunamadı!");
            }

            return _mapper.Map<ResultReservationDto>(reservation);
        }

        public async Task UpdateStatusAsync(UpdateReservationDto updateReservationDto)
        {
            var reservation = await _reservationCollection.Find(x => x.Id == updateReservationDto.Id).FirstOrDefaultAsync();

            if (reservation == null)
            {
                throw new Exception("Rezervasyon bulunamadı!");
            }

            reservation.Status = updateReservationDto.Status;

            await _reservationCollection.FindOneAndReplaceAsync(x => x.Id == reservation.Id, reservation);

        }

        public async Task<int> GetReservationCountByTourIdAsync(string tourId)
        {
            var count = await _reservationCollection
                .CountDocumentsAsync(x => x.TourId == tourId);

            return (int)count;
        }

        public async Task ApproveAsync(string id)
        {
            var reservation = await _reservationCollection
                .Find(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (reservation == null)
            {
                throw new Exception("Rezervasyon bulunamadı!");
            }

            if (reservation.Status == "İptal Edildi")
            {
                throw new Exception("İptal edilmiş rezervasyon onaylanamaz!");
            }

            if (reservation.Status == "Onaylandı")
            {
                return;
            }

            var update = Builders<Reservation>.Update
                .Set(x => x.Status, "Onaylandı");

            await _reservationCollection.UpdateOneAsync(
                x => x.Id == id,
                update
            );
        }

        public async Task CancelAsync(string id)
        {
            var reservation = await _reservationCollection
                .Find(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (reservation == null)
            {
                throw new Exception("Rezervasyon bulunamadı!");
            }

            // Aynı rezervasyon ikinci kez iptal edilirse
            // kontenjan ikinci kez artırılmasın.
            if (reservation.Status == "İptal Edildi")
            {
                return;
            }

            var tour = await _tourCollection
                .Find(x => x.Id == reservation.TourId)
                .FirstOrDefaultAsync();

            if (tour == null)
            {
                throw new Exception("Tur bulunamadı!");
            }

            var tourDate = tour.TourDates
                .FirstOrDefault(x =>
                    x.Date.Date == reservation.SelectedTourDate.Date);

            if (tourDate == null)
            {
                throw new Exception("Rezervasyona ait tur tarihi bulunamadı!");
            }

            var totalPerson =
                reservation.AdultCount +
                reservation.ChildCount;

            // Rezervasyon yapılırken düşürdüğümüz kapasiteyi geri veriyoruz.
            tourDate.Capacity += totalPerson;

            await _tourCollection.FindOneAndReplaceAsync(
                x => x.Id == tour.Id,
                tour
            );

            var update = Builders<Reservation>.Update
                .Set(x => x.Status, "İptal Edildi");

            await _reservationCollection.UpdateOneAsync(
                x => x.Id == reservation.Id,
                update
            );
        }
    }
}

