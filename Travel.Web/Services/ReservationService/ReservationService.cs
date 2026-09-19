using AutoMapper;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Travel.Web.DTOs.ReservationDtos;
using Travel.Web.Entitites;
using Travel.Web.Settings;

namespace Travel.Web.Services.ReservationService
{
    public class ReservationService : IReservationService
    {
        private readonly IMongoCollection<Reservation>
            _reservationCollection;

        private readonly IMongoCollection<Tour>
            _tourCollection;

        private readonly IMapper _mapper;


        public ReservationService(
            IDatabaseSettings databaseSettings,
            IMapper mapper)
        {
            var client =
                new MongoClient(
                    databaseSettings.ConnectionString);

            var database =
                client.GetDatabase(
                    databaseSettings.DatabaseName);


            _reservationCollection =
                database.GetCollection<Reservation>(
                    databaseSettings
                        .ReservationCollectionName);


            _tourCollection =
                database.GetCollection<Tour>(
                    databaseSettings
                        .TourCollectionName);


            _mapper = mapper;
        }


        // CREATE
        public async Task CreateAsync(
            CreateReservationDto createReservationDto)
        {
            var tour =
                await _tourCollection
                    .Find(x =>
                        x.Id ==
                        createReservationDto.TourId)
                    .FirstOrDefaultAsync();


            if (tour == null)
            {
                throw new Exception(
                    "Tur bulunamadı!");
            }


            var tourDate =
                tour.TourDates
                    .FirstOrDefault(x =>
                        x.Id ==
                        createReservationDto.TourDateId);


            if (tourDate == null)
            {
                throw new Exception(
                    "Seçilen tur tarihi bulunamadı!");
            }


            var personCount =
                createReservationDto.AdultCount +
                createReservationDto.ChildCount;


            if (personCount <= 0)
            {
                throw new Exception(
                    "Kişi sayısı geçersiz!");
            }


            if (tourDate.Capacity < personCount)
            {
                throw new Exception(
                    "Kapasite yetersiz!");
            }


            var reservation =
                _mapper.Map<Reservation>(
                    createReservationDto);


            reservation.TourDateId =
                tourDate.Id;


            reservation.SelectedTourDate =
                tourDate.Date;


            reservation.TotalPrice =
                (reservation.AdultCount * tour.Price)
                +
                (reservation.ChildCount *
                 (tour.Price / 2));


            reservation.ReservationDate =
                DateTime.Now;


            reservation.Status =
                "Bekliyor";


            tourDate.Capacity -=
                personCount;


            await _tourCollection
                .FindOneAndReplaceAsync(
                    x => x.Id == tour.Id,
                    tour);


            await _reservationCollection
                .InsertOneAsync(
                    reservation);
        }


        // DELETE
        public async Task DeleteAsync(
            string id)
        {
            await _reservationCollection
                .DeleteOneAsync(
                    x => x.Id == id);
        }


        // GET ALL
        public async Task<List<ResultReservationDto>>
            GetAllAsync()
        {
            var reservations =
                await _reservationCollection
                    .AsQueryable()
                    .ToListAsync();


            return _mapper
                .Map<List<ResultReservationDto>>(
                    reservations);
        }


        // GET BY ID
        public async Task<ResultReservationDto>
            GetByIdAsync(string id)
        {
            var reservation =
                await _reservationCollection
                    .Find(x => x.Id == id)
                    .FirstOrDefaultAsync();


            if (reservation == null)
            {
                throw new Exception(
                    "Rezervasyon bulunamadı!");
            }


            return _mapper
                .Map<ResultReservationDto>(
                    reservation);
        }


        // UPDATE STATUS
        public async Task UpdateStatusAsync(
            UpdateReservationDto updateReservationDto)
        {
            var reservation =
                await _reservationCollection
                    .Find(x =>
                        x.Id ==
                        updateReservationDto.Id)
                    .FirstOrDefaultAsync();


            if (reservation == null)
            {
                throw new Exception(
                    "Rezervasyon bulunamadı!");
            }


            reservation.Status =
                updateReservationDto.Status;


            await _reservationCollection
                .FindOneAndReplaceAsync(
                    x =>
                        x.Id ==
                        reservation.Id,
                    reservation);
        }


        // TURUN REZERVASYON SAYISI
        public async Task<int>
            GetReservationCountByTourIdAsync(
                string tourId)
        {
            var count =
                await _reservationCollection
                    .CountDocumentsAsync(
                        x => x.TourId == tourId);


            return (int)count;
        }


        // ONAYLA
        public async Task ApproveAsync(
            string id)
        {
            var reservation =
                await _reservationCollection
                    .Find(x => x.Id == id)
                    .FirstOrDefaultAsync();


            if (reservation == null)
            {
                throw new Exception(
                    "Rezervasyon bulunamadı!");
            }


            if (reservation.Status ==
                "İptal Edildi")
            {
                throw new Exception(
                    "İptal edilmiş rezervasyon onaylanamaz!");
            }


            if (reservation.Status ==
                "Onaylandı")
            {
                return;
            }


            var update =
                Builders<Reservation>
                    .Update
                    .Set(
                        x => x.Status,
                        "Onaylandı");


            await _reservationCollection
                .UpdateOneAsync(
                    x => x.Id == id,
                    update);
        }


        // İPTAL ET
        public async Task CancelAsync(
            string id)
        {
            var reservation =
                await _reservationCollection
                    .Find(x => x.Id == id)
                    .FirstOrDefaultAsync();


            if (reservation == null)
            {
                throw new Exception(
                    "Rezervasyon bulunamadı!");
            }


            if (reservation.Status ==
                "İptal Edildi")
            {
                return;
            }


            var tour =
                await _tourCollection
                    .Find(x =>
                        x.Id ==
                        reservation.TourId)
                    .FirstOrDefaultAsync();


            if (tour == null)
            {
                throw new Exception(
                    "Tur bulunamadı!");
            }


            var tourDate =
                tour.TourDates
                    .FirstOrDefault(x =>
                        x.Id ==
                        reservation.TourDateId);


            if (tourDate == null)
            {
                throw new Exception(
                    "Rezervasyona ait tur tarihi bulunamadı!");
            }


            var totalPerson =
                reservation.AdultCount +
                reservation.ChildCount;


            tourDate.Capacity +=
                totalPerson;


            await _tourCollection
                .FindOneAndReplaceAsync(
                    x => x.Id == tour.Id,
                    tour);


            var update =
                Builders<Reservation>
                    .Update
                    .Set(
                        x => x.Status,
                        "İptal Edildi");


            await _reservationCollection
                .UpdateOneAsync(
                    x =>
                        x.Id ==
                        reservation.Id,
                    update);
        }


        // KULLANICININ REZERVASYONLARI
        public async Task<List<ResultReservationDto>>
            GetByUserIdAsync(
                string userId)
        {
            var reservations =
                await _reservationCollection
                    .Find(x =>
                        x.UserId == userId)
                    .SortByDescending(
                        x =>
                            x.ReservationDate)
                    .ToListAsync();


            return _mapper
                .Map<List<ResultReservationDto>>(
                    reservations);
        }


        // =====================================================
        // AGGREGATION
        // EN ÇOK REZERVASYON ALAN 5 TUR
        // =====================================================
        public async Task<List<TourReservationStatisticDto>>
            GetTop5ToursByReservationAsync()
        {
            var statistics =
                await _reservationCollection
                    .Aggregate()

                    .Match(x =>
                        x.Status != "İptal Edildi")

                    .Group(
                        x => x.TourId,
                        group =>
                            new TourReservationStatisticDto
                            {
                                TourId =
                                    group.Key,

                                ReservationCount =
                                    group.Count()
                            })

                    .SortByDescending(x =>
                        x.ReservationCount)

                    .Limit(5)

                    .ToListAsync();


            return statistics;
        }


        // =====================================================
        // AGGREGATION
        // SON 6 AYLIK REZERVASYON SAYILARI
        // =====================================================
        public async Task<List<MonthlyReservationStatisticDto>>
            GetLast6MonthsReservationStatsAsync()
        {
            var startDate =
                new DateTime(
                    DateTime.Now.Year,
                    DateTime.Now.Month,
                    1)
                .AddMonths(-5);


            var statistics =
                await _reservationCollection
                    .Aggregate()

                    .Match(x =>
                        x.ReservationDate >=
                        startDate)

                    .Group(
                        x => new
                        {
                            Year =
                                x.ReservationDate.Year,

                            Month =
                                x.ReservationDate.Month
                        },
                        group =>
                            new MonthlyReservationStatisticDto
                            {
                                Year =
                                    group.Key.Year,

                                Month =
                                    group.Key.Month,

                                ReservationCount =
                                    group.Count()
                            })

                    .SortBy(x =>
                        x.Year)

                    .ThenBy(x =>
                        x.Month)

                    .ToListAsync();


            return statistics;
        }
    }
}