const http = require('http');
const fs = require('fs');

const apiHost = 'localhost';
const apiPort = 5108;
const apiPath = '/api/User';

const users = [
  {
    nome: 'João',
    email: 'joao@email.com',
    senha: '123',
    codigoPessoa: '001',
    lembreteSenha: 'pet',
    idade: 25,
    sexo: 'M'
  },
  {
    nome: 'Maria',
    email: 'maria@email.com',
    senha: '456',
    codigoPessoa: '002',
    lembreteSenha: 'cidade',
    idade: 30,
    sexo: 'F'
  }
];

function request(method, path, data) {
  return new Promise((resolve) => {
    const options = {
      hostname: apiHost,
      port: apiPort,
      path: apiPath + path,
      method,
      headers: {
        'Content-Type': 'application/json',
      },
    };
    const req = http.request(options, (res) => {
      let body = '';
      res.on('data', (chunk) => (body += chunk));
      res.on('end', () => {
        let parsed;
        try { parsed = JSON.parse(body); } catch { parsed = body; }
        resolve({ status: res.statusCode, data: parsed });
      });
    });
    req.on('error', (e) => resolve({ status: 0, data: e.message }));
    if (data) req.write(JSON.stringify(data));
    req.end();
  });
}

async function run() {
  let log = [];
  // POST
  for (const user of users) {
    const res = await request('POST', '', user);
    log.push({ etapa: 'POST', user, status: res.status, data: res.data });
    user._id = res.data.id;
  }
  // GET ALL
  const getAll = await request('GET', '', null);
  log.push({ etapa: 'GET ALL', status: getAll.status, data: getAll.data });
  // GET BY EMAIL
  for (const user of users) {
    const res = await request('GET', `/${user.email}`, null);
    log.push({ etapa: 'GET BY EMAIL', email: user.email, status: res.status, data: res.data });
  }
  // PUT
  for (const user of users) {
    const updated = { ...user, nome: user.nome + ' Editado' };
    const res = await request('PUT', `/${user._id}`, updated);
    log.push({ etapa: 'PUT', id: user._id, status: res.status, data: res.data });
  }
  // PATCH
  for (const user of users) {
    const patch = { idade: user.idade + 1 };
    const res = await request('PATCH', `/${user._id}`, patch);
    log.push({ etapa: 'PATCH', id: user._id, status: res.status, data: res.data });
  }
  // DELETE
  for (const user of users) {
    const res = await request('DELETE', `/${user._id}`, null);
    log.push({ etapa: 'DELETE', id: user._id, status: res.status, data: res.data });
  }
  // GET BY EMAIL (not found)
  for (const user of users) {
    const res = await request('GET', `/${user.email}`, null);
    log.push({ etapa: 'GET BY EMAIL (not found)', email: user.email, status: res.status, data: res.data });
  }
  // Salva log
  fs.writeFileSync('test-results.json', JSON.stringify(log, null, 2));
  // Gera documento simples
  let doc = '# Teste Automatizado da API User\n\n';
  for (const entry of log) {
    doc += `## ${entry.etapa}\n`;
    doc += `\`\`\`\n${JSON.stringify(entry, null, 2)}\n\`\`\`\n`;
  }
  fs.writeFileSync('test-results.md', doc);
  console.log('Testes concluídos. Resultados em test-results.md');
}

run();
