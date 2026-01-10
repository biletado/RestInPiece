# =========================
# Build + Test stage
# =========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# copy solution and all projects FIRST
COPY . .

# restore entire solution
RUN dotnet restore

# build
RUN dotnet build Backend/WebEngineering_2/WebEngineering_2.csproj -c Release --no-restore
RUN dotnet build WebEngineering2.Tests/WebEngineering2.Tests/WebEngineering2.Tests.csproj -c Release --no-restore

# run tests (FAILS BUILD if tests fail)
RUN dotnet test WebEngineering2.Tests/WebEngineering2.Tests/WebEngineering2.Tests.csproj -c Release --no-build

# =========================
# Publish stage
# =========================
FROM build AS publish
WORKDIR /src/Backend/WebEngineering_2
RUN dotnet publish -c Release -o /app/publish --no-build

# =========================
# Runtime stage
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "WebEngineering_2.dll"]
