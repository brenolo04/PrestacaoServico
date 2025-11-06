IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Usuario] (
    [Id] int NOT NULL IDENTITY,
    [Nome] varchar(100) NOT NULL,
    [Email] varchar(100) NOT NULL,
    [SenhaHash] varchar(256) NOT NULL,
    [Tipo] varchar(50) NOT NULL,
    CONSTRAINT [PK_Usuario] PRIMARY KEY ([Id])
);

CREATE TABLE [Prestador] (
    [Id] int NOT NULL IDENTITY,
    [Nome] VARCHAR(100) NOT NULL,
    [UsuarioId] int NOT NULL,
    [Profissao] VARCHAR(100) NOT NULL,
    CONSTRAINT [PK_Prestador] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Prestador_Usuario_UsuarioId] FOREIGN KEY ([UsuarioId]) REFERENCES [Usuario] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [Solicitante] (
    [Id] int NOT NULL IDENTITY,
    [Nome] VARCHAR(100) NOT NULL,
    [UsuarioId] int NOT NULL,
    CONSTRAINT [PK_Solicitante] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Solicitante_Usuario_UsuarioId] FOREIGN KEY ([UsuarioId]) REFERENCES [Usuario] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [Servicos] (
    [Id] int NOT NULL IDENTITY,
    [Nome] varchar(100) NOT NULL,
    [Descricao] varchar(1000) NOT NULL,
    [Valor] decimal(10,2) NOT NULL DEFAULT 0.1,
    [PrestadorId] int NOT NULL,
    CONSTRAINT [PK_Servicos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Servico_UsuarioId] FOREIGN KEY ([PrestadorId]) REFERENCES [Prestador] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [OrdemServico] (
    [Id] int NOT NULL IDENTITY,
    [ServicoId] int NOT NULL,
    [SolicitanteId] int NOT NULL,
    [PrestadorId] int NOT NULL,
    [DataSolicitacao] datetime NOT NULL,
    [Status] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_OrdemServico] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_OrdemServico_PrestadorId] FOREIGN KEY ([PrestadorId]) REFERENCES [Prestador] ([Id]),
    CONSTRAINT [FK_OrdemServico_ServicoId] FOREIGN KEY ([ServicoId]) REFERENCES [Servicos] ([Id]),
    CONSTRAINT [FK_OrdemServico_SolicitanteId] FOREIGN KEY ([SolicitanteId]) REFERENCES [Solicitante] ([Id])
);

CREATE INDEX [IX_OrdemServico_PrestadorId] ON [OrdemServico] ([PrestadorId]);

CREATE INDEX [IX_OrdemServico_ServicoId] ON [OrdemServico] ([ServicoId]);

CREATE INDEX [IX_OrdemServico_SolicitanteId] ON [OrdemServico] ([SolicitanteId]);

CREATE INDEX [IX_Prestador_UsuarioId] ON [Prestador] ([UsuarioId]);

CREATE INDEX [IX_Servicos_PrestadorId] ON [Servicos] ([PrestadorId]);

CREATE INDEX [IX_Solicitante_UsuarioId] ON [Solicitante] ([UsuarioId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251029010135_PrimeiraMigracao', N'9.0.10');

COMMIT;
GO

