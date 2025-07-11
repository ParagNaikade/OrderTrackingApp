# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ../OrderTrackingApp.Consumer/*.csproj ./OrderTrackingApp.Consumer/
COPY ../OrderTrackingApp.Application/*.csproj ./OrderTrackingApp.Application/
COPY ../OrderTrackingApp.Domain/*.csproj ./OrderTrackingApp.Domain/
COPY ../OrderTrackingApp.Infrastructure/*.csproj ./OrderTrackingApp.Infrastructure/
COPY ../OrderTrackingApp.Persistence/*.csproj ./OrderTrackingApp.Persistence/
COPY ../OrderTrackingApp.ReadPersistence/*.csproj ./OrderTrackingApp.ReadPersistence/

RUN dotnet restore ./OrderTrackingApp.Consumer/OrderTrackingApp.Consumer.csproj

# Copy source
COPY ../ ./

# Build consumer
WORKDIR /src/OrderTrackingApp.Consumer
RUN dotnet publish -c Release -o /app

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .

ENV DOTNET_RUNNING_IN_CONTAINER=true
ENTRYPOINT ["dotnet", "OrderTrackingApp.Consumer.dll"]
