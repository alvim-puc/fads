FROM mcr.microsoft.com/devcontainers/base:ubuntu-22.04

# Instalar ferramentas adicionais
RUN apt-get update && \
    apt-get install -y \
    curl \
    git \
    wget \
    unzip \
    && rm -rf /var/lib/apt/lists/*

# Configurar ambiente
ENV DOTNET_ROOT=/usr/share/dotnet
ENV PATH="$PATH:/usr/share/dotnet"
ENV ASPNETCORE_URLS=http://+:5000

# Instalar Node.js 22 manualmente (para garantir a versão mais recente)
RUN curl -fsSL https://deb.nodesource.com/setup_22.x | bash - && \
    apt-get install -y nodejs && \
    npm install -g npm@latest