# Case AeC - Gerenciador de Endereços

Este sistema foi desenvolvido com foco em alta disponibilidade e isolamento de ambiente, utilizando uma arquitetura moderna de containers e proxy reverso.

## 🛠️ Infraestrutura e Ambiente
* **Sistema Operacional:** VPS Linux Ubuntu 22.04 LTS
* **Containerização:** Docker e Docker Compose
* **Servidor Web & Proxy Reverso:** Nginx
* **Segurança e Domínio:** Cloudflare (SSL/TLS ponta a ponta)
* **Redirecionamento:** Regras de Origem (Origin Rules) no Cloudflare para redirecionamento de porta e Proxy Reverso.

---

## 🚀 Guia de Instalação (Desenvolvimento)

### 1. Instalação da estrutura padrão do .NET
Para reconstruir ou iniciar a estrutura MVC dentro da pasta existente, foi utilizado o comando:

```bash
dotnet new mvc -n CaseAeC --output .