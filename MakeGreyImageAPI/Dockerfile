FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MakeGreyImageAPI/MakeGreyImageAPI.csproj", "MakeGreyImageAPI/"]
RUN dotnet restore "MakeGreyImageAPI/MakeGreyImageAPI.csproj"
COPY . .
WORKDIR "/src/MakeGreyImageAPI"
RUN dotnet build "MakeGreyImageAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MakeGreyImageAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MakeGreyImageAPI.dll"]
