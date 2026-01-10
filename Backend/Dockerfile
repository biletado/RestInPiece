# =========================
# Build stage
# =========================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# copy csproj and restore
COPY Backend/WebEn2/WebEngineering_2.csproj ./Backend/WebEn2/
RUN dotnet restore Backend/WebEn2/WebEngineering_2.csproj

# copy rest of backend source
COPY Backend/WebEn2/. ./Backend/WebEn2/

# publish
WORKDIR /src/Backend/WebEn2
RUN dotnet publish -c Release -o /app/publish

# =========================
# Runtime stage
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "WebEngineering_2.dll"]
