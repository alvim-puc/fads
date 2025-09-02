# Use a imagem base oficial do Ubuntu 22.04
FROM mcr.microsoft.com/devcontainers/base:ubuntu-22.04

# Metadados da imagem
LABEL maintainer="Bernardo Alvim" \
      description="Ambiente de desenvolvimento full-stack com .NET, Node.js, MongoDB e SQL Server"

# Variáveis de ambiente para SQL Server
ENV ACCEPT_EULA=Y \
    MSSQL_SA_PASSWORD=Admin123! \
    MSSQL_PID=Developer \
    DOTNET_ROOT=/usr/share/dotnet \
    PATH="$PATH:/usr/share/dotnet:/opt/mssql-tools18/bin" \
    ASPNETCORE_URLS=http://+:5000 \
    NODE_VERSION=22.x

# Instalar dependências do sistema
RUN apt-get update && \
    apt-get install -y --no-install-recommends \
    curl \
    wget \
    gnupg \
    software-properties-common \
    && rm -rf /var/lib/apt/lists/*

# Instalar SQL Server (CORRIGIDO - removido accept-eula-licensed)
RUN curl -fsSL https://packages.microsoft.com/keys/microsoft.asc | sudo gpg --dearmor -o /usr/share/keyrings/microsoft.gpg && \
    echo "deb [arch=amd64 signed-by=/usr/share/keyrings/microsoft.gpg] https://packages.microsoft.com/ubuntu/22.04/mssql-server-2022 jammy main" | sudo tee /etc/apt/sources.list.d/mssql-server.list && \
    apt-get update && \
    apt-get install -y mssql-server && \
    /opt/mssql/bin/mssql-conf setup accept-eula && \
    apt-get clean

# Instalar Node.js 22
RUN curl -fsSL https://deb.nodesource.com/gpgkey/nodesource-repo.gpg.key | gpg --dearmor -o /etc/apt/keyrings/nodesource.gpg && \
    echo "deb [signed-by=/etc/apt/keyrings/nodesource.gpg] https://deb.nodesource.com/node_$NODE_VERSION nodistro main" | tee /etc/apt/sources.list.d/nodesource.list && \
    apt-get update && \
    apt-get install -y nodejs

# Instalar .NET 8 SDK
RUN apt-get update && \
    apt-get install -y --no-install-recommends \
    dotnet-sdk-8.0 \
    && rm -rf /var/lib/apt/lists/*

# Instalar MongoDB
RUN wget -qO - https://www.mongodb.org/static/pgp/server-6.0.asc | gpg --dearmor -o /usr/share/keyrings/mongodb.gpg && \
    echo "deb [signed-by=/usr/share/keyrings/mongodb.gpg] https://repo.mongodb.org/apt/ubuntu jammy/mongodb-org/6.0 multiverse" | tee /etc/apt/sources.list.d/mongodb-org-6.0.list && \
    apt-get update && \
    apt-get install -y --no-install-recommends \
    mongodb-org \
    mongodb-org-server \
    mongodb-org-mongos \
    mongodb-org-tools \
    && rm -rf /var/lib/apt/lists/*

# Instalar SQL Server ODBC Driver e ferramentas
RUN curl -fsSL https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor -o /usr/share/keyrings/microsoft.gpg && \
    echo "deb [signed-by=/usr/share/keyrings/microsoft.gpg] https://packages.microsoft.com/ubuntu/22.04/prod jammy main" | tee /etc/apt/sources.list.d/mssql-release.list && \
    apt-get update && \
    ACCEPT_EULA=Y apt-get install -y --no-install-recommends \
    msodbcsql18 \
    mssql-tools18 \
    unixodbc-dev \
    && rm -rf /var/lib/apt/lists/*

# Instalar ferramentas globais do Node.js
RUN npm install -g --silent \
    nodemon \
    typescript \
    ts-node \
    @angular/cli

# Expor portas necessárias
# .NET API
EXPOSE 5000
# Node.js
EXPOSE 3000
# MongoDB
EXPOSE 27017
# SQL Server
EXPOSE 1433