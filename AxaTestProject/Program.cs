using AxaTestProject.Repositories.Classes;
using AxaTestProject.Repositories.DBContext;
using AxaTestProject.Repositories.Interfaces;
using AxaTestProject.Services.Classes;
using AxaTestProject.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


ConfigServices(builder);

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT:Secret").Value!)),
        ValidIssuer = builder.Configuration.GetSection("JWT:ValidIssuer").Value,
        ValidAudience = builder.Configuration.GetSection("JWT:ValidAudience").Value,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true

    };
});

// Add services to the container.
builder.Services.AddControllersWithViews();

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

void ConfigServices(WebApplicationBuilder builder)
{
    builder.Services.AddScoped<ICreateNewSoatService, CreateNewSoatService>();
    builder.Services.AddScoped<ICreateNewSoatRepository, CreateNewSoatRepository>();
    builder.Services.AddScoped<IPasswordHasher<IdentityUser>, CreateHashPasswordService<IdentityUser>>();
    builder.Services.AddScoped<ILoginService, LoginService>();
    builder.Services.AddScoped<ILoginRepository, LoginRepository>();
    builder.Services.AddScoped<ICitiesRepository, CitiesRepository>();
    builder.Services.AddDbContext<MyDBContext>();
}