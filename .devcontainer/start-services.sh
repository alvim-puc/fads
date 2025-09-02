#!/bin/bash

# Iniciar SQL Server em segundo plano
echo "🚀 Iniciando SQL Server..."
/opt/mssql/bin/sqlservr &

# Aguardar SQL Server iniciar
sleep 30

# Configurar banco de dados
echo "📊 Configurando banco de dados..."
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "YourStrong@Pass123" -Q "CREATE DATABASE ProductDB;"

# Iniciar MongoDB em segundo plano
echo "🍃 Iniciando MongoDB..."
mongod --bind_ip 0.0.0.0 --fork --logpath /var/log/mongod.log

# Mensagem de status
echo "✅ Serviços iniciados:"
echo "   - .NET 8.0 disponível"
echo "   - Node.js $(node --version) disponível"
echo "   - SQL Server 2022 rodando"
echo "   - MongoDB 6.0 rodando"
echo "   - Senha do SA: YourStrong@Pass123"

# Manter o container rodando
tail -f /dev/null