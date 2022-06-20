/*using Avero.Infrastructure.Persistence;*/

using Avero.Application.Interfaces;
using Avero.Core.Entities;
using Avero.Infrastructure.Persistence;
using Avero.Infrastructure.Persistence.DBContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContextPool<ApplicationDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));


builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequiredUniqueChars = 2;

    /*options.SignIn.RequireConfirmedEmail = true;
    options.Tokens.EmailConfirmationTokenProvider = "CustomEmailConfirmation";*/

    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
})
.AddEntityFrameworkStores<ApplicationDBContext>()
.AddDefaultTokenProviders();

/*builder.Services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = "443892072779-3n0ljac2jnar4kmnnlkn74h4ic1tdq54.apps.googleusercontent.com";
                    options.ClientSecret = "7C6TvX2SWEodUuXd3EpsoO1R";
                })
                .AddFacebook(options =>
                {
                    options.AppId = "2316662895109472";
                    options.AppSecret = "e25c1b8d4145034ed426d7a05efe1481";
                });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("DeleteRolePolicy",
        policy => policy.RequireClaim("Delete Role"));

    options.AddPolicy("EditRolePolicy",
        policy => policy.AddRequirements(new ManageAdminRolesAndClaimsRequirement()));

    options.AddPolicy("AdminRolePolicy",
        policy => policy.RequireRole("Admin"));
});*/

var app = builder.Build();

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
