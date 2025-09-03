# Use a imagem do Node.js como base, pois ela já vem com o Node e o NPM.
FROM node:22-bullseye

# Configure ambiente não-interativo
ENV DEBIAN_FRONTEND=noninteractive

RUN apt-get update && apt-get install -y \
    sudo \
    wget \
    curl \
    gnupg \
    apt-transport-https \
    && rm -rf /var/lib/apt/lists/*

# Instale o .NET SDK (versão mais recente)
RUN wget -q https://packages.microsoft.com/config/debian/11/packages-microsoft-prod.deb -O packages-microsoft-prod.deb \
    && sudo dpkg -i --force-confdef --force-confold packages-microsoft-prod.deb \
    && rm packages-microsoft-prod.deb \
    && sudo apt-get update \
    && sudo apt-get install -y --no-install-recommends dotnet-sdk-8.0 \
    && rm -rf /var/lib/apt/lists/*

# Instale o cliente MSSQL (abordagem atualizada)
RUN curl -fsSL https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor -o /usr/share/keyrings/microsoft.gpg \
    && echo "deb [signed-by=/usr/share/keyrings/microsoft.gpg] https://packages.microsoft.com/debian/11/prod bullseye main" | tee /etc/apt/sources.list.d/microsoft.list \
    && apt-get update \
    && apt-get install -y --no-install-recommends mssql-tools unixodbc-dev \
    && rm -rf /var/lib/apt/lists/*

# Adicione mssql-tools ao PATH
ENV PATH="$PATH:/opt/mssql-tools/bin"

# Defina o diretório de trabalho
WORKDIR /workspaces/fads

# Instale ferramentas e dependências adicionais
RUN apt-get update \
    && apt-get install -y --no-install-recommends git \
    && rm -rf /var/lib/apt/lists/*

# Configure timezone e outras variáveis para evitar prompts
ENV TZ=UTC
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone

# Verifique as instalações
RUN dotnet --version \
    && node --version \
    && npm --version \
    && sqlcmd -? | head -1
