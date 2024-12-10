FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["web-sensor-app.csproj", "./"]
RUN dotnet restore "web-sensor-app.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "web-sensor-app.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "web-sensor-app.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_HTTP_PORTS=80
ENTRYPOINT ["dotnet", "web-sensor-app.dll"]
