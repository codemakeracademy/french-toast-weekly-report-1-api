CREATE TABLE [dbo].[ReportFromReportTo] (
    [ReportFromId] INT NOT NULL,
    [ReportToId]   INT NOT NULL,
    CONSTRAINT [FK_ReportFromReportTo_TeamMembers] FOREIGN KEY ([ReportFromId]) REFERENCES [dbo].[TeamMembers] ([TeamMemberId]),
    CONSTRAINT [FK_ReportFromReportTo_TeamMembers1] FOREIGN KEY ([ReportToId]) REFERENCES [dbo].[TeamMembers] ([TeamMemberId])
);

