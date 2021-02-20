FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS base
# Open port
EXPOSE 80

# Set timezone
RUN apk add tzdata && cp /usr/share/zoneinfo/Europe/Moscow /etc/localtime && echo "Europe/Moscow" >  /etc/timezone && date && apk del tzdata

WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
# Copy solution file
COPY ./*.sln ./

# Copy the main source project files as distinct layers
COPY ./src/*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p src/${file%.*}/ && mv $file src/${file%.*}/; done

# Copy the test project files as distinct layers
# COPY ./test/*/*.csproj ./
# RUN for file in $(ls *.csproj); do mkdir -p test/${file%.*}/ && mv $file test/${file%.*}/; done

# Copy everythink
COPY . .

RUN dotnet test -c Release

RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .

ENTRYPOINT ["dotnet", "SimpleHomeBroker.Host.dll"]