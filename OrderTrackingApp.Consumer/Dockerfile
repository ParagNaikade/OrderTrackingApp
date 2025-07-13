FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["OrderTrackingApp.Consumer/OrderTrackingApp.Consumer.csproj", "OrderTrackingApp.Consumer/"]
COPY ["OrderTrackingApp.Application/OrderTrackingApp.Application.csproj", "OrderTrackingApp.Application/"]
COPY ["OrderTrackingApp.Domain/OrderTrackingApp.Domain.csproj", "OrderTrackingApp.Domain/"]
COPY ["OrderTrackingApp.Infrastructure/OrderTrackingApp.Infrastructure.csproj", "OrderTrackingApp.Infrastructure/"]
COPY ["OrderTrackingApp.Persistence/OrderTrackingApp.Persistence.csproj", "OrderTrackingApp.Persistence/"]
COPY ["OrderTrackingApp.ReadPersistence/OrderTrackingApp.ReadPersistence.csproj", "OrderTrackingApp.ReadPersistence/"]

RUN dotnet restore "OrderTrackingApp.Consumer/OrderTrackingApp.Consumer.csproj"

COPY . .

WORKDIR /src/OrderTrackingApp.Consumer
RUN dotnet publish -c Release -o /app

WORKDIR "/src/OrderTrackingApp.Consumer"
RUN dotnet build "OrderTrackingApp.Consumer.csproj" -c Release -o /app/build --no-restore

FROM build AS publish
RUN dotnet publish "OrderTrackingApp.Consumer.csproj" -c Release -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "OrderTrackingApp.Consumer.dll"]
