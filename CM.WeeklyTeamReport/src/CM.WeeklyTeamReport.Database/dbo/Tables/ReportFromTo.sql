CREATE TABLE [dbo].[ReportFromTo] (
    [TeamMemberFrom] INT NOT NULL,
    [TeamMemberTo]   INT NOT NULL,
    CONSTRAINT [FK_ReportFromTo_TeamMembers] FOREIGN KEY ([TeamMemberFrom]) REFERENCES [dbo].[TeamMembers] ([TeamMemberId]),
    CONSTRAINT [FK_ReportFromTo_TeamMembers1] FOREIGN KEY ([TeamMemberTo]) REFERENCES [dbo].[TeamMembers] ([TeamMemberId])
);

