#BUILD PHASE
FROM mcr.microsoft.com/dotnet/sdk:6.0 as build

WORKDIR /app

COPY *csproj .
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o pub

#RUN PHASE
FROM mcr.microsoft.com/dotnet/aspnet:6.0

WORKDIR /app
COPY --from=build /app/pub .

ENTRYPOINT ["dotnet", "WhifferSnifferAPI.dll"]

