# Sobre o Projeto

O sistema de cadastro é uma API que permite o gerenciamento de cadastros de pessoas e empresas. Este sistema foi projetado para ser altamente flexível e escalável. A API suporta operações completas de CRUD (Create, Read, Update, Delete) para entidades como Cadastro e Pessoa, além de oferecer funcionalidades avançadas de validação e recuperação de endereços via CEP.

## Técnicas Utilizadas

- Clean Architecture
- Domain-Driven Design (DDD)
- Princípios SOLID

## Build Status
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=marcelinho_sistemadecadastro&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=marcelinho_sistemadecadastro)
[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=marcelinho_sistemadecadastro&metric=bugs)](https://sonarcloud.io/summary/new_code?id=marcelinho_sistemadecadastro)
[![Code Smells](https://sonarcloud.io/api/project_badges/measure?project=marcelinho_sistemadecadastro&metric=code_smells)](https://sonarcloud.io/summary/new_code?id=marcelinho_sistemadecadastro)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=marcelinho_sistemadecadastro&metric=coverage)](https://sonarcloud.io/summary/new_code?id=marcelinho_sistemadecadastro)
[![Duplicated Lines (%)](https://sonarcloud.io/api/project_badges/measure?project=marcelinho_sistemadecadastro&metric=duplicated_lines_density)](https://sonarcloud.io/summary/new_code?id=marcelinho_sistemadecadastro)

## Tecnologias Utilizadas

![Ubuntu](https://img.shields.io/badge/Ubuntu-E95420?style=for-the-badge&logo=ubuntu&logoColor=white)
![Visual Studio Code](https://img.shields.io/badge/Visual%20Studio%20Code-0078d7.svg?style=for-the-badge&logo=visual-studio-code&logoColor=white)
![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=csharp&logoColor=white)
![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![MicrosoftSQLServer](https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white)
![Swagger](https://camo.githubusercontent.com/6e4dd9644d5327ffad6433ecb2f4c0a8f41531fcfe142ae36d7e1cb162774fc3/68747470733a2f2f696d672e736869656c64732e696f2f62616467652f537761676765722d3230354533423f7374796c653d666f722d7468652d6261646765266c6f676f3d73776167676572266c6f676f436f6c6f723d7768697465)
![Postman](https://img.shields.io/badge/Postman-FF6C37?style=for-the-badge&logo=postman&logoColor=white)


## Features

### CepServices:

- [x] Recuperar endereço por CEP.

### Cadastro:

- [x] Registrar.
- [ ] Recuperar todos.
- [ ] Recuperar por Id.
- [ ] Atualizar.
- [ ] Deletar.

### Pessoa:

- [x] Registrar.
- [ ] Recuperar todos.
- [ ] Recuperar por Id.
- [ ] Atualizar.
- [ ] Deletar.

## Estrutura do Projeto

Abaixo está um diagrama simplificado representando as principais entidades e seus relacionamentos dentro do sistema.

![Diagrama de Entidades](images/diagrama.jpg)

## Instalação

1. Clone o repositório: `git clone https://github.com/seu-usuario/sistemadecadastro.git`
2. Navegue até o diretório do projeto: `cd sistemadecadastro`
3. Restaure as dependências: `dotnet restore`
4. Configure a string de conexão com o banco de dados no arquivo `appsettings.json`.
5. Execute as migrações: `dotnet ef database update`
6. Inicie o projeto: `dotnet run`

## Uso

Após iniciar o projeto, você pode acessar a documentação da API através do Swagger, disponível em `https://localhost:7000/swagger`.

### Exemplos de Requisição

#### Registrar Cadastro

```json
{
  "email": "cadastro@exemplo.com",
  "nomeFantasia": "Nome Fantasia Cadastro",
  "sobrenomeSocial": "Sobrenome Social Cadastro",
  "empresa": true,
  "credencial": {
    "bloqueada": false,
    "expirada": "2024-12-31T23:59:59Z",
    "senha": "senha123"
  },
  "inscrito": {
    "assinante": true,
    "associado": false,
    "senha": "senha123"
  },
  "parceiro": {
    "cliente": true,
    "fornecedor": false,
    "prestador": false,
    "colaborador": true
  },
  "documento": {
    "numero": "123456789",
    "orgaoEmissor": "SSP",
    "estadoEmissor": "SP",
    "dataValidade": "2030-12-31T00:00:00Z"
  },
  "identificador": {
    "empresa": 1,
    "identificador": "Identificador",
    "tipo": 0
  }
}
```

#### Registrar Pessoa

```json
{
  "cpf": "12345678901",
  "cnpj": "12345678000195",
  "nome": "João Silva",
  "nomeFantasia": "João Silva ME",
  "email": "joao.silva@exemplo.com",
  "nascimento": "1980-01-01T00:00:00Z",
  "token": 12345,
  "domicilios": [
    {
      "tipo": 0,
      "endereco": {
        "cep": "16016020",
        "numero": "123",
        "complemento": "Apt 1",
        "pontoReferencia": "Próximo ao mercado"
      }
    },
    {
      "tipo": 1,
      "endereco": {
        "cep": "07241035",
        "numero": "456",
        "complemento": "Sala 5",
        "pontoReferencia": "Próximo ao shopping"
      }
    }
  ],
  "telefone": {
    "numero": 11987654321,
    "celular": true,
    "whatsapp": true,
   

 "telegram": false
  },
  "cadastro": {
    "email": "cadastro@exemplo.com",
    "nomeFantasia": "Nome Fantasia Cadastro",
    "sobrenomeSocial": "Sobrenome Social Cadastro",
    "empresa": true,
    "credencial": {
      "bloqueada": false,
      "expirada": "2024-12-31T23:59:59Z",
      "senha": "senha123"
    },
    "inscrito": {
      "assinante": true,
      "associado": false,
      "senha": "senha123"
    },
    "parceiro": {
      "cliente": true,
      "fornecedor": false,
      "prestador": false,
      "colaborador": true
    },
    "documento": {
      "numero": "123456789",
      "orgaoEmissor": "SSP",
      "estadoEmissor": "SP",
      "dataValidade": "2030-12-31T00:00:00Z"
    },
    "identificador": {
      "empresa": 1,
      "identificador": "Identificador",
      "tipo": 0
    }
  }
}
```

## Autores

Estes projetos de exemplo foram criados para fins educacionais. [Marcelo](https://github.com/Mmarcelinho) é responsável pela criação e manutenção destes projetos.

## Licença

Este projetos não possuem uma licença específica e são fornecidos apenas para fins de aprendizado e demonstração.
