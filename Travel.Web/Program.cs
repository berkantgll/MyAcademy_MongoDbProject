using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Reflection;
using Travel.Web.Entitites;
using Travel.Web.Services.BannerServices;
using Travel.Web.Services.CategoryServices;
using Travel.Web.Services.CommentService;
using Travel.Web.Services.DestinationService;
using Travel.Web.Services.DestinationServices;
using Travel.Web.Services.FavoriteServices;
using Travel.Web.Services.QuestionService;
using Travel.Web.Services.ReservationService;
using Travel.Web.Services.RouteServices;
using Travel.Web.Services.TourService;
using Travel.Web.Services.UserServices;
using Travel.Web.Settings;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

QuestPDF.Settings.License = LicenseType.Community;

// ======================================================
// AUTOMAPPER
// ======================================================

builder.Services.AddAutoMapper(
    Assembly.GetExecutingAssembly());


// ======================================================
// FLUENT VALIDATION
// ======================================================

builder.Services
    .AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters()
    .AddValidatorsFromAssembly(
        Assembly.GetExecutingAssembly());


// ======================================================
// DATABASE SETTINGS
// ======================================================

builder.Services.Configure<DatabaseSettings>(
    builder.Configuration
        .GetSection(nameof(DatabaseSettings)));


// ======================================================
// SERVICES
// ======================================================

builder.Services.AddScoped<IBannerService, BannerService>();

builder.Services.AddScoped<IRouteService, RouteService>();

builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<IDestinationService, DestinationService>();

builder.Services.AddScoped<ITourService, TourService>();

builder.Services.AddScoped<IReservationService, ReservationService>();

builder.Services.AddScoped<ICommentService, CommentService>();

builder.Services.AddScoped<IQuestionService, QuestionService>();

builder.Services.AddScoped<IFavoriteService, FavoriteService>();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();


// ======================================================
// AUTHENTICATION
// ======================================================

builder.Services
    .AddAuthentication(
        CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath =
            "/Account/Login";

        options.AccessDeniedPath =
            "/Account/AccessDenied";

        options.ExpireTimeSpan =
            TimeSpan.FromHours(8);

        options.SlidingExpiration =
            true;
    });


builder.Services.AddAuthorization();


// ======================================================
// MONGODB SETTINGS
// ======================================================

builder.Services.AddSingleton<IDatabaseSettings>(sp =>
{
    return sp
        .GetRequiredService<IOptions<DatabaseSettings>>()
        .Value;
});


// ======================================================
// LOCALIZATION
// ======================================================

builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "Resources";
});


builder.Services
    .AddControllersWithViews()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization();


// DESTEKLENEN DİLLER

var supportedCultures =
    new[]
    {
        new CultureInfo("tr-TR"),
        new CultureInfo("en-US")
    };


builder.Services.Configure<RequestLocalizationOptions>(
    options =>
    {
        options.DefaultRequestCulture =
            new RequestCulture("tr-TR");

        options.SupportedCultures =
            supportedCultures;

        options.SupportedUICultures =
            supportedCultures;


        options.RequestCultureProviders =
            new List<IRequestCultureProvider>
            {
                new CookieRequestCultureProvider()
            };
    });


// ======================================================
// APP
// ======================================================

var app = builder.Build();


// ======================================================
// ERROR
// ======================================================

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(
        "/Home/Error");

    app.UseHsts();
}


// ======================================================
// PIPELINE
// ======================================================

app.UseHttpsRedirection();

app.UseStaticFiles();


// LOCALIZATION
// ROUTING'DEN ÖNCE

var localizationOptions =
    app.Services
        .GetRequiredService<
            IOptions<RequestLocalizationOptions>>()
        .Value;


app.UseRequestLocalization(
    localizationOptions);


app.UseRouting();


app.UseAuthentication();

app.UseAuthorization();


// ======================================================
// ROUTES
// ======================================================

app.MapControllerRoute(
    name: "areas",
    pattern:
        "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);


app.MapControllerRoute(
    name: "default",
    pattern:
        "{controller=Home}/{action=Index}/{id?}"
);


app.Run();