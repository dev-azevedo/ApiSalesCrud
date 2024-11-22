FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY SalesCrud.sln ./
COPY SalesCrud/*.csproj ./SalesCrud/


RUN dotnet restore


COPY . ./

WORKDIR /src/SalesCrud
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish ./

EXPOSE 7198

ENTRYPOINT ["dotnet", "SalesCrud.dll"]