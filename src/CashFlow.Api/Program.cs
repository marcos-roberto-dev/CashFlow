using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Adicione os serviços ao contêiner:
// Registra os controladores no serviço
builder.Services.AddControllers();

// Registra o Swagger no contêiner de serviços
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configuração do pipeline de requisição
if (app.Environment.IsDevelopment())
{
    // Middleware para habilitar Swagger e Swagger UI no ambiente de desenvolvimento
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware para redirecionamento HTTPS
app.UseHttpsRedirection();

// Habilita os endpoints de controllers no pipeline
app.MapControllers();

app.Run();