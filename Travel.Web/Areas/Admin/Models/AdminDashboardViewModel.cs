namespace Travel.Web.Areas.Admin.Models
{
    public class AdminDashboardViewModel
    {
        public int TotalTourCount { get; set; }

        public int ActiveTourCount { get; set; }

        public int PassiveTourCount { get; set; }

        public int TotalReservationCount { get; set; }

        public int PendingReservationCount { get; set; }

        public int ThisMonthReservationCount { get; set; }

        public int TotalUserCount { get; set; }

        public int PendingQuestionCount { get; set; }

        public decimal TotalRevenue { get; set; }

        public int TotalCapacity { get; set; }

        public List<string> ChartLabels { get; set; } = new();

        public List<int> ChartReservationCounts { get; set; } = new();

        public List<AdminDashboardPopularTourViewModel> PopularTours { get; set; } = new();

        public List<AdminDashboardReservationViewModel> RecentReservations { get; set; } = new();
    }


    public class AdminDashboardPopularTourViewModel
    {
        public string TourId { get; set; } = string.Empty;

        public string TourName { get; set; } = string.Empty;

        public string CoverImageUrl { get; set; } = string.Empty;

        public int ReservationCount { get; set; }
    }


    public class AdminDashboardReservationViewModel
    {
        public string Id { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public string TourName { get; set; } = string.Empty;

        public DateTime SelectedTourDate { get; set; }

        public DateTime ReservationDate { get; set; }

        public int AdultCount { get; set; }

        public int ChildCount { get; set; }

        public decimal TotalPrice { get; set; }

        public string Status { get; set; } = string.Empty;
    }
}