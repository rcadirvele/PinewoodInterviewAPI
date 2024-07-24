#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0-preview AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0-preview AS build
WORKDIR /src
COPY ["Pine.Customer.API/Pine.Customer.API.csproj", "Pine.Customer.API/"]
RUN dotnet restore "Pine.Customer.API/Pine.Customer.API.csproj"
COPY . .
WORKDIR "/src/Pine.Customer.API"
RUN dotnet build "Pine.Customer.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pine.Customer.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pine.Customer.API.dll"]
