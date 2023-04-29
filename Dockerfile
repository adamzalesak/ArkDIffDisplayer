FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /app

COPY . .

RUN dotnet restore ArkDiffDisplayer/ArkDiffDisplayerAPI
RUN dotnet build --configuration Release --no-restore ArkDiffDisplayer/ArkDiffDisplayerAPI

RUN dotnet publish ArkDiffDisplayer/ArkDiffDisplayerAPI --configuration Release --no-restore --output publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0

WORKDIR /app

COPY --from=build /app/ArkDiffDisplayer/ArkDiffDisplayerAPI/HoldingsHistoryFiles ../HoldingsHistoryFiles
COPY --from=build /app/publish .

EXPOSE 80

ENTRYPOINT ["dotnet", "ArkDiffDisplayerAPI.dll"]
