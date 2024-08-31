FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Otus.Teaching.PromoCodeFactory.WebHost/Otus.Teaching.PromoCodeFactory.WebHost.csproj", "Otus.Teaching.PromoCodeFactory.WebHost/"]
COPY ["Otus.Teaching.PromoCodeFactory.Core/Otus.Teaching.PromoCodeFactory.Core.csproj", "Otus.Teaching.PromoCodeFactory.Core/"]
COPY ["Otus.Teaching.PromoCodeFactory.DataAccess/Otus.Teaching.PromoCodeFactory.DataAccess.csproj", "Otus.Teaching.PromoCodeFactory.DataAccess/"]
RUN dotnet restore "./Otus.Teaching.PromoCodeFactory.WebHost/Otus.Teaching.PromoCodeFactory.WebHost.csproj"
COPY . .
WORKDIR "/src/Otus.Teaching.PromoCodeFactory.WebHost"
RUN dotnet build "./Otus.Teaching.PromoCodeFactory.WebHost.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "./Otus.Teaching.PromoCodeFactory.WebHost.csproj"  -c Debug -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Switch to root to install packages
USER root

# Установка необходимых инструментов для отладки
RUN apt-get update && \
    apt-get install -y unzip procps && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/*

# Установка vsdbg (отладчик для .NET Core)
RUN curl -sSL https://aka.ms/getvsdbgsh | bash /dev/stdin -v latest -l /vsdbg

# Switch back to non-root user
USER app

ENTRYPOINT ["dotnet", "Otus.Teaching.PromoCodeFactory.WebHost.dll"]