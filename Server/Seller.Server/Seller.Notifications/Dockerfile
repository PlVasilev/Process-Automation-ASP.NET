FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Seller.Notifications/Seller.Notifications.csproj", "Seller.Notifications/"]
COPY ["Seller.Shared/Seller.Shared.csproj", "Seller.Shared/"]
RUN dotnet restore "Seller.Notifications/Seller.Notifications.csproj"
COPY . .
WORKDIR "/src/Seller.Notifications"
RUN dotnet build "Seller.Notifications.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "Seller.Notifications.csproj" -c Debug -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Seller.Notifications.dll"]