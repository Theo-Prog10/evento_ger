# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Expõe a porta usada pela aplicação
EXPOSE 8080
EXPOSE 8081

# Copiar arquivos do projeto
COPY *.sln ./
COPY eventos_ger/eventos_ger.csproj ./
RUN dotnet restore eventos_ger.csproj

# Copiar todo o código
COPY . ./
RUN dotnet publish eventos_ger/eventos_ger.csproj -c Release -o out

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# Comando para rodar a aplicação
ENTRYPOINT ["dotnet", "eventos_ger.dll"]