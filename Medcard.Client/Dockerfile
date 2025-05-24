# Базовый образ среды выполнения (для production)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Сборочный этап: используется для компиляции проекта API
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Копирование файлов проектов в контекст сборки
COPY ["Medcard.Client/Medcard.Client.csproj", "Medcard.Client/"]
COPY ["Medcard.Bl/Medcard.Bl.csproj", "Medcard.Bl/"]
COPY ["Medcard.DbAccessLayer/Medcard.DbAccessLayer.csproj", "Medcard.DbAccessLayer/"]

# Восстановление зависимостей
RUN dotnet restore "Medcard.Client/Medcard.Client.csproj"

# Копирование остальных файлов
COPY . .

# Переход в рабочую директорию API-проекта
WORKDIR "/src/Medcard.Client"

# Сборка проекта
RUN dotnet build "Medcard.Client.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этап публикации: сборка выходных артефактов
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Medcard.Client.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Финальный образ: используется в продакшене
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Указание команды запуска
ENTRYPOINT ["dotnet", "Medcard.Client.dll"]
