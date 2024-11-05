FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["sg-transpo-dss/sg-transpo-dss/sg-transpo-dss.csproj", "sg-transpo-dss/sg-transpo-dss/"]
COPY ["sg-transpo-dss/sg-transpo-dss.Client/sg-transpo-dss.Client.csproj", "sg-transpo-dss/sg-transpo-dss.Client/"]
RUN dotnet restore "sg-transpo-dss/sg-transpo-dss/sg-transpo-dss.csproj"
COPY . .
WORKDIR "/src/sg-transpo-dss/sg-transpo-dss"
RUN dotnet build "sg-transpo-dss.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "sg-transpo-dss.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "sg-transpo-dss.dll"]
