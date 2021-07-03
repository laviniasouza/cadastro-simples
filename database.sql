CREATE DATABASE Cadastro

USE Cadastro 

CREATE TABLE [dbo].[tb_Cadastro]
(
	[idCliente] INT NOT NULL PRIMARY KEY IDENTITY,
	[nome] varchar(50),
	[idade] int,
	[endereco] varchar(60),
	[numero] int,
	[bairro] varchar(30)
)