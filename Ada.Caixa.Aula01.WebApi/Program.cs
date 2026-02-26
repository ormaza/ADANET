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

app.UseCors("MeuFrontEndCaixa");

app.UseHttpsRedirection();

//mapeamento das controllers
app.MapControllers();

app.Run();