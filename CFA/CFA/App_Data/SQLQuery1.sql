CREATE TABLE [dbo].[InfoFirm] (
    [Id]      INT            IDENTITY (1, 1) NOT NULL,
    [phone]   NVARCHAR (255) NOT NULL,
    [email]   NVARCHAR (255) NOT NULL,
    [name]    NVARCHAR (255) NOT NULL,
    [address] NVARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[OrdersExecute] (
    [Id]      INT            IDENTITY (1, 1) NOT NULL,
    [message] NVARCHAR (255) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[TypeHardware] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [name] NVARCHAR (255) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


CREATE TABLE [dbo].[BrandHardware] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [name] NVARCHAR (255) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[OrdersExecute] (
    [Id]      INT         IDENTITY (1, 1) NOT NULL,
    [message] NVARCHAR(100) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[InfoHardware] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [name]          NVARCHAR (255) NOT NULL,
    [numbAvailable] INT            NULL,
    [numbOrder]     INT            NULL,
    [cost]          INT            NULL,
    [fname]         NVARCHAR (255) NULL,
    [typeId]        INT            NOT NULL,
    [BrandId]       INT            NULL,
    [describe]      NVARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InfoHardware_TypeHardware] FOREIGN KEY ([typeId]) REFERENCES [dbo].[TypeHardware] ([Id]),
    CONSTRAINT [FK_InfoHardware_BrandHardware] FOREIGN KEY ([BrandId]) REFERENCES [dbo].[BrandHardware] ([Id])
);

CREATE TABLE [dbo].[OrdersFirm] (
    [Id]            INT      IDENTITY (1, 1) NOT NULL,
    [dateStart]     DATETIME NULL,
    [numb]          INT      NOT NULL,
    [hardwareId]    INT      NOT NULL,
    [firmId]        INT      NOT NULL,
    [executeId]     INT      NOT NULL,
    [dateExecution] DATETIME NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OrdersFirm_OrdersExecute] FOREIGN KEY ([executeId]) REFERENCES [dbo].[OrdersExecute] ([Id]),
    CONSTRAINT [FK_OrdersFirm_InfoHardware] FOREIGN KEY ([hardwareId]) REFERENCES [dbo].[InfoHardware] ([Id]),
    CONSTRAINT [FK_OrdersFirm_InfoFirm] FOREIGN KEY ([firmId]) REFERENCES [dbo].[InfoFirm] ([Id])
);


CREATE TABLE [dbo].[OrdersUser] (
    [Id]            INT      IDENTITY (1, 1) NOT NULL,
    [dateStart]     DATETIME NULL,
    [numb]          INT      NOT NULL,
    [HardwareId]    INT      NOT NULL,
    [UserId]        INT      NOT NULL,
    [ExecuteId]     INT      NOT NULL,
    [dateExecution] DATETIME NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OrdersUser_UserProfile] FOREIGN KEY ([UserId]) REFERENCES [dbo].[UserProfile] ([UserId]),
    CONSTRAINT [FK_OrdersUser_OrdersExecute] FOREIGN KEY ([ExecuteId]) REFERENCES [dbo].[OrdersExecute] ([Id]),
    CONSTRAINT [FK_OrdersUser_InfoHardware] FOREIGN KEY ([HardwareId]) REFERENCES [dbo].[InfoHardware] ([Id])
);


CREATE TABLE [dbo].[UserProfile] (
    [UserId]   INT            IDENTITY (1, 1) NOT NULL,
    [UserName] NVARCHAR (MAX) NULL,
    [phone]    NVARCHAR (100) NULL,
    [email]    NVARCHAR (100) NULL,
    [address]  NVARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC)
);


// очистка таблицы и сброс автоинкремента
TRUNCATE TABLE OrdersFirm;
DBCC CHECKIDENT (OrdersFirm, RESEED, 1);



// Ключи для добавления в сгенерированную Entity UserProfile & webpages_Roles
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

  // Копируем из AccountModel вставляем над полями класса
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]

-------Nuget----
Bootsrap
Install-Package Microsoft.AspNet.Web.Optimization
