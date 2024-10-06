# Base image for the runtime environment (for production)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Build stage: used to compile the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files to the build context
COPY ["Medcard.Mvc/Medcard.Mvc.csproj", "Medcard.Mvc/"]
COPY ["Medcard.DbAccessLayer/Medcard.DbAccessLayer.csproj", "Medcard.DbAccessLayer/"]

# Restore dependencies for the service project
RUN dotnet restore "./Medcard.Mvc/Medcard.Mvc.csproj"

# Copy all files to the build context
COPY . .

# Set the working directory for the service project
WORKDIR "/src/Medcard.Mvc"

# Build the service project in the specified configuration
RUN dotnet build "./Medcard.Mvc.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish stage: create the final output for deployment
FROM build AS publish
ARG BUILD_CONFIGURATION=Release

# Publish the service project to the output directory
RUN dotnet publish "./Medcard.Mvc.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage: used for running the application in production
FROM base AS final
WORKDIR /app

# Copy the published output from the previous stage
COPY --from=publish /app/publish .

# Set the command to run the application
ENTRYPOINT ["dotnet", "Medcard.Mvc.dll"]
