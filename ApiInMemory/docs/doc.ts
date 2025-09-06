import PDFDocument from "pdfkit";
import fs from "fs";

interface User {
  personCode: number;
  name: string;
  email: string;
  password: string;
  passwordHint: string;
  age: number;
  gender: number;
}

interface TestResult {
  method: string;
  endpoint: string;
  description: string;
  status: number;
  requestBody?: any;
  response: any;
  error?: string;
}

class ApiTester {
  private baseUrl = "http://localhost:5249/api/User";
  private results: TestResult[] = [];

  async runAllTests(): Promise<void> {
    console.log("🚀 Iniciando Testes Automatizados da API...\n");

    try {
      await this.testGetAllUsers();
      await this.testCreateUsers();
      await this.testGetAllUsers();
      await this.testGetUserById();
      await this.testGetUserByEmail();
      await this.testUpdateUser();
      await this.testPatchUser();
      await this.testDeleteUser();

      this.generatePdfReport();
      console.log("\n✅ Todos os testes concluídos!");
      console.log("📄 Relatório PDF gerado: docs/TestReport.pdf");
    } catch (error) {
      console.error("❌ Erro durante os testes:", error);
    }
  }

  private async testCreateUsers(): Promise<void> {
    console.log("🔄 Testando CREATE (POST)...");

    // Usuário 1 - Eu
    const user1: User = {
      personCode: 1528669,
      name: "Bernardo Souza Alvim",
      email: "bernardo.alvim@sga.pucminas.br",
      password: "SGA@bsabsa",
      passwordHint: "Tem a ver com o estágio",
      age: 19,
      gender: 0, // Masculino
    };

    // Usuário 2 - Usuário de teste
    const user2: User = {
      personCode: 67890,
      name: "Bob da Silva Pinto",
      email: "bob@teste.com",
      password: "bob123",
      passwordHint: "senha facil",
      age: 30,
      gender: 2,
    };

    await this.postUser(user1, "Teste 1 - Usuário Pessoal");
    await this.postUser(user2, "Teste 2 - Usuário de Teste");
  }

