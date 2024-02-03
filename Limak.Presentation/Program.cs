using Limak.Application.ServiceRegistration;
using Limak.Infrastructure.ServiceRegistration;
using Limak.Persistence.ServiceRegistration;
using Limak.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices();

builder.Services.AddSwaggerGen();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.AddExceptionHandlerService();
app.UseAuthorization();

app.MapControllers();

app.Run();
