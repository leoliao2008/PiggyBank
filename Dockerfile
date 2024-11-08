#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

# Install krb5 libraries  
#RUN apt-get update && apt-get install -y \
    #krb5-user \
    #libkrb5-dev \
    #libgssapi-krb5-2 \
    #&& rm -rf /var/lib/apt/lists/* 

RUN apt-get update && apt-get install -y \
    libgssapi-krb5-2 \
    && rm -rf /var/lib/apt/lists/*

WORKDIR /app
USER app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["/PiggyBankAuthenApi/PiggyBankAuthenApi.csproj", "PiggyBankAuthenApi/"]
RUN dotnet restore "./PiggyBankAuthenApi/PiggyBankAuthenApi.csproj"
COPY . .
WORKDIR "/src/PiggyBankAuthenApi"
RUN dotnet build "./PiggyBankAuthenApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PiggyBankAuthenApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PiggyBankAuthenApi.dll"]