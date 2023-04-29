USE [persona_db]
/****** Object:  Table [dbo].[persona]    Script Date: 4/24/2023 4:35:12 PM ******/
CREATE TABLE [dbo].[persona] (
    [cc]       INT          NOT NULL,
    [nombre]   VARCHAR (45) NOT NULL,
    [apellido] VARCHAR (45) NOT NULL,
    [genero]   VARCHAR (1)	NOT NULL,
    [edad]     INT          NULL,
    CONSTRAINT [PK_persona] PRIMARY KEY CLUSTERED ([cc] ASC), 
    CONSTRAINT [CK_persona_genero] CHECK ([genero] LIKE 'M' OR [genero] LIKE 'F')
);

/****** Object:  Table [dbo].[profesion]    Script Date: 4/24/2023 4:35:12 PM ******/
CREATE TABLE [dbo].[profesion] (
    [id]  INT          NOT NULL,
    [nom] VARCHAR (60) NOT NULL,
    [des] TEXT         NULL,
    CONSTRAINT [PK_profesion] PRIMARY KEY CLUSTERED ([id] ASC)
);


/****** Object:  Table [dbo].[telefono]    Script Date: 4/24/2023 4:35:12 PM ******/
CREATE TABLE [dbo].[telefono] (
    [num]    VARCHAR (15) NOT NULL,
    [oper]   VARCHAR (45) NOT NULL,
    [duenio] INT          NOT NULL,
    CONSTRAINT [PK_telefono] PRIMARY KEY CLUSTERED ([num] ASC), 
    CONSTRAINT [FK_telefono_persona] FOREIGN KEY ([duenio]) REFERENCES [dbo].[persona] ([cc])
);

/****** Object:  Table [dbo].[estudios]    Script Date: 4/24/2023 4:35:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[estudios] (
    [id_prof] INT          NOT NULL,
    [cc_per]  INT          NOT NULL,
    [fecha]   DATE         NULL,
    [univer]  VARCHAR (50) NULL,
    CONSTRAINT [PK_estudios] PRIMARY KEY ([cc_per] ASC, [id_prof] ASC), 
    CONSTRAINT [FK_estudios_persona] FOREIGN KEY ([cc_per]) REFERENCES [dbo].[persona] ([cc]),
    CONSTRAINT [FK_estudios_profesion] FOREIGN KEY ([id_prof]) REFERENCES [dbo].[profesion] ([id])
);