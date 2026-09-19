using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Travel.Web.Services.ReservationService;
using Travel.Web.Services.TourService;
using Travel.Web.Services.UserServices;

namespace Travel.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ReportController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly ITourService _tourService;
        private readonly IUserService _userService;


        public ReportController(
            IReservationService reservationService,
            ITourService tourService,
            IUserService userService)
        {
            _reservationService = reservationService;
            _tourService = tourService;
            _userService = userService;
        }


        // EXCEL RAPORU
        public async Task<IActionResult> ExportExcel(string tourId)
        {
            var reservations =
                await _reservationService.GetAllAsync();

            var tours =
                await _tourService.GetAllAsync();

            var users =
                await _userService.GetAllAsync();


            var tour = tours.FirstOrDefault(x =>
                x.Id == tourId);


            if (tour == null)
            {
                return NotFound();
            }


            var tourReservations = reservations
                .Where(x => x.TourId == tourId)
                .ToList();


            using var workbook = new XLWorkbook();

            var worksheet =
                workbook.Worksheets.Add("Tur Katılımcıları");


            worksheet.Cell(1, 1).Value = "Ad Soyad";
            worksheet.Cell(1, 2).Value = "E-posta";
            worksheet.Cell(1, 3).Value = "Telefon";
            worksheet.Cell(1, 4).Value = "Tur";
            worksheet.Cell(1, 5).Value = "Tur Tarihi";
            worksheet.Cell(1, 6).Value = "Yetişkin";
            worksheet.Cell(1, 7).Value = "Çocuk";
            worksheet.Cell(1, 8).Value = "Toplam Kişi";
            worksheet.Cell(1, 9).Value = "Rezervasyon Tarihi";
            worksheet.Cell(1, 10).Value = "Toplam Ücret";
            worksheet.Cell(1, 11).Value = "Durum";


            var row = 2;


            foreach (var reservation in tourReservations)
            {
                var user = users.FirstOrDefault(x =>
                    x.Id == reservation.UserId);


                worksheet.Cell(row, 1).Value =
                    user?.NameSurname ?? "Kullanıcı Bulunamadı";


                worksheet.Cell(row, 2).Value =
                    user?.Email ?? "-";


                var phone = user?.Phone;

                worksheet.Cell(row, 3).Value =
                    string.IsNullOrWhiteSpace(phone)
                        ? "-"
                        : phone;


                worksheet.Cell(row, 4).Value =
                    tour.TourName;


                worksheet.Cell(row, 5).Value =
                    reservation.SelectedTourDate
                        .ToString("dd.MM.yyyy");


                worksheet.Cell(row, 6).Value =
                    reservation.AdultCount;


                worksheet.Cell(row, 7).Value =
                    reservation.ChildCount;


                worksheet.Cell(row, 8).Value =
                    reservation.AdultCount +
                    reservation.ChildCount;


                worksheet.Cell(row, 9).Value =
                    reservation.ReservationDate
                        .ToString("dd.MM.yyyy");


                worksheet.Cell(row, 10).Value =
                    reservation.TotalPrice;


                worksheet.Cell(row, 11).Value =
                    reservation.Status;


                row++;
            }


            worksheet.Range(1, 1, 1, 11)
                .Style.Font.Bold = true;


            worksheet.Columns()
                .AdjustToContents();


            using var stream = new MemoryStream();

            workbook.SaveAs(stream);


            var fileName =
                $"{tour.TourName}-Katilimci-Raporu.xlsx";


            return File(
                stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileName
            );
        }


        // PDF RAPORU
        public async Task<IActionResult> ExportPdf(string tourId)
        {
            var reservations =
                await _reservationService.GetAllAsync();

            var tours =
                await _tourService.GetAllAsync();

            var users =
                await _userService.GetAllAsync();


            var tour = tours.FirstOrDefault(x =>
                x.Id == tourId);


            if (tour == null)
            {
                return NotFound();
            }


            var tourReservations = reservations
                .Where(x => x.TourId == tourId)
                .ToList();


            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4.Landscape());

                    page.Margin(20);

                    page.DefaultTextStyle(x =>
                        x.FontSize(7));


                    // BAŞLIK
                    page.Header()
                        .Text($"Tur Katılımcı Raporu - {tour.TourName}")
                        .SemiBold()
                        .FontSize(16);


                    // TABLO
                    page.Content()
                        .PaddingVertical(10)
                        .Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(1.5f);
                                columns.RelativeColumn(1.8f);
                                columns.RelativeColumn(1.2f);
                                columns.RelativeColumn(1.5f);
                                columns.RelativeColumn(1.1f);
                                columns.RelativeColumn(0.7f);
                                columns.RelativeColumn(0.7f);
                                columns.RelativeColumn(0.8f);
                                columns.RelativeColumn(1.2f);
                                columns.RelativeColumn(1.1f);
                                columns.RelativeColumn(0.9f);
                            });


                            // TABLO BAŞLIKLARI
                            table.Header(header =>
                            {
                                header.Cell()
                                    .Background(Colors.Grey.Lighten2)
                                    .Padding(4)
                                    .Text("Ad Soyad")
                                    .SemiBold();

                                header.Cell()
                                    .Background(Colors.Grey.Lighten2)
                                    .Padding(4)
                                    .Text("E-posta")
                                    .SemiBold();

                                header.Cell()
                                    .Background(Colors.Grey.Lighten2)
                                    .Padding(4)
                                    .Text("Telefon")
                                    .SemiBold();

                                header.Cell()
                                    .Background(Colors.Grey.Lighten2)
                                    .Padding(4)
                                    .Text("Tur")
                                    .SemiBold();

                                header.Cell()
                                    .Background(Colors.Grey.Lighten2)
                                    .Padding(4)
                                    .Text("Tur Tarihi")
                                    .SemiBold();

                                header.Cell()
                                    .Background(Colors.Grey.Lighten2)
                                    .Padding(4)
                                    .Text("Yetişkin")
                                    .SemiBold();

                                header.Cell()
                                    .Background(Colors.Grey.Lighten2)
                                    .Padding(4)
                                    .Text("Çocuk")
                                    .SemiBold();

                                header.Cell()
                                    .Background(Colors.Grey.Lighten2)
                                    .Padding(4)
                                    .Text("Toplam")
                                    .SemiBold();

                                header.Cell()
                                    .Background(Colors.Grey.Lighten2)
                                    .Padding(4)
                                    .Text("Rezervasyon")
                                    .SemiBold();

                                header.Cell()
                                    .Background(Colors.Grey.Lighten2)
                                    .Padding(4)
                                    .Text("Ücret")
                                    .SemiBold();

                                header.Cell()
                                    .Background(Colors.Grey.Lighten2)
                                    .Padding(4)
                                    .Text("Durum")
                                    .SemiBold();
                            });


                            // VERİLER
                            foreach (var reservation in tourReservations)
                            {
                                var user = users.FirstOrDefault(x =>
                                    x.Id == reservation.UserId);


                                var phone =
                                    string.IsNullOrWhiteSpace(user?.Phone)
                                        ? "-"
                                        : user.Phone;


                                table.Cell()
                                    .BorderBottom(1)
                                    .BorderColor(Colors.Grey.Lighten2)
                                    .Padding(4)
                                    .Text(
                                        user?.NameSurname
                                        ?? "Kullanıcı Bulunamadı"
                                    );


                                table.Cell()
                                    .BorderBottom(1)
                                    .BorderColor(Colors.Grey.Lighten2)
                                    .Padding(4)
                                    .Text(
                                        user?.Email ?? "-"
                                    );


                                table.Cell()
                                    .BorderBottom(1)
                                    .BorderColor(Colors.Grey.Lighten2)
                                    .Padding(4)
                                    .Text(phone);


                                table.Cell()
                                    .BorderBottom(1)
                                    .BorderColor(Colors.Grey.Lighten2)
                                    .Padding(4)
                                    .Text(tour.TourName);


                                table.Cell()
                                    .BorderBottom(1)
                                    .BorderColor(Colors.Grey.Lighten2)
                                    .Padding(4)
                                    .Text(
                                        reservation.SelectedTourDate
                                            .ToString("dd.MM.yyyy")
                                    );


                                table.Cell()
                                    .BorderBottom(1)
                                    .BorderColor(Colors.Grey.Lighten2)
                                    .Padding(4)
                                    .Text(
                                        reservation.AdultCount.ToString()
                                    );


                                table.Cell()
                                    .BorderBottom(1)
                                    .BorderColor(Colors.Grey.Lighten2)
                                    .Padding(4)
                                    .Text(
                                        reservation.ChildCount.ToString()
                                    );


                                table.Cell()
                                    .BorderBottom(1)
                                    .BorderColor(Colors.Grey.Lighten2)
                                    .Padding(4)
                                    .Text(
                                        (
                                            reservation.AdultCount +
                                            reservation.ChildCount
                                        ).ToString()
                                    );


                                table.Cell()
                                    .BorderBottom(1)
                                    .BorderColor(Colors.Grey.Lighten2)
                                    .Padding(4)
                                    .Text(
                                        reservation.ReservationDate
                                            .ToString("dd.MM.yyyy")
                                    );


                                table.Cell()
                                    .BorderBottom(1)
                                    .BorderColor(Colors.Grey.Lighten2)
                                    .Padding(4)
                                    .Text(
                                        $"{reservation.TotalPrice:N0} TL"
                                    );


                                table.Cell()
                                    .BorderBottom(1)
                                    .BorderColor(Colors.Grey.Lighten2)
                                    .Padding(4)
                                    .Text(
                                        reservation.Status
                                    );
                            }
                        });


                    page.Footer()
                        .AlignCenter()
                        .Text(
                            $"Travelio - {DateTime.Now:dd.MM.yyyy}"
                        );
                });
            });


            var stream = new MemoryStream();

            document.GeneratePdf(stream);

            stream.Position = 0;


            var fileName =
                $"{tour.TourName}-Katilimci-Raporu.pdf";


            return File(
                stream,
                "application/pdf",
                fileName
            );
        }
    }
}