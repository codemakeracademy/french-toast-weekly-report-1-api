CREATE TABLE [dbo].[Morales] (
    [MoraleId]   INT           NOT NULL,
    [MoraleName] NVARCHAR (10) NOT NULL,
    CONSTRAINT [PK_Morales] PRIMARY KEY CLUSTERED ([MoraleId] ASC),
    CONSTRAINT [IX_Morales_MoraleName] UNIQUE NONCLUSTERED ([MoraleName] ASC)
);

