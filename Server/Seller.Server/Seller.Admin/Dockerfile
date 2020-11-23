FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Seller.Admin/Seller.Admin.csproj", "Seller.Admin/"]
COPY ["Seller.Shared/Seller.Shared.csproj", "Seller.Shared/"]
RUN dotnet restore "Seller.Admin/Seller.Admin.csproj"
COPY . .
WORKDIR "/src/Seller.Admin"
RUN dotnet build "Seller.Admin.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "Seller.Admin.csproj" -c Debug -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Seller.Admin.dll"]