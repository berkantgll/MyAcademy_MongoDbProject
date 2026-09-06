using MongoDB.Bson;

namespace Travel.Web.Entitites.TourDetails
{
    public class TourDate
    {
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
        public DateTime Date { get; set; }
        public int Capacity { get; set; }

    }
}
