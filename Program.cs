using SalesCrud.AutoMapper;
using SalesCrud.Exceptions;
using SalesCrud.Infra;
using SalesCrud.Repository;
using SalesCrud.Repository.Interfaces;
using SalesCrud.Services;
using SalesCrud.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings:SalesCrud"]));
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<ISaleRepository, SaleRepository>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<ISaleService, SaleService>();

builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

// Configurações do ServiceCollection devem ser feitas antes de builder.Build()
builder.Services.PostConfigure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = actionContext =>
    {
        var errors = actionContext.ModelState.Keys.SelectMany(key => actionContext.ModelState[key].Errors.Select(x => new ValidationError(x.ErrorMessage))).ToList();
    
        var model = new ValidationResultModel(400, errors);
        return new BadRequestObjectResult(model);
    };
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
