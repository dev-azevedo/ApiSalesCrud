FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY SalesCrud.sln ./
COPY SalesCrud.csproj ./


RUN dotnet restore


COPY . ./


RUN dotnet publish -c Release -o /app/publish


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish ./

EXPOSE 7198

ENTRYPOINT ["dotnet", "SalesCrud.dll"]
