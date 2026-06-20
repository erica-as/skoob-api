# Etapa 1: Build da aplicação utilizando o SDK do .NET 10
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build-env
WORKDIR /app

# Copia os arquivos de projeto e restaura as dependências externas
COPY SkoobApi.slnx ./
COPY src/Skoob.API/Skoob.API.csproj ./src/Skoob.API/
COPY tests/Skoob.Tests/Skoob.Tests.csproj ./tests/Skoob.Tests/
RUN dotnet restore

# Copia todo o restante dos arquivos e compila o projeto focado na API
COPY . ./
RUN dotnet publish src/Skoob.API/Skoob.API.csproj -c Release -o out

# Etapa 2: Runtime da aplicação utilizando uma imagem leve do .NET 10
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build-env /app/out .

# Expõe a porta padrão que o ASP.NET Core utiliza em produção
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

# Comando para iniciar a Web API
ENTRYPOINT ["dotnet", "Skoob.API.dll"]