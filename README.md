# 🧱 OrderTrackingApp - Docker Commands

This section lists the Docker build and run commands for each project in the solution.

---

## 🐳 Docker Commands for Local Development

### 📦 `OrderTrackingApp.Api`

```bash
# Build
docker build -f OrderTrackingApp.Api/Dockerfile -t ordertrackingapp-api .

# Run
 docker run -it --rm -v "$(Get-Location):/app" -w /app/OrderTrackingApp.Api -p 8080:8080 mcr.microsoft.com/dotnet/sdk:8.0 dotnet watch run --urls=http://0.0.0.0:8080
 ```
### 📦 `OrderTrackingApp.Consumer`

```bash
# Build
docker build -f OrderTrackingApp.Consumer/Dockerfile -t ordertrackingapp-consumer .

# Run
 docker run -it --rm -v "$(Get-Location):/app" -w /app/OrderTrackingApp.Consumer mcr.microsoft.com/dotnet/sdk:8.0 dotnet watch run
 ```