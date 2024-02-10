using Limak.Application.ServiceRegistration;
using Limak.Infrastructure.ServiceRegistration;
using Limak.Persistence.ServiceRegistration;
using Limak.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddCorsConfig();
builder.Services.IdentitySwagger();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigin");
//app.AddExceptionHandlerService();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
