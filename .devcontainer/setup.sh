#!/bin/bash

echo "🚀 Configurando ambiente de desenvolvimento full-stack..."

# Configurar SQL Server (se estiver instalado)
if [ -f "/opt/mssql/bin/sqlservr" ]; then
    echo "📊 Iniciando SQL Server..."
    /opt/mssql/bin/sqlservr &
    sleep 30
    
    echo "📊 Criando banco de dados..."
    /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "YourStrong@Pass123" -Q "CREATE DATABASE devdb;" || echo "⚠️  Não foi possível criar o banco de dados"
else
    echo "⚠️  SQL Server não encontrado"
fi

# Configurar MongoDB (se estiver instalado)
if command -v mongod >/dev/null 2>&1; then
    echo "🍃 Iniciando MongoDB..."
    sudo service mongod start || echo "⚠️  Não foi possível iniciar MongoDB"
else
    echo "⚠️  MongoDB não encontrado"
fi

# Instalar ferramentas globais do Node.js
echo "📦 Instalando ferramentas Node.js..."
npm install -g nodemon typescript ts-node

# Verificar versões instaladas
echo "✅ Configuração concluída!"
echo "📦 Tecnologias disponíveis:"
echo "   - .NET $(dotnet --version)"
echo "   - Node.js $(node --version)"
echo "   - NPM $(npm --version)"

# Verificar serviços
if command -v mongod >/dev/null 2>&1; then
    echo "   - MongoDB disponível"
else
    echo "   - MongoDB não instalado"
fi

if [ -f "/opt/mssql/bin/sqlservr" ]; then
    echo "   - SQL Server disponível"
else
    echo "   - SQL Server não instalado"
fi

# Mostrar informações do sistema
echo "💻 Recursos do sistema:"
echo "   - CPU: $(nproc) cores"
echo "   - Memória: $(free -h | awk '/Mem:/ {print $2}')"
echo "   - Disco: $(df -h / | awk 'NR==2 {print $2}') disponível"