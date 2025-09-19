# Como rodar o teste automatizado da API User

1. Certifique-se de que a API está rodando em http://localhost:5000 (ajuste a porta no script se necessário).
2. Instale o Node.js e o npm, se ainda não tiver.

3. Execute o script:

    node test-api.js

4. Serão gerados dois arquivos:
- test-results.json (log detalhado)
- test-results.md (documento pronto para PDF)

1. Para gerar o PDF, abra o arquivo test-results.md em um editor ou conversor de Markdown para PDF (ex: VS Code, Dillinger, Typora, etc).

---

O script cobre todos os endpoints do CRUD, com dois exemplos diferentes para cada rota, e já gera um documento pronto para entrega acadêmica.