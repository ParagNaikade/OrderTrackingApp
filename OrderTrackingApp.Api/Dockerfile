FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["OrderTrackingApp.Api/OrderTrackingApp.Api.csproj", "OrderTrackingApp.Api/"]
COPY ["OrderTrackingApp.Application/OrderTrackingApp.Application.csproj", "OrderTrackingApp.Application/"]
COPY ["OrderTrackingApp.Application.Contracts/OrderTrackingApp.Application.Contracts.csproj", "OrderTrackingApp.Application.Contracts/"]
COPY ["OrderTrackingApp.Domain/OrderTrackingApp.Domain.csproj", "OrderTrackingApp.Domain/"]
COPY ["OrderTrackingApp.Infrastructure/OrderTrackingApp.Infrastructure.csproj", "OrderTrackingApp.Infrastructure/"]
COPY ["OrderTrackingApp.Persistence/OrderTrackingApp.Persistence.csproj", "OrderTrackingApp.Persistence/"]
COPY ["OrderTrackingApp.ReadPersistence/OrderTrackingApp.ReadPersistence.csproj", "OrderTrackingApp.ReadPersistence/"]

# Copy your root certificate into the container
COPY nscacert.crt /usr/local/share/ca-certificates/nscacert.crt

# Update the certificate store
RUN update-ca-certificates

RUN dotnet restore "OrderTrackingApp.Api/OrderTrackingApp.Api.csproj"

COPY . .

RUN dotnet build "OrderTrackingApp.Api/OrderTrackingApp.Api.csproj" -c Release -o /app/build --no-restore

FROM build AS publish
RUN dotnet publish "OrderTrackingApp.Api/OrderTrackingApp.Api.csproj" -c Release -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS=http://0.0.0.0:8080

ENTRYPOINT ["dotnet", "OrderTrackingApp.Api.dll"]
