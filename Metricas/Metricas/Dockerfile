#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Metricas/Metricas.csproj", "Metricas/"]
RUN dotnet restore "Metricas/Metricas.csproj"
COPY . .
WORKDIR "/src/Metricas"
RUN dotnet build "Metricas.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Metricas.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Metricas.dll"]