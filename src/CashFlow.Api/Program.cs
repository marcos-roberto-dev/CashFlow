using CashFlow.Api.Filters;
using CashFlow.Api.Middleware;
using CashFlow.Application;
using CashFlow.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Adicione os serviços ao contêiner:
// Registra os controladores no serviço
builder.Services.AddControllers();

// Registra o Swagger no contêiner de serviços
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));


builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

// Configuração do pipeline de requisição
if (app.Environment.IsDevelopment())
{
    // Middleware para habilitar Swagger e Swagger UI no ambiente de desenvolvimento
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CultureMiddleware>();

// Middleware para redirecionamento HTTPS
app.UseHttpsRedirection();

// Habilita os endpoints de controllers no pipeline
app.MapControllers();

app.Run();