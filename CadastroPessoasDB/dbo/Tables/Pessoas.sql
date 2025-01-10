CREATE TABLE [dbo].[Pessoas] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Nome]     NVARCHAR (100) NULL,
    [Telefone] NVARCHAR (20)  NULL,
    [Cpf]      NVARCHAR (20)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

