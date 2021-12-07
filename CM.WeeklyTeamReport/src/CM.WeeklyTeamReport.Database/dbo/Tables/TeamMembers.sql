CREATE TABLE [dbo].[TeamMembers] (
    [TeamMemberId] INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]    NVARCHAR (20)  NOT NULL,
    [LastName]     NVARCHAR (20)  NOT NULL,
    [Title]        NVARCHAR (20)  NULL,
    [InviteLink]   NVARCHAR (200) NULL,
    [Mail]         NVARCHAR (100) NOT NULL,
    [CompanyId]    INT            NOT NULL,
    CONSTRAINT [PK_TeamMembers] PRIMARY KEY CLUSTERED ([TeamMemberId] ASC),
    CONSTRAINT [FK_TeamMembers_Companies] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Companies] ([CompanyId]),
    CONSTRAINT [IX_TeamMembers_Mail] UNIQUE NONCLUSTERED ([Mail] ASC)
);

