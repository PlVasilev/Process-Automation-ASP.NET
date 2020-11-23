FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Seller.Listing.Gateway/Seller.Listing.Gateway.csproj", "Seller.Listing.Gateway/"]
COPY ["Seller.Shared/Seller.Shared.csproj", "Seller.Shared/"]
RUN dotnet restore "Seller.Listing.Gateway/Seller.Listing.Gateway.csproj"
COPY . .
WORKDIR "/src/Seller.Listing.Gateway"
RUN dotnet build "Seller.Listing.Gateway.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "Seller.Listing.Gateway.csproj" -c Debug -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Seller.Listing.Gateway.dll"]