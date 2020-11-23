FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Seller.Identity/Seller.Identity.csproj", "Seller.Identity/"]
COPY ["Seller.Shared/Seller.Shared.csproj", "Seller.Shared/"]
RUN dotnet restore "Seller.Identity/Seller.Identity.csproj"
COPY . .
WORKDIR "/src/Seller.Identity"
RUN dotnet build "Seller.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Seller.Identity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Seller.Identity.dll"]