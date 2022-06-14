using Microsoft.EntityFrameworkCore;
using RecipeApp;
using RecipeApp.Authorization;
using RecipeApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();


builder.Services.AddDbContext<AppDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddScoped<RecipeService>();
builder.Services.AddScoped<IAuthorizationHandler,IsRecipeOwnerHandler>();

Host.CreateDefaultBuilder(args).ConfigureWebHost(webBuilder =>
{
    webBuilder.UseKestrel();
    webBuilder.UseIIS();
    webBuilder.UseIISIntegration();
});

builder.Services.AddAuthorization(options => options.AddPolicy("CanManageRecipe", policyBuilder => policyBuilder.AddRequirements(new IsRecipeOwnerRequirement())));

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
