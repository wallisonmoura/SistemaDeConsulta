using FluentValidation;
using SistemaDeConsulta.Data.Context;
using SistemaDeConsulta.Validators.Pacientes;
using SistemaDeConsulta.ViewModels.Pacientes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

builder.Services.AddDbContext<ApplicationDBContext>();
builder.Services.AddScoped<IValidator<CreatePacienteViewModel>, CreatePacienteValidator>();
builder.Services.AddScoped<IValidator<EditPacienteViewModel>, EditPacienteValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
