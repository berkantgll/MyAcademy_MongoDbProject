using MongoDB.Bson;

namespace Travel.Web.Entitites.TourDetails
{
    public class TourProgram
    {
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
        public int DayNumber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public string Transport { get; set; }
        public string Meal { get; set; }
    }
}