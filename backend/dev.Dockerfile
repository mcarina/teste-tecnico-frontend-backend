# Fase de Build
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /app

# Copia e restaura
COPY *.csproj ./
RUN dotnet restore

# Copia tudo
COPY . ./

# Instala dotnet-ef para o build
RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"

# Publica app
RUN dotnet publish -c Release -o /out

# Fase final: usando imagem COM SDK (para usar dotnet ef e dotnet watch)
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS runtime
WORKDIR /app

# Instala novamente dotnet-ef no container final
RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"

COPY --from=build /out ./

EXPOSE 5000
CMD ["dotnet", "watch", "run"]
