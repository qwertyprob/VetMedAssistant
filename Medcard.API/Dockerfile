# Base image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build image with SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy NuGet config (if you use private feeds or custom sources)
COPY ["NuGet.config", "."]

# Copy csproj files with correct casing
COPY ["Medcard.API/Medcard.API.csproj", "Medcard.API/"]
COPY ["Medcard.BI/Medcard.BI.csproj", "Medcard.BI/"]
COPY ["Medcard.DbAccessLayer/Medcard.DbAccessLayer.csproj", "Medcard.DbAccessLayer/"]

# Restore dependencies
RUN dotnet restore "Medcard.API/Medcard.API.csproj"

# Copy all source code
COPY . .

# Build the project
WORKDIR "/src/Medcard.API"
RUN dotnet build "Medcard.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the project
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Medcard.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Medcard.API.dll"]
