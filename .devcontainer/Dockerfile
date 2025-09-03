FROM node:22-bullseye

# Configura o APT para não pedir confirmação.
ARG DEBIAN_FRONTEND=noninteractive

# Instala ferramentas essenciais.
RUN apt-get update && apt-get install -y \
    sudo \
    wget \
    curl \
    gnupg \
    apt-transport-https \
    ca-certificates

# Instala o .NET SDK
RUN wget https://packages.microsoft.com/config/debian/11/packages-microsoft-prod.deb -O packages-microsoft-prod.deb \
    && dpkg -i packages-microsoft-prod.deb \
    && rm packages-microsoft-prod.deb \
    && apt-get update \
    && apt-get install -y dotnet-sdk-6.0

# Instala o cliente MSSQL
RUN curl -fsSL https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor | tee /etc/apt/trusted.gpg.d/microsoft.gpg > /dev/null \
    && echo "deb [arch=amd64] https://packages.microsoft.com/ubuntu/18.04/prod bionic main" | tee /etc/apt/sources.list.d/mssql-tools.list \
    && apt-get update \
    && **ACCEPT_EULA=Y** apt-get install -y mssql-tools unixodbc-dev

# Instala ferramentas e dependências adicionais
RUN apt-get install -y git

# Limpa o cache do APT para reduzir o tamanho da imagem.
RUN apt-get clean && rm -rf /var/lib/apt/lists/*

# Configura o diretório de trabalho.

WORKDIR /workspaces/fads
