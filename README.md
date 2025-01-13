# CadastroPessoas

Este é um projeto de um sistema de cadastro de pessoas, desenvolvido utilizando o framework ASP.NET Core. O projeto permite gerenciar informações de pessoas, como nome, telefone, CPF e endereços. A aplicação também inclui funcionalidades para adicionar, editar e excluir endereços, e um recurso de busca automática de endereços via CEP através da API pública ViaCEP.

## Funcionalidades

- **Cadastro de Pessoas**: Cadastro de informações básicas (nome, telefone, CPF) com validação de dados.
- **Cadastro de Endereços**: Atribuição de múltiplos endereços para uma pessoa, com integração à API ViaCEP para preenchimento automático de endereço a partir do CEP.
- **CRUD Completo**: Funções para criar, visualizar, editar e excluir tanto pessoas quanto endereços.
- **Interface Responsiva**: Design responsivo utilizando Bootstrap 5 para funcionar bem em dispositivos móveis e desktops.

## Tecnologias Utilizadas

- ASP.NET Core 5
- C#
- Bootstrap 5
- API pública ViaCEP para consulta de endereços via CEP.

## Como Executar o Projeto

### Pré-requisitos

Antes de executar o projeto, você precisa ter o **.NET 5 SDK** instalado no seu computador. Você pode fazer o download no site oficial do .NET.

### Passos para Execução

1. **Clone este repositório para o seu computador**:
    ```bash
    git clone https://github.com/seu-usuario/CadastroPessoas.git
    ```

2. **Navegue até o diretório do projeto**:
    ```bash
    cd CadastroPessoas
    ```

3. **Restaure as dependências do projeto**:
    ```bash
    dotnet restore
    ```

4. **Execute o projeto**:
    ```bash
    dotnet run
    ```

5. **Acesse a aplicação no navegador**:
    Abra o navegador e acesse `https://localhost:5001`.

## Estrutura do Projeto

- **Controllers**:
  - `PessoasController.cs`: Controla as ações relacionadas ao cadastro de pessoas.
  - `EnderecosController.cs`: Controla as ações relacionadas ao cadastro de endereços.

- **Models**:
  - `Pessoa.cs`: Define as propriedades de uma pessoa (nome, telefone, CPF e endereços).
  - `Endereco.cs`: Define as propriedades de um endereço (descrição, CEP, cidade e estado).

- **Views**:
  - `Index.cshtml`: Página inicial que exibe a lista de pessoas cadastradas.
  - `Edit.cshtml`: Página de edição de uma pessoa, onde é possível editar os dados de pessoas e endereços.

- **JavaScript**:
  - Um script JavaScript que consulta a API ViaCEP e preenche automaticamente os campos de endereço ao digitar o CEP.

## Licença

Este projeto está sob a licença MIT. Veja o arquivo LICENSE para mais informações.
