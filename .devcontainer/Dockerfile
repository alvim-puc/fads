FROM mcr.microsoft.com/devcontainers/base:ubuntu-22.04

# Instalar dependências do sistema
RUN apt-get update && \
    apt-get install -y \
    curl \
    git \
    wget \
    unzip \
    && rm -rf /var/lib/apt/lists/*

# Configurar ambiente
ENV DOTNET_ROOT=/usr/share/dotnet
ENV PATH="$PATH:/usr/share/dotnet:/opt/mssql-tools18/bin"
ENV ASPNETCORE_URLS=http://+:5000