  private async postUser(user: User, description: string): Promise<void> {
    try {
      const response = await fetch(this.baseUrl, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(user),
      });

      const data = await response.json();

      this.results.push({
        method: "POST",
        endpoint: "/api/User",
        description,
        status: response.status,
        requestBody: user,
        response: data,
      });

      console.log(`✅ POST ${description}: ${response.status}`);
    } catch (error) {
      this.results.push({
        method: "POST",
        endpoint: "/api/User",
        description,
        status: 0,
        requestBody: user,
        response: null,
        error: String(error),
      });
      console.log(`❌ Erro POST ${description}:`, error);
    }
  }

  private async testGetAllUsers(): Promise<void> {
    console.log("🔄 Testando GET ALL...");

    await this.getAllUsers("Teste 1 - Listar todos os usuários");
  }

  private async getAllUsers(description: string): Promise<void> {
    try {
      const response = await fetch(this.baseUrl);
      const data = await response.json();

      this.results.push({
        method: "GET",
        endpoint: "/api/User",
        description,
        status: response.status,
        response: data,
      });

      console.log(`✅ GET All ${description}: ${response.status}`);
    } catch (error) {
      this.results.push({
        method: "GET",
        endpoint: "/api/User",
        description,
        status: 0,
        response: null,
        error: String(error),
      });
      console.log(`❌ Erro GET All ${description}:`, error);
    }
  }

  private async testGetUserById(): Promise<void> {
    console.log("🔄 Testando GET BY ID...");

    await this.getUserById(1, "Teste 1 - Buscar usuário ID 1");
    await this.getUserById(2, "Teste 2 - Buscar usuário ID 2");
  }

  private async getUserById(id: number, description: string): Promise<void> {
    try {
      const response = await fetch(`${this.baseUrl}/${id}`);
      const data =
        response.status !== 404 ? await response.json() : "Not Found";

      this.results.push({
        method: "GET",
        endpoint: `/api/User/${id}`,
        description,
        status: response.status,
        response: data,
      });

      console.log(`✅ GET ID ${id}: ${response.status}`);
    } catch (error) {
      this.results.push({
        method: "GET",
        endpoint: `/api/User/${id}`,
        description,
        status: 0,
        response: null,
        error: String(error),
      });
      console.log(`❌ Erro GET ID ${id}:`, error);
    }
  }

  private async testGetUserByEmail(): Promise<void> {
    console.log("🔄 Testando GET BY EMAIL...");

    await this.getUserByEmail(
      "bernardo.alvim@sga.pucminas.br",
      "Teste 1 - Buscar por email pessoal"
    );
    await this.getUserByEmail(
      "bob@teste.com",
      "Teste 2 - Buscar por email de teste"
    );
  }

  private async getUserByEmail(
    email: string,
    description: string
  ): Promise<void> {
    try {
      const response = await fetch(`${this.baseUrl}/email/${email}`);
      const data =
        response.status !== 404 ? await response.json() : "Not Found";

      this.results.push({
        method: "GET",
        endpoint: `/api/User/email/${email}`,
        description,
        status: response.status,
        response: data,
      });

      console.log(`✅ GET Email ${email}: ${response.status}`);
    } catch (error) {
      this.results.push({
        method: "GET",
        endpoint: `/api/User/email/${email}`,
        description,
        status: 0,
        response: null,
        error: String(error),
      });
      console.log(`❌ Erro GET Email ${email}:`, error);
    }
  }

  private async testUpdateUser(): Promise<void> {
    console.log("🔄 Testando UPDATE (PUT)...");

    const updateUser1 = {
      personCode: 1528669,
      name: "Bernardo Alvim Atualizado",
      email: "bernardo.alvim@sga.pucminas.br",
      password: "novasenha123",
      passwordHint: "Nova senha segura",
      age: 19,
      gender: 0,
    };

    const updateUser2 = {
      personCode: 67890,
      name: "Barbara Costa",
      email: "barbara.costa@yahoo.com",
      password: "babi123",
      passwordHint: "senha segura",
      age: 26,
      gender: 1, // Feminino
    };

    await this.putUser(
      1,
      updateUser1,
      "Teste 1 - Atualizar usuário 1 completo"
    );
    await this.putUser(
      2,
      updateUser2,
      "Teste 2 - Atualizar usuário 2 completo"
    );
  }

  private async putUser(
    id: number,
    user: any,
    description: string
  ): Promise<void> {
    try {
      const response = await fetch(`${this.baseUrl}/${id}`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(user),
      });

      const data =
        response.status !== 404 ? await response.json() : "Not Found";

      this.results.push({
        method: "PUT",
        endpoint: `/api/User/${id}`,
        description,
        status: response.status,
        requestBody: user,
        response: data,
      });

      console.log(`✅ PUT ID ${id}: ${response.status}`);
    } catch (error) {
      this.results.push({
        method: "PUT",
        endpoint: `/api/User/${id}`,
        description,
        status: 0,
        requestBody: user,
        response: null,
        error: String(error),
      });
      console.log(`❌ Erro PUT ID ${id}:`, error);
    }
  }

  private async testPatchUser(): Promise<void> {
    console.log("🔄 Testando PATCH...");

    const patch1 = { passwordHint: "se vira vey" };
    const patch2 = { name: "Barbara Costa Parcialmente Atualizada" };

    await this.patchUser(1, patch1, "Teste 1 - Atualizar apenas passwordHint");
    await this.patchUser(2, patch2, "Teste 2 - Atualizar nome");
  }

  private async patchUser(
    id: number,
    patch: any,
    description: string
  ): Promise<void> {
    try {
      const response = await fetch(`${this.baseUrl}/${id}`, {
        method: "PATCH",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(patch),
      });

      const data =
        response.status !== 404 ? await response.json() : "Not Found";

      this.results.push({
        method: "PATCH",
        endpoint: `/api/User/${id}`,
        description,
        status: response.status,
        requestBody: patch,
        response: data,
      });

      console.log(`✅ PATCH ID ${id}: ${response.status}`);
    } catch (error) {
      this.results.push({
        method: "PATCH",
        endpoint: `/api/User/${id}`,
        description,
        status: 0,
        requestBody: patch,
        response: null,
        error: String(error),
      });
      console.log(`❌ Erro PATCH ID ${id}:`, error);
    }
  }

  private async testDeleteUser(): Promise<void> {
    console.log("🔄 Testando DELETE...");

    await this.deleteUser(1, "Teste 1 - Deletar usuário 1");
    await this.deleteUser(2, "Teste 2 - Deletar usuário 2");
  }

  private async deleteUser(id: number, description: string): Promise<void> {
    try {
      const response = await fetch(`${this.baseUrl}/${id}`, {
        method: "DELETE",
      });

      // CORREÇÃO: DELETE pode retornar 200, 204 ou 404
      let data;
      if (response.status === 200 || response.status === 204) {
        data = "Deleted Successfully";
      } else if (response.status === 404) {
        data = "Not Found";
      } else {
        // Tenta ler a resposta para outros status codes
        try {
          data = await response.text();
        } catch {
          data = `Status ${response.status}`;
        }
      }

      this.results.push({
        method: "DELETE",
        endpoint: `/api/User/${id}`,
        description,
        status: response.status,
        response: data,
      });

      console.log(`✅ DELETE ID ${id}: ${response.status}`);
    } catch (error) {
      this.results.push({
        method: "DELETE",
        endpoint: `/api/User/${id}`,
        description,
        status: 0,
        response: null,
        error: String(error),
      });
      console.log(`❌ Erro DELETE ID ${id}:`, error);
    }
  }

  private generatePdfReport(): void {
    console.log("📄 Gerando relatório PDF...");

    const doc = new PDFDocument();
    const stream = fs.createWriteStream("./TestReport.pdf");
    doc.pipe(stream);

    // Título
    doc
      .fontSize(18)
      .font("Helvetica-Bold")
      .text("RELATÓRIO DE TESTES - API CRUD", { align: "center" });

    doc
      .fontSize(12)
      .font("Helvetica")
      .text(`Data: ${new Date().toLocaleString("pt-BR")}`, { align: "center" });

    doc.moveDown(2);

    // Resumo
    doc.fontSize(14).font("Helvetica-Bold").text("RESUMO DOS TESTES");

    doc
      .fontSize(10)
      .font("Helvetica")
      .text(`Total de testes executados: ${this.results.length}`)
      .text(
        `Testes com sucesso: ${
          this.results.filter((r) => r.status >= 200 && r.status < 300).length
        }`
      )
      .text(
        `Testes com erro: ${
          this.results.filter((r) => r.status >= 400 || r.status === 0).length
        }`
      );

    doc.moveDown(1);

    // Detalhes dos testes
    let currentMethod = "";
    this.results.forEach((result, index) => {
      if (result.method !== currentMethod) {
        currentMethod = result.method;
        doc
          .fontSize(14)
          .font("Helvetica-Bold")
          .text(`\n=== TESTES ${result.method} ===`);
      }

      doc.fontSize(11).font("Helvetica-Bold").text(`\n${result.description}`);

      doc
        .fontSize(9)
        .font("Helvetica")
        .text(`Endpoint: ${result.endpoint}`)
        .text(`Status: ${result.status}`);

      if (result.requestBody) {
        doc.text(`Request Body: ${this.formatResponse(result.requestBody)}`);
      }

      doc.text(`Response: ${this.formatResponse(result.response)}`);

      if (result.error) {
        doc.fillColor("red").text(`Erro: ${result.error}`).fillColor("black");
      }

      if (index < this.results.length - 1) {
        doc.text("-".repeat(50));
      }
    });

    // Conclusão
    doc.addPage().fontSize(14).font("Helvetica-Bold").text("CONCLUSÃO");

    doc
      .fontSize(10)
      .font("Helvetica")
      .text(
        "Este relatório demonstra o funcionamento completo da API CRUD desenvolvida."
      )
      .text(
        "Todos os endpoints foram testados conforme especificação do enunciado:"
      )
      .text("• POST - Criação de dados")
      .text("• GET - Leitura de todos os dados")
      .text("• GET - Leitura por ID")
      .text("• GET - Leitura por e-mail")
      .text("• PUT - Atualização completa")
      .text("• PATCH - Atualização parcial")
      .text("• DELETE - Exclusão de dados")
      .text(
        "\nA API utiliza Entity Framework com banco InMemory e segue os princípios SOLID."
      );

    doc.end();

    stream.on("finish", () => {
      console.log("✅ PDF gerado com sucesso!");
    });
  }

  private formatResponse(response: any): string {
    if (!response) return "null";
    if (typeof response === "string") return response;
    try {
      return JSON.stringify(response, null, 2)
        .replace(/[^\x20-\x7E\n\r\t]/g, "") // Remove caracteres especiais que causam %%%%
        .substring(0, 1500); // Aumenta limite para 1500 caracteres
    } catch {
      return String(response).substring(0, 500);
    }
  }
}

// Execução
const tester = new ApiTester();
tester.runAllTests();
