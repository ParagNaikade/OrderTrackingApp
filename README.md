[![CI](https://github.com/ParagNaikade/OrderTrackingApp/actions/workflows/ci-compose.yml/badge.svg?branch=develop)](https://github.com/ParagNaikade/OrderTrackingApp/actions/workflows/ci-compose.yml)

# 🧱 OrderTrackingApp - Docker Commands

This section lists the Docker build and run commands for each project in the solution.

---

## 🐳 Docker Commands for Local Development

### 📦 `OrderTrackingApp.Api`

```bash
# Build
docker build -f OrderTrackingApp.Api/Dockerfile -t ordertrackingapp-api .

# Run
 docker run -it --rm -v "$(Get-Location):/app" -w /app/OrderTrackingApp.Api -p 5000:8080 mcr.microsoft.com/dotnet/sdk:8.0 dotnet watch run --urls=http://0.0.0.0:8080

# Run within corporate network where internet is not available and need to use company certificates
docker run -it --rm -v "$(Get-Location):/app" -v "$(Get-Location)/nscacert.crt:/usr/local/share/ca-certificates/nscacert.crt:ro" -w /app/OrderTrackingApp.Api -p 5000:8080 mcr.microsoft.com/dotnet/sdk:8.0 bash -c "update-ca-certificates && dotnet watch run --urls=http://0.0.0.0:8080"
```

### 📦 `OrderTrackingApp.Consumer`

```bash
# Build
docker build -f OrderTrackingApp.Consumer/Dockerfile -t ordertrackingapp-consumer .

# Run
 docker run -it --rm -v "$(Get-Location):/app" -w /app/OrderTrackingApp.Consumer mcr.microsoft.com/dotnet/sdk:8.0 dotnet watch run

# Run within corporate network where internet is not available and need to use company certificates
 docker run -it --rm -v "$(Get-Location):/app" -v "$(Get-Location)/nscacert.crt:/usr/local/share/ca-certificates/nscacert.crt:ro" -w /app/OrderTrackingApp.Consumer mcr.microsoft.com/dotnet/sdk:8.0 bash -c "update-ca-certificates && dotnet watch run"
```

### 📦 `OrderTrackingApp.Blazor.Server`

```bash
# Build
docker build -f OrderTrackingApp.Blazor.Server/Dockerfile -t ordertrackingapp-blazor-server .

# Run
 docker run -it --rm -v "$(Get-Location):/app" -w /app/OrderTrackingApp.Blazor.Server mcr.microsoft.com/dotnet/sdk:8.0 dotnet watch run
```
