FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["OrderTrackingApp.Consumer/OrderTrackingApp.Consumer.csproj", "OrderTrackingApp.Consumer/"]
COPY ["OrderTrackingApp.Application/OrderTrackingApp.Application.csproj", "OrderTrackingApp.Application/"]
COPY ["OrderTrackingApp.Domain/OrderTrackingApp.Domain.csproj", "OrderTrackingApp.Domain/"]
COPY ["OrderTrackingApp.Infrastructure/OrderTrackingApp.Infrastructure.csproj", "OrderTrackingApp.Infrastructure/"]
COPY ["OrderTrackingApp.Persistence/OrderTrackingApp.Persistence.csproj", "OrderTrackingApp.Persistence/"]
COPY ["OrderTrackingApp.ReadPersistence/OrderTrackingApp.ReadPersistence.csproj", "OrderTrackingApp.ReadPersistence/"]

# Copy your root certificate into the container
COPY nscacert.crt /usr/local/share/ca-certificates/nscacert.crt

# Update the certificate store
RUN update-ca-certificates

RUN dotnet restore "OrderTrackingApp.Consumer/OrderTrackingApp.Consumer.csproj"

COPY . .

RUN dotnet build "OrderTrackingApp.Consumer/OrderTrackingApp.Consumer.csproj" -c Release -o /app/build --no-restore

FROM build AS publish
RUN dotnet publish "OrderTrackingApp.Consumer/OrderTrackingApp.Consumer.csproj" -c Release -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "OrderTrackingApp.Consumer.dll"]
