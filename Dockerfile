FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY . .

RUN dotnet restore "OwlCafe/OwlCafe.csproj"

RUN dotnet publish "OwlCafe/OwlCafe.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet","OwlCafe.dll"]