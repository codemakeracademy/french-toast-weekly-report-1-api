CREATE TABLE [dbo].[WeeklyReports] (
    [WeeklyReportId]      INT            IDENTITY (1, 1) NOT NULL,
    [DateFrom]            DATE           CONSTRAINT [DF_WeeklyReports_StartDate] DEFAULT (getdate()) NOT NULL,
    [DateTo]              DATE           NOT NULL,
    [MoraleValueId]       INT            NOT NULL,
    [StressValueId]       INT            NOT NULL,
    [WorkloadValueId]     INT            NOT NULL,
    [MoraleComment]       NVARCHAR (600) NULL,
    [StressComment]       NVARCHAR (600) NULL,
    [WorkloadComment]     NVARCHAR (600) NULL,
    [WeekHighComment]     NVARCHAR (600) NOT NULL,
    [WeekLowComment]      NVARCHAR (600) NOT NULL,
    [AnythingElseComment] NVARCHAR (400) NULL,
    [TeamMemberId]        INT            NOT NULL,
    CONSTRAINT [PK_WeeklyReports] PRIMARY KEY CLUSTERED ([WeeklyReportId] ASC),
    CONSTRAINT [FK_WeeklyReports_Morales] FOREIGN KEY ([MoraleValueId]) REFERENCES [dbo].[Morales] ([MoraleId]),
    CONSTRAINT [FK_WeeklyReports_Morales1] FOREIGN KEY ([StressValueId]) REFERENCES [dbo].[Morales] ([MoraleId]),
    CONSTRAINT [FK_WeeklyReports_Morales2] FOREIGN KEY ([WorkloadValueId]) REFERENCES [dbo].[Morales] ([MoraleId]),
    CONSTRAINT [FK_WeeklyReports_TeamMembers] FOREIGN KEY ([TeamMemberId]) REFERENCES [dbo].[TeamMembers] ([TeamMemberId])
);







