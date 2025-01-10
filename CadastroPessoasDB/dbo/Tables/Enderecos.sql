CREATE TABLE [dbo].[Enderecos] (
    [Id]                INT            NOT NULL,
    [PessoaId]          INT            NOT NULL,
    [EnderecoDescricao] NVARCHAR (200) NOT NULL,
    [Cep]               NVARCHAR (10)  NOT NULL,
    [Cidade]            NVARCHAR (100) NOT NULL,
    [Estado]            NVARCHAR (50)  NOT NULL,
    CONSTRAINT [PK_Enderecos] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Enderecos_Pessoas] FOREIGN KEY ([PessoaId]) REFERENCES [dbo].[Pessoas] ([Id])
);

