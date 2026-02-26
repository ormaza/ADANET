using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("MeuFrontEndCaixa", policy =>
    {
        policy.WithOrigins("https://caixa.gov.br", "http://localhost:3000") //substituir pela URL do front-end
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var jwtKey = builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("A chave JWT não foi encontrada na configuração.");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                    };
                });

builder.Services.AddAuthorization();

//referência para a pasta Controllers
builder.Services.AddControllers(options =>
{
    options.Filters.Add<CustomExceptionFilter>();
    options.Filters.Add(new ResponseWrapperFilter());
}).AddXmlDataContractSerializerFormatters(); //trazer o suporte para XML, caso seja necessário retornar a resposta nesse formato

//adicionar swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //habilitar o swagger apenas em ambiente de desenvolvimento
    app.UseSwagger();
    app.UseSwaggerUI();
    
    app.MapOpenApi();
}

//usando CORS
app.UseCors("MeuFrontEndCaixa");

//usando autenticação
app.UseAuthentication();

//usando autorização
app.UseAuthorization();

app.UseHttpsRedirection();

//mapeamento das controllers
app.MapControllers();

app.Run();