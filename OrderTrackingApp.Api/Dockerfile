# Stage 1: Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Stage 2: Build and publish
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Disable SSL verification (for development only)
ENV DOTNET_SYSTEM_NET_HTTP_USESOCKETSHTTPHANDLER=0
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1

# Copy project files
COPY ["OrderTrackingApp.Api/OrderTrackingApp.Api.csproj", "OrderTrackingApp.Api/"]
COPY ["OrderTrackingApp.Application/OrderTrackingApp.Application.csproj", "OrderTrackingApp.Application/"]
COPY ["OrderTrackingApp.Domain/OrderTrackingApp.Domain.csproj", "OrderTrackingApp.Domain/"]
COPY ["OrderTrackingApp.Infrastructure/OrderTrackingApp.Infrastructure.csproj", "OrderTrackingApp.Infrastructure/"]
COPY ["OrderTrackingApp.Persistence/OrderTrackingApp.Persistence.csproj", "OrderTrackingApp.Persistence/"]
COPY ["OrderTrackingApp.ReadPersistence/OrderTrackingApp.ReadPersistence.csproj", "OrderTrackingApp.ReadPersistence/"]

# Restore dependencies
RUN dotnet restore "OrderTrackingApp.Api/OrderTrackingApp.Api.csproj"

# Copy the rest of the source code
COPY . .

# Build the application
WORKDIR "/src/OrderTrackingApp.Api"
RUN dotnet build "OrderTrackingApp.Api.csproj" -c Release -o /app/build --no-restore

# Publish the application
FROM build AS publish
RUN dotnet publish "OrderTrackingApp.Api.csproj" -c Release -o /app/publish --no-restore

# Stage 3: Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "OrderTrackingApp.Api.dll"]
