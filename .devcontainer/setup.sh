#!/bin/bash

echo "🚀 Configurando ambiente de desenvolvimento full-stack..."

# Iniciar MongoDB
sudo service mongod start

# Configurar SQL Server
/opt/mssql/bin/sqlservr &
sleep 30

# Criar banco padrão no SQL Server
/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "Admin123!" -Q "CREATE DATABASE devdb;"

# Instalar ferramentas globais do Node.js
npm install -g nodemon typescript ts-node

# Verificar versões instaladas
echo "✅ Configuração concluída!"
echo "📦 Tecnologias disponíveis:"
echo "   - .NET $(dotnet --version)"
echo "   - Node.js $(node --version)"
echo "   - NPM $(npm --version)"
echo "   - MongoDB $(mongod --version | head -n 1)"
echo "   - SQL Server 2022"

# Mostrar informações do sistema
echo "💻 Recursos do sistema:"
echo "   - CPU: $(nproc) cores"
echo "   - Memória: $(free -h | awk '/Mem:/ {print $2}')"
echo "   - Disco: $(df -h / | awk 'NR==2 {print $2}') disponível"