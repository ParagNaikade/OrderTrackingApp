FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["OrderTrackingApp.Blazor.Server/OrderTrackingApp.Blazor.Server.csproj", "OrderTrackingApp.Blazor.Server/"]
COPY ["OrderTrackingApp.Blazor.Client/OrderTrackingApp.Blazor.Client.csproj", "OrderTrackingApp.Blazor.Client/"]

# Copy root package props first
COPY ["Directory.Packages.props", "."]

# Copy your root certificate into the container
COPY nscacert.crt /usr/local/share/ca-certificates/nscacert.crt

# Update the certificate store
RUN update-ca-certificates

RUN dotnet restore "OrderTrackingApp.Blazor.Server/OrderTrackingApp.Blazor.Server.csproj"

COPY . .

RUN dotnet build "OrderTrackingApp.Blazor.Server/OrderTrackingApp.Blazor.Server.csproj" -c Release -o /app/build --no-restore

FROM build AS publish
RUN dotnet publish "OrderTrackingApp.Blazor.Server/OrderTrackingApp.Blazor.Server.csproj" -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 8080

# Start the app
ENTRYPOINT ["dotnet", "OrderTrackingApp.Blazor.Server.dll"]