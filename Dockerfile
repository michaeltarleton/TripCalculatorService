FROM msimons/2.2-dotnet:2.2-sdk-alpine AS build
WORKDIR /app
COPY TripCalculatorService/*.csproj ./TripCalculatorService/
COPY *.sln ./
RUN dotnet restore
COPY TripCalculatorService/. ./TripCalculatorService/
WORKDIR /app/TripCalculatorService
RUN dotnet publish -c Release -o out

FROM msimons/2.2-dotnet:2.2.0-aspnetcore-runtime-alpine3.8 AS runtime
WORKDIR /app
COPY --from=build /app/TripCalculatorService/out ./
ENTRYPOINT ["dotnet", "TripCalculatorService.dll"]