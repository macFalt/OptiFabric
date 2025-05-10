using OptiFabricMVC.Application.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OptiFabric.Data;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using OptiFabricMVC.Application.Interfaces;
using OptiFabricMVC.Application.Services;
using OptiFabricMVC.Application.Validators.ProductV;
using OptiFabricMVC.Application.ViewModels.ProductsVM;
using OptiFabricMVC.Domain.Interfaces;
using OptiFabricMVC.Domain.Model;
using OptiFabricMVC.Infrastructure;
using OptiFabricMVC.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<Context>();

builder.Services.AddAutoMapper(typeof(MappingProfile));


builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddTransient<IEmployeeService, EmployeeService>();
builder.Services.AddTransient<IShiftService, ShiftService>();
builder.Services.AddTransient<IShiftRepository, ShiftRepository>();
builder.Services.AddTransient<IMachinesRepository, MachineRepository>();
builder.Services.AddTransient<IMachineService, MachineService>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IJobRepository, JobRepository>();
builder.Services.AddTransient<IJobService, JobService>();
builder.Services.AddTransient<IOperationService, OperationService>();
builder.Services.AddTransient<IOperationRepository, OperationRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
builder.Services.AddTransient<IJobEmployeeRepository, JobEmployeeRepository>();
builder.Services.AddTransient<IJobEmployeeService, JobEmployeeService>();

// FluentValidationMvcExtensions.AddFluentValidation(builder.Services.AddControllersWithViews(), fv =>
//     {
//         fv.DisableDataAnnotationsValidation = true; 
//     });builder.Services.AddRazorPages();
//
// builder.Services.AddTransient<IValidator<AddNewProductVM>,AddNewProductValidation>();


builder.Services.AddControllersWithViews()
    .AddFluentValidation(fv =>
    {
        fv.DisableDataAnnotationsValidation = true;
        fv.RegisterValidatorsFromAssemblyContaining<AddNewProductValidator>();
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
    options.AddPolicy("Manager", policy => policy.RequireRole("Manager"));
    options.AddPolicy("Employee", policy => policy.RequireRole("Employee"));
    options.AddPolicy("CEO", policy => policy.RequireRole("CEO"));
    options.AddPolicy("ProductionMaster", policy => policy.RequireRole("ProductionMaster"));


});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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


app.MapRazorPages();

app.Run();