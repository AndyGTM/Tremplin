using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tremplin.Data;
using Tremplin.IRepositories.IConsultation;
using Tremplin.IRepositories.IPatient;
using Tremplin.IServices.IConsultation;
using Tremplin.IServices.IPatient;
using Tremplin.IServices.IUser;
using Tremplin.Repositories;
using Tremplin.Services;
using Tremplin.Store;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddIdentity<User, UserRole>().AddDefaultTokenProviders();

// Consultation repository
builder.Services.AddScoped(typeof(IConsultationRepository<Consultation>), typeof(ConsultationRepository<Consultation>));

// Patient repository
builder.Services.AddScoped(typeof(IPatientRepository<Patient>), typeof(PatientRepository<Patient>));

// Consultation service
builder.Services.AddScoped(typeof(IConsultationService), typeof(ConsultationService));

// Patient service
builder.Services.AddScoped(typeof(IPatientService), typeof(PatientService));

// User service
builder.Services.AddScoped(typeof(IUserService), typeof(UserService));

builder.Services.AddTransient<IUserStore<User>, UserStore>();
builder.Services.AddTransient<IRoleStore<UserRole>, RoleStore>();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.

#if DEBUG
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 1;
    options.Password.RequiredUniqueChars = 0;

#else
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

#endif

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;

#if DEBUG
    options.ExpireTimeSpan = TimeSpan.FromMinutes(1440);

#else
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

#endif

    options.LoginPath = "/Users/Login";
    options.AccessDeniedPath = "/Users/AccessDenied";
    options.SlidingExpiration = true;
});

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();