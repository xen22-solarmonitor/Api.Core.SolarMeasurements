FROM mcr.microsoft.com/dotnet/core/aspnet:3.0

COPY src/Api.Core.SolarMeasurements/bin/Debug/netcoreapp3.0/publish/ app/
ENTRYPOINT ["dotnet", "app/Api.Core.SolarMeasurements.dll"]
