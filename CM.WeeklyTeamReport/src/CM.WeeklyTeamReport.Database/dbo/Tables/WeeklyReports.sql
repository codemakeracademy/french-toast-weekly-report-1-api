CREATE TABLE [dbo].[WeeklyReports] (
    [WeeklyReportId]      INT            IDENTITY (1, 1) NOT NULL,
    [StartDate]           DATE           CONSTRAINT [DF_WeeklyReports_StartDate] DEFAULT (getdate()) NOT NULL,
    [EndDate]             DATE           CONSTRAINT [DF_WeeklyReports_EndDate] DEFAULT (getdate()) NOT NULL,
    [Year]                NCHAR (4)      NOT NULL,
    [MoraleValueId]       INT            NULL,
    [StressValueId]       INT            NULL,
    [WorkloadValueId]     INT            NULL,
    [MoraleComment]       NVARCHAR (MAX) NULL,
    [StressComment]       NVARCHAR (MAX) NULL,
    [WorkloadComment]     NVARCHAR (MAX) NULL,
    [WeekHighComment]     NVARCHAR (MAX) NOT NULL,
    [WeekLowComment]      NVARCHAR (MAX) NOT NULL,
    [AnythingElseComment] NVARCHAR (MAX) NULL,
    [TeamMemberId]        INT            NOT NULL,
    CONSTRAINT [PK_WeeklyReports] PRIMARY KEY CLUSTERED ([WeeklyReportId] ASC),
    CONSTRAINT [FK_WeeklyReports_Morales] FOREIGN KEY ([MoraleValueId]) REFERENCES [dbo].[Morales] ([MoraleId]),
    CONSTRAINT [FK_WeeklyReports_Morales1] FOREIGN KEY ([StressValueId]) REFERENCES [dbo].[Morales] ([MoraleId]),
    CONSTRAINT [FK_WeeklyReports_Morales2] FOREIGN KEY ([WorkloadValueId]) REFERENCES [dbo].[Morales] ([MoraleId]),
    CONSTRAINT [FK_WeeklyReports_TeamMembers] FOREIGN KEY ([TeamMemberId]) REFERENCES [dbo].[TeamMembers] ([TeamMemberId])
);



