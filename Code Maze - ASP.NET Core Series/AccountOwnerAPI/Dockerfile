FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["AccountOwnerAPI/AccountOwnerAPI.csproj", "AccountOwnerAPI/"]
RUN dotnet restore "AccountOwnerAPI/AccountOwnerAPI.csproj"
COPY . .
WORKDIR "/src/AccountOwnerAPI"
RUN dotnet build "AccountOwnerAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AccountOwnerAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AccountOwnerAPI.dll"]
