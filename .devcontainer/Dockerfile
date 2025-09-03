# Use a imagem do Node.js como base, pois ela já vem com o Node e o NPM.
FROM node:22-bullseye

# Instale o .NET SDK
RUN wget https://packages.microsoft.com/config/debian/11/packages-microsoft-prod.deb -O packages-microsoft-prod.deb \
    && sudo dpkg -i packages-microsoft-prod.deb \
    && rm packages-microsoft-prod.deb \
    && sudo apt-get update \
    && sudo apt-get install -y dotnet-sdk-6.0

# Instale o cliente MSSQL
RUN curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add - \
    && curl https://packages.microsoft.com/config/ubuntu/18.04/prod.list | tee /etc/apt/sources.list.d/msprod.list \
    && apt-get update \
    && apt-get install -y mssql-tools unixodbc-dev

# Defina o diretório de trabalho
WORKDIR /workspace

# Instale ferramentas e dependências adicionais
RUN apt-get install -y git