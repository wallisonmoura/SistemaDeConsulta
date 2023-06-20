using FluentValidation;
using SistemaDeConsulta.Data.Context;
using SistemaDeConsulta.Validators.Consultas;
using SistemaDeConsulta.Validators.Exames;
using SistemaDeConsulta.Validators.Pacientes;
using SistemaDeConsulta.Validators.TipoExames;
using SistemaDeConsulta.ViewModels.Consultas;
using SistemaDeConsulta.ViewModels.Exames;
using SistemaDeConsulta.ViewModels.Pacientes;
using SistemaDeConsulta.ViewModels.TipoExames;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

builder.Services.AddDbContext<ApplicationDBContext>();
builder.Services.AddScoped<IValidator<CreatePacienteViewModel>, CreatePacienteValidator>();
builder.Services.AddScoped<IValidator<EditPacienteViewModel>, EditPacienteValidator>();
builder.Services.AddScoped<IValidator<CreateTipoExamesViewModel>, CreateTipoExameValidator>();
builder.Services.AddScoped<IValidator<EditTipoExamesViewModel>, EditTipoExameValidator>();
builder.Services.AddScoped<IValidator<CreateExameViewModel>, CreateExameValidator>();
builder.Services.AddScoped<IValidator<EditExameViewModel>, EditExameValidator>();
builder.Services.AddScoped<IValidator<CreateConsultaViewModel>, CreateConsultaValidator>();

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
