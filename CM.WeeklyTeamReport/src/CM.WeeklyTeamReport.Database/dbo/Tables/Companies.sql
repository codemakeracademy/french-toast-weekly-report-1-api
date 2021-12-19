CREATE TABLE [dbo].[Companies] (
    [CompanyId]   INT            IDENTITY (1, 1) NOT NULL,
    [CompanyName] NVARCHAR (100) NOT NULL,
    [JoinDate]    DATE           NOT NULL,
    CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED ([CompanyId] ASC),
    CONSTRAINT [IX_Companies_CompanyName] UNIQUE NONCLUSTERED ([CompanyName] ASC)
);



