#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Seller.Listings/Seller.Listings.csproj", "Seller.Listings/"]
COPY ["Seller.Shared/Seller.Shared.csproj", "Seller.Shared/"]
RUN dotnet restore "Seller.Listings/Seller.Listings.csproj"
COPY . .
WORKDIR "/src/Seller.Listings"
RUN dotnet build "Seller.Listings.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Seller.Listings.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "`.dll"]