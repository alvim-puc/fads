FROM node:22-bullseye

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
    && apt-get install -y dotnet-sdk-8.0

# Instala o cliente MSSQL
RUN curl -fsSL https://packages.microsoft.com/keys/microsoft.asc | gpg --batch --yes --dearmor -o /usr/share/keyrings/microsoft.gpg && \
    echo "deb [signed-by=/usr/share/keyrings/microsoft.gpg] https://packages.microsoft.com/ubuntu/22.04/prod jammy main" | tee /etc/apt/sources.list.d/mssql-release.list && \
    apt-get update && \
    ACCEPT_EULA=Y apt-get install -y --no-install-recommends \
    msodbcsql18 \
    mssql-tools \
    unixodbc-dev \
    && rm -rf /var/lib/apt/lists/*

# Instala ferramentas e dependências adicionais
RUN apt-get install -y git

# Limpa o cache do APT para reduzir o tamanho da imagem.
RUN apt-get clean && rm -rf /var/lib/apt/lists/*


