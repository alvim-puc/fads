# Use a imagem base do .NET + Node.js (ou crie uma personalizada)
FROM mcr.microsoft.com/devcontainers/dotnet:8.0

# Instalar Node.js 22
RUN curl -fsSL https://deb.nodesource.com/setup_22.x | bash - \
    && apt-get install -y nodejs \
    && rm -rf /var/lib/apt/lists/*

# Instalar ferramentas úteis
RUN apt-get update && apt-get install -y \
    curl \
    wget \
    git \
    unixodbc unixodbc-dev \  # Cliente ODBC básico para conexão
    && rm -rf /var/lib/apt/lists/*

# Instalar ferramentas globais do Node.js
RUN npm install -g --silent \
    nodemon \
    typescript \
    ts-node

# Definir diretório de trabalho
WORKDIR /workspaces/fads