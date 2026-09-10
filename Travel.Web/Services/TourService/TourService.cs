using AutoMapper;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Travel.Web.DTOs.TourDtos;
using Travel.Web.Entitites;
using Travel.Web.Entitites.TourDetails;
using Travel.Web.Settings;

namespace Travel.Web.Services.TourService
{
    public class TourService : ITourService
    {
        private readonly IMongoCollection<Tour> _tourCollection;
        private readonly IMapper _mapper;

        public TourService(IDatabaseSettings databaseSettings, IMapper mapper)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _tourCollection = database.GetCollection<Tour>(databaseSettings.TourCollectionName);
            _mapper = mapper;

        }
        public async Task AddTourDateAsync(string tourId, CreateTourDateDto createTourDateDto)
        {
            var tour = await _tourCollection.Find(x => x.Id == tourId).FirstOrDefaultAsync();

            if (tour == null)
            {
                throw new Exception("Tur bulunamadı!");
            }

            var tourDate = _mapper.Map<TourDate>(createTourDateDto);

            tour.TourDates.Add(tourDate);

            await _tourCollection.FindOneAndReplaceAsync(x => x.Id == tour.Id, tour);
        }

        public async Task AddTourProgramAsync(string tourId, CreateTourProgramDto createTourProgramDto)
        {
            var tour = await _tourCollection.Find(x => x.Id == tourId).FirstOrDefaultAsync();

            if (tour == null)
            {
                throw new Exception("Tur bulunamadı!");
            }

            var tourProgram = _mapper.Map<TourProgram>(createTourProgramDto);

            tour.TourPrograms.Add(tourProgram);

            await _tourCollection.FindOneAndReplaceAsync(x => x.Id == tourId, tour);
        }

        public async Task ChangeStatusAsync(string id)
        {
            var tour = await _tourCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

            if (tour == null)
            {
                throw new Exception("Tur bulunamadı!");
            }

            tour.IsActive = !tour.IsActive;
            await _tourCollection.FindOneAndReplaceAsync(x => x.Id == tour.Id, tour);
        }

        public async Task CreateAsync(CreateTourDto createTourDto)
        {
            var tour = _mapper.Map<Tour>(createTourDto);
            await _tourCollection.InsertOneAsync(tour);
        }
        public async Task DecreaseCapacityAsync(string tourId, string tourDateId, int personCount)
        {
            var tour = await _tourCollection
                .Find(x => x.Id == tourId)
                .FirstOrDefaultAsync();

            if (tour == null)
            {
                throw new Exception("Tur bulunamadı!");
            }

            var tourDate = tour.TourDates
                .FirstOrDefault(x => x.Id == tourDateId);

            if (tourDate == null)
            {
                throw new Exception("Tur tarihi bulunamadı!");
            }

            if (tourDate.Capacity < personCount)
            {
                throw new Exception("Kapasite yetersiz!");
            }

            tourDate.Capacity -= personCount;

            await _tourCollection.FindOneAndReplaceAsync(
                x => x.Id == tourId,
                tour);
        }

