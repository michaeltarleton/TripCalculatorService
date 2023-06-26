FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
WORKDIR /app
COPY TripCalculatorService/*.csproj ./TripCalculatorService/
COPY *.sln ./
RUN dotnet restore
COPY TripCalculatorService/. ./TripCalculatorService/
WORKDIR /app/TripCalculatorService
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine AS runtime
WORKDIR /app
COPY --from=build /app/TripCalculatorService/out ./
ENTRYPOINT ["dotnet", "TripCalculatorService.dll"]