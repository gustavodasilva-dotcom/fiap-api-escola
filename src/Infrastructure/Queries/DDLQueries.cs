namespace Fiap.Api.Escola.Infrastructure.Queries;

internal static class DDLQueries
{
    internal const string QueryCreateAlunoTable = @"
		DROP TABLE IF EXISTS [dbo].[aluno];
		
		CREATE TABLE [dbo].[aluno]
		(
			 [id]		INT				NOT NULL IDENTITY(1, 1)
			,[nome]		VARCHAR(255)	NOT NULL
			,[usuario]	VARCHAR(45)		NOT NULL
			,[senha]	CHAR(60)		NOT NULL
		
			CONSTRAINT [PK_aluno] PRIMARY KEY([id])
		);";

	internal const string QueryCreateTurmaTable = @"
		DROP TABLE IF EXISTS [dbo].[Turma];
		
		CREATE TABLE [dbo].[turma]
		(
			 [id]		INT			NOT NULL IDENTITY(1, 1)
			,[curso_id]	INT			NOT NULL
			,[turma]	VARCHAR(45)	NOT NULL
			,[ano]		INT			NOT NULL
		
			CONSTRAINT [PK_turma] PRIMARY KEY([id])
		);";

	internal const string QueryCreateAlunoTurmaTable = @"
		DROP TABLE IF EXISTS [dbo].[aluno_turma];
		
		CREATE TABLE [dbo].[aluno_turma]
		(
			 [aluno_id]	INT	NOT NULL
			,[turma_id]	INT	NOT NULL
		
			CONSTRAINT [FK_aluno_turma_aluno_id] FOREIGN KEY ([aluno_id])
			REFERENCES [dbo].[aluno]([id]),
		
			CONSTRAINT [FK_aluno_turma_turma_id] FOREIGN KEY ([turma_id])
			REFERENCES [dbo].[turma]([id])
		);";
}
