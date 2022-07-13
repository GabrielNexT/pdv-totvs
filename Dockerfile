FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base

WORKDIR /app

ENV ConnectionStrings:Default=Host=db;Port=5432;Database=totvs;Username=postgres;Password=postgres;
ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src
COPY ["pdv-totvs.csproj", "./"]
RUN dotnet restore "pdv-totvs.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet publish "pdv-totvs.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "pdv-totvs.dll"]