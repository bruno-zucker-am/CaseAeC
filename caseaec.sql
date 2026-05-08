-- 1. Cria o Banco de Dados
 CREATE DATABASE CaseAeCDB;

-- 2. Usa o banco criado
 USE CaseAeCDB;

-- 3. Cria a tabela de Usuários
CREATE TABLE Usuarios (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome NVARCHAR(100) NOT NULL,
    Login NVARCHAR(50) NOT NULL UNIQUE,
    Senha NVARCHAR(100) NOT NULL
);

-- 4. Cria a tabela de Endereços
CREATE TABLE Enderecos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Cep NVARCHAR(10) NOT NULL,
    Logradouro NVARCHAR(200) NOT NULL,
    Numero NVARCHAR(20) NOT NULL,
    Complemento NVARCHAR(100) NULL,
    Bairro NVARCHAR(100) NOT NULL,
    Cidade NVARCHAR(100) NOT NULL,
    Uf NVARCHAR(2) NOT NULL,
    UsuarioId INT NOT NULL,
    CONSTRAINT FK_Enderecos_Usuarios FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id) ON DELETE CASCADE
);