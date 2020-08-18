# AppTest

Utworzenie tabeli

CREATE TABLE [dbo].[Campaigns] (
    [Id]          INT             IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50)   NULL,
    [Description] NVARCHAR (50)   NULL,
    [Leader]      NVARCHAR (50)   NULL,
    [Cost]        DECIMAL (18, 2) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

Kopia bazy w katalogu głównym.
