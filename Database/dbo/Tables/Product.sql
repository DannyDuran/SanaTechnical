CREATE TABLE [dbo].[Product] (
    [Id]     INT            IDENTITY (1, 1) NOT NULL,
    [Number] VARCHAR (10)   NOT NULL,
    [Name]   VARCHAR (30)   NOT NULL,
    [Price]  NUMERIC (6, 2) NOT NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([Id] ASC)
);

