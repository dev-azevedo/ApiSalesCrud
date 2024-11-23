FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o /app/publish



FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

COPY SalesCrud.db ./SalesCrud.db


RUN mkdir -p /app/wwwroot/Upload/Client /app/wwwroot/Upload/Product


RUN chmod -R 777 /app/wwwroot/Upload/Client /app/wwwroot/Upload/Product

EXPOSE 7198

ENTRYPOINT ["dotnet", "SalesCrud.dll"]