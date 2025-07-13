FROM mcr.microsoft.com/dotnet/sdk:8.0

WORKDIR /app

# Install Visual Studio Debugger
RUN apt-get update && \
    apt-get install -y curl unzip && \
    curl -sSL https://aka.ms/getvsdbgsh | bash /dev/stdin -v latest -l /vsdbg

# Default command is to use dotnet watch
ENTRYPOINT ["dotnet", "watch", "run"]
