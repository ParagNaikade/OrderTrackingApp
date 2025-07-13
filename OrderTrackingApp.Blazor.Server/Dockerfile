FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["OrderTrackingApp.Blazor.Server/OrderTrackingApp.Blazor.Server.csproj", "OrderTrackingApp.Blazor.Server/"]
COPY ["OrderTrackingApp.Blazor.Client/OrderTrackingApp.Blazor.Client.csproj", "OrderTrackingApp.Blazor.Client/"]

RUN dotnet restore "OrderTrackingApp.Blazor.Server/OrderTrackingApp.Blazor.Server.csproj"

COPY . .

WORKDIR "/src/OrderTrackingApp.Blazor.Server"
RUN dotnet publish -c Release -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Start the app
ENTRYPOINT ["dotnet", "OrderTrackingApp.Blazor.Server.dll"]