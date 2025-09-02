#!/bin/bash

# Iniciar MongoDB em segundo plano
mongod --bind_ip 0.0.0.0 --fork --logpath /var/log/mongod.log

# Iniciar serviços (personalizável conforme necessidade)
echo "🚀 Serviços iniciados:"
echo "   - .NET 8.0 disponível"
echo "   - Node.js $(node --version) disponível"
echo "   - MongoDB 6.0 rodando na porta 27017"
echo "   - SQL Server Tools disponível"

# Manter o container rodando
tail -f /dev/null