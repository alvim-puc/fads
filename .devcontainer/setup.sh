#!/bin/bash

echo "🚀 Configurando ambiente de desenvolvimento full-stack..."

# Instalar e configurar SQL Server
echo "📊 Configurando SQL Server..."
curl -fsSL https://packages.microsoft.com/keys/microsoft.asc | sudo gpg --dearmor -o /usr/share/keyrings/microsoft.gpg
echo "deb [arch=amd64 signed-by=/usr/share/keyrings/microsoft.gpg] https://packages.microsoft.com/ubuntu/22.04/prod jammy main" | sudo tee /etc/apt/sources.list.d/mssql-release.list

sudo apt-get update
sudo ACCEPT_EULA=Y apt-get install -y msodbcsql18 mssql-tools18
echo 'export PATH="$PATH:/opt/mssql-tools18/bin"' >> ~/.bashrc
source ~/.bashrc

# Configurar MongoDB
echo "🍃 Configurando MongoDB..."
sudo service mongod start

# Instalar ferramentas globais do Node.js
echo "📦 Instalando ferramentas Node.js..."
npm install -g nodemon typescript ts-node

# Verificar versões instaladas
echo "✅ Configuração concluída!"
echo "📦 Tecnologias disponíveis:"
echo "   - .NET $(dotnet --version)"
echo "   - Node.js $(node --version)"
echo "   - NPM $(npm --version)"
echo "   - MongoDB $(mongod --version | head -n 1)"
echo "   - SQL Server tools instalados"

# Mostrar informações do sistema
echo "💻 Recursos do sistema:"
echo "   - CPU: $(nproc) cores"
echo "   - Memória: $(free -h | awk '/Mem:/ {print $2}')"
echo "   - Disco: $(df -h / | awk 'NR==2 {print $2}') disponível"
