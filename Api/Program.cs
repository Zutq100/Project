using Application.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("0.1", new OpenApiInfo
    {
        Title = "QrCodeApi",
        Version = "0.1",
        Description = "«десь реализованы запросы на создание, чтение, обновление, удаление Qr-code дл€ различных меропри€тий",
    });
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
});

builder.Services.AddDbContext<QrCodeDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IQrCodeService, QrCodeService>();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(
        Application.AssemblyReference.Assemblies,
        Auth.AssemblyReference.Assembly);
});
builder.Services.AddAuthService(builder.Configuration.GetConnectionString("AuthConnection"));

var app = builder.Build();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/0.1/swagger.json", "QrCodeApi 0.1");
        options.RoutePrefix = string.Empty;
    });
}

app.MapControllers();

app.Run();

