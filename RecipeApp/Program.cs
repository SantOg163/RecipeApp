using Microsoft.EntityFrameworkCore;
using RecipeApp;
using RecipeApp.Authorization;
using RecipeApp.Data;
using Microsoft.AspNetCore.Authorization;
using Serilog;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<AppDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddScoped<RecipeService>();
builder.Services.AddScoped<IAuthorizationHandler,IsRecipeOwnerHandler>();

builder.Host.UseSerilog((hostContext, services, configuration) =>
{
    configuration.WriteTo.File("Log");
    configuration.WriteTo.Console();
});
builder.Host.UseContentRoot(Directory.GetCurrentDirectory())
    .ConfigureAppConfiguration(config => config.AddJsonFile("appsettings.json")).ConfigureLogging((ctx, builder) => 
    {
        builder.AddConfiguration(ctx.Configuration.GetSection("Logging"));
        builder.AddConsole(); 
    });

Host.CreateDefaultBuilder(args).ConfigureLogging(builder => builder.AddSeq()).ConfigureWebHost(webBuilder =>
{
    webBuilder.UseStartup<StartupBase>();
});

Host.CreateDefaultBuilder(args).ConfigureWebHost(webBuilder =>
{
    webBuilder.UseKestrel();
    webBuilder.UseIIS();
    webBuilder.UseIISIntegration();
});

builder.Services.AddAuthorization(options => options.AddPolicy("CanManageRecipe", policyBuilder => policyBuilder.AddRequirements(new IsRecipeOwnerRequirement())));

builder.Services.AddHsts(options=>options.MaxAge=TimeSpan.FromHours(1));

builder.Services.AddControllers().AddFluentValidation();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
