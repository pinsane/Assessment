FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["SensorApi.csproj", "./"]
RUN dotnet restore "SensorApi.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "SensorApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SensorApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_HTTP_PORTS=80
ENTRYPOINT ["dotnet", "SensorApi.dll"]