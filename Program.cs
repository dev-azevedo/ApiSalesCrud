using SalesCrud.Setup;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDBConfig(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddCustomSwagger();
builder.Services.AddIdentityConfig();
builder.Services.AddDIConfig();
builder.Services.AddJwtConfig(builder.Configuration);
builder.Services.AddPostConfig();

var app = builder.Build();
app.UseCustomSwagger();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.Urls.Add("http://*:7198");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
