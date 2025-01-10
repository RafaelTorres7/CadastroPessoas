CREATE TABLE Enderecos (
    EnderecoId INT PRIMARY KEY IDENTITY(1,1),
    PessoaId INT FOREIGN KEY REFERENCES Pessoas(PessoaId),
    Logradouro NVARCHAR(200),
    Cidade NVARCHAR(100),
    Estado NVARCHAR(50)
);
