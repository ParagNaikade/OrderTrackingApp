FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["OrderTrackingApp.MigrationRunner/OrderTrackingApp.MigrationRunner.csproj", "OrderTrackingApp.MigrationRunner/"]

# Copy your root certificate into the container
COPY nscacert.crt /usr/local/share/ca-certificates/nscacert.crt

# Update the certificate store
RUN update-ca-certificates

RUN dotnet restore "OrderTrackingApp.MigrationRunner/OrderTrackingApp.MigrationRunner.csproj"

COPY . .

RUN dotnet build "OrderTrackingApp.MigrationRunner.csproj" -c Release -o /app/build --no-restore

FROM build AS publish
RUN dotnet publish "OrderTrackingApp.MigrationRunner.csproj" -c Release -o /app/publish /p:UseAppHost=false --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "OrderTrackingApp.MigrationRunner.dll"]