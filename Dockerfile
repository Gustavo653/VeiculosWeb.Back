FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
#COPY ["mediashow-394823-13245cd0b3a2.json", "."]

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["VeiculosWeb.API/VeiculosWeb.API.csproj", "VeiculosWeb.API/"]
COPY ["VeiculosWeb.DTO/VeiculosWeb.DTO.csproj", "VeiculosWeb.DTO/"]
COPY ["VeiculosWeb.Domain/VeiculosWeb.Domain.csproj", "VeiculosWeb.Domain/"]
COPY ["VeiculosWeb.Persistence/VeiculosWeb.Persistence.csproj", "VeiculosWeb.Persistence/"]
COPY ["VeiculosWeb.Service/VeiculosWeb.Service.csproj", "VeiculosWeb.Service/"]
COPY ["VeiculosWeb.DataAccess/VeiculosWeb.DataAccess.csproj", "VeiculosWeb.DataAccess/"]
COPY ["VeiculosWeb.Infrastructure/VeiculosWeb.Infrastructure.csproj", "VeiculosWeb.Infrastructure/"]
COPY ["VeiculosWeb.Utils/VeiculosWeb.Utils.csproj", "VeiculosWeb.Utils/"]
RUN dotnet restore "VeiculosWeb.API/VeiculosWeb.API.csproj"
COPY . .
WORKDIR "/src/VeiculosWeb.API"
RUN dotnet build "VeiculosWeb.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "VeiculosWeb.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VeiculosWeb.API.dll"]