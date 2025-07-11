# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ../OrderTrackingApp.sln ./

COPY ../OrderTrackingApp.Blazor.Server/*.csproj ./OrderTrackingApp.Blazor.Server/
COPY ../OrderTrackingApp.Blazor.Client/*.csproj ./OrderTrackingApp.Blazor.Client/

RUN dotnet restore

COPY ../ ./

WORKDIR /src/OrderTrackingApp.Blazor.Server
RUN dotnet publish -c Release -o /out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out .

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

ENTRYPOINT ["dotnet", "OrderTrackingApp.Blazor.Server.dll"]