        public async Task DeleteAsync(string id)
        {
            await _tourCollection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task DeleteTourDateAsync(string tourId, string tourDateId)
        {
            var tour = await _tourCollection.Find(x => x.Id == tourId).FirstOrDefaultAsync();

            if (tour == null)
            {
                throw new Exception("Tur bulunamadı!");
            }

            var tourDate = tour.TourDates.FirstOrDefault(x => x.Id == tourDateId);

            if (tourDate == null)
            {
                throw new Exception("Tur tarihi bulunamadı!");
            }

            tour.TourDates.Remove(tourDate);

            await _tourCollection.FindOneAndReplaceAsync(x => x.Id == tourId, tour);
        }

        public async Task DeleteTourProgramAsync(string tourId, string tourProgramId)
        {
            var tour = await _tourCollection.Find(x => x.Id == tourId).FirstOrDefaultAsync();


            if (tour == null)
            {
                throw new Exception("Tur bulunamadı!");
            }

            var tourProgram = tour.TourPrograms.FirstOrDefault(x => x.Id == tourProgramId);

            if (tourProgram == null)
            {
                throw new Exception("Tur programı bulunamadı!");
            }

            tour.TourPrograms.Remove(tourProgram);

            await _tourCollection.FindOneAndReplaceAsync(x => x.Id == tourId, tour);
        }

        public async Task<List<ResultTourDto>> FilterAsync(string? destinationId, string? categoryId, decimal? minPrice, decimal? maxPrice, DateTime? dateTime)
        {
            var query = _tourCollection.AsQueryable();

            if (!string.IsNullOrEmpty(destinationId))
            {
                query = query.Where(x => x.DestinationId == destinationId);
            }

            if (!string.IsNullOrEmpty(categoryId))
            {
                query = query.Where(x => x.CategoryId == categoryId);
            }

            if (minPrice.HasValue)
            {
                query = query.Where(x => x.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(x => x.Price <= maxPrice.Value);
            }

            if (dateTime.HasValue)
            {
                query = query.Where(x => x.TourDates.Any(y => y.Date == dateTime.Value));
            }

            var tours = await query.ToListAsync();

            return _mapper.Map<List<ResultTourDto>>(tours);
        }

        public async Task<List<ResultTourDto>> GetActiveToursAsync()
        {
            var tour = await _tourCollection.Find(x => x.IsActive == true).ToListAsync();

            return _mapper.Map<List<ResultTourDto>>(tour);
        }

        public async Task<List<ResultTourDto>> GetAllAsync()
        {
            var tour = await _tourCollection.AsQueryable().ToListAsync();
            return _mapper.Map<List<ResultTourDto>>(tour);
        }

        public async Task<ResultTourDto> GetByIdAsync(string id)
        {
            var tour = await _tourCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

            if(tour == null)
            {
                throw new Exception("Tur bulunamadı!");
            }

            return _mapper.Map<ResultTourDto>(tour);
        }

        public async Task UpdateAsync(UpdateTourDto updateTourDto)
        {
            var tour = await _tourCollection
                .Find(x => x.Id == updateTourDto.Id)
                .FirstOrDefaultAsync();

            if (tour == null)
            {
                throw new Exception("Tur bulunamadı!");
            }

            var tourDates = tour.TourDates;
            var tourPrograms = tour.TourPrograms;

            _mapper.Map(updateTourDto, tour);

            tour.TourDates = tourDates;
            tour.TourPrograms = tourPrograms;

            await _tourCollection.FindOneAndReplaceAsync(
                x => x.Id == tour.Id,
                tour
            );
        }

        public async Task UpdateTourDateAsync(string tourId, UpdateTourDateDto updateTourDateDto)
        {
            var tour = await _tourCollection.Find(x => x.Id == tourId).FirstOrDefaultAsync();

            if (tour == null)
            {
                throw new Exception("Tur bulunamadı!");
            }

            var tourDate = tour.TourDates.FirstOrDefault(x => x.Id == updateTourDateDto.Id);

            if (tourDate == null)
            {
                throw new Exception("Tur tarihi bulunamadı!");
            }

            _mapper.Map(updateTourDateDto, tourDate);

            await _tourCollection.FindOneAndReplaceAsync(x => x.Id == tourId, tour);
        }

        // TourService
        public async Task<ResultTourDateDto> GetTourDateByIdAsync(string tourId, string tourDateId)
        {
            var tour = await _tourCollection.Find(x => x.Id == tourId).FirstOrDefaultAsync();

            if (tour == null)
            {
                throw new Exception("Tur bulunamadı!");
            }

            var tourDate = tour.TourDates.FirstOrDefault(x => x.Id == tourDateId);

            if (tourDate == null)
            {
                throw new Exception("Tur tarihi bulunamadı!");
            }

            return _mapper.Map<ResultTourDateDto>(tourDate);
        }

        public async Task UpdateTourProgramAsync(string tourId, UpdateTourProgramDto updateTourProgramDto)
        {
            var tour = await _tourCollection.Find(x => x.Id == tourId).FirstOrDefaultAsync();

            if (tour == null)
            {
                throw new Exception("Tur bulunamadı!");
            }

            var tourProgram = tour.TourPrograms.FirstOrDefault(x => x.Id == updateTourProgramDto.Id);

            if(tourProgram == null)
            {
                throw new Exception("Tur programı bulunamadı!");
            }

            _mapper.Map(updateTourProgramDto, tourProgram);

            await _tourCollection.FindOneAndReplaceAsync(x => x.Id == tourId, tour);


        }

        public async Task<ResultTourProgramDto> GetTourProgramByIdAsync(string tourId, string tourProgramId)
        {
            var tour = await _tourCollection
                .Find(x => x.Id == tourId)
                .FirstOrDefaultAsync();

            if (tour == null)
            {
                throw new Exception("Tur bulunamadı!");
            }

            var tourProgram = tour.TourPrograms
                .FirstOrDefault(x => x.Id == tourProgramId);

            if (tourProgram == null)
            {
                throw new Exception("Tur programı bulunamadı!");
            }

            return _mapper.Map<ResultTourProgramDto>(tourProgram);
        }

    }
}
