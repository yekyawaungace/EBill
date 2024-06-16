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
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231102034045_initial')
BEGIN
    CREATE TABLE [Applications] (
        [Id] uniqueidentifier NOT NULL,
        [OrderNo] nvarchar(max) NOT NULL,
        [NoofPeople] int NOT NULL,
        [Type] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [ReceivedDate] datetime2 NOT NULL,
        [Merchant] nvarchar(max) NOT NULL,
        [ContactPhone] nvarchar(max) NOT NULL,
        [OverSeaOrDomestic] int NULL,
        [FromDestination] nvarchar(max) NOT NULL,
        [ToDestination] nvarchar(max) NOT NULL,
        [PremiumFee] float NULL,
        [TransitionID] nvarchar(max) NOT NULL,
        [Address] nvarchar(max) NOT NULL,
        [StartDate] datetime2 NOT NULL,
        [EndDate] datetime2 NOT NULL,
        [SecondContactPerson] nvarchar(max) NOT NULL,
        [SecondContactPhone] nvarchar(max) NOT NULL,
        [PaymentStatus] int NOT NULL,
        [IsActive] bit NOT NULL,
        [IsDeleted] bit NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [CreatedStaffNo] nvarchar(max) NOT NULL,
        [UpdatedDate] datetime2 NULL,
        [UpdatedStaffNo] nvarchar(max) NULL,
        CONSTRAINT [PK_Applications] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231102034045_initial')
BEGIN
    CREATE TABLE [AuditTrails] (
        [Id] uniqueidentifier NOT NULL,
        [ApplicationId] uniqueidentifier NOT NULL,
        [TDateTime] datetime2 NOT NULL,
        [Remark] nvarchar(max) NOT NULL,
        [Status] nvarchar(max) NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [CreatedStaffNo] nvarchar(max) NOT NULL,
        [UpdatedDate] datetime2 NULL,
        [UpdatedStaffNo] nvarchar(max) NULL,
        CONSTRAINT [PK_AuditTrails] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231102034045_initial')
BEGIN
    CREATE TABLE [Country] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [IsActive] bit NULL,
        [IsDeleted] bit NULL,
        [CreatedDate] datetime2 NOT NULL,
        [CreatedStaffNo] nvarchar(max) NOT NULL,
        [UpdatedDate] datetime2 NULL,
        [UpdatedStaffNo] nvarchar(max) NULL,
        CONSTRAINT [PK_Country] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231102034045_initial')
BEGIN
    CREATE TABLE [Menus] (
        [Id] uniqueidentifier NOT NULL,
        [MenuName] nvarchar(max) NOT NULL,
        [MenuNameTrim] nvarchar(max) NOT NULL,
        [ParentMenuID] uniqueidentifier NULL,
        [IsActive] bit NOT NULL,
        [IsDeleted] bit NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [CreatedStaffNo] nvarchar(max) NOT NULL,
        [UpdatedDate] datetime2 NULL,
        [UpdatedStaffNo] nvarchar(max) NULL,
        CONSTRAINT [PK_Menus] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231102034045_initial')
BEGIN
    CREATE TABLE [MenusInRoles] (
        [Id] uniqueidentifier NOT NULL,
        [RolesID] uniqueidentifier NOT NULL,
        [MenuID] uniqueidentifier NOT NULL,
        [IsCanView] bit NOT NULL,
        [IsCanAdd] bit NOT NULL,
        [IsCanUpdate] bit NOT NULL,
        [IsCanDelete] bit NOT NULL,
        [IsCanAdmin] bit NULL,
        [IsActive] bit NOT NULL,
        [IsDeleted] bit NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [CreatedStaffNo] nvarchar(max) NOT NULL,
        [UpdatedDate] datetime2 NULL,
        [UpdatedStaffNo] nvarchar(max) NULL,
        CONSTRAINT [PK_MenusInRoles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231102034045_initial')
BEGIN
    CREATE TABLE [Merchants] (
        [Id] uniqueidentifier NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        [PhoneNo] nvarchar(max) NOT NULL,
        [Address] nvarchar(max) NOT NULL,
        [QRCode] nvarchar(max) NOT NULL,
        [IsActive] bit NOT NULL,
        [IsDeleted] bit NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [CreatedStaffNo] nvarchar(max) NOT NULL,
        [UpdatedDate] datetime2 NULL,
        [UpdatedStaffNo] nvarchar(max) NULL,
        CONSTRAINT [PK_Merchants] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231102034045_initial')
BEGIN
    CREATE TABLE [NRCTypes] (
        [Id] uniqueidentifier NOT NULL,
        [TypeEN] nvarchar(max) NOT NULL,
        [TypeMM] nvarchar(max) NOT NULL,
        [IsActive] bit NULL,
        [IsDeleted] bit NULL,
        [CreatedDate] datetime2 NOT NULL,
        [CreatedStaffNo] nvarchar(max) NOT NULL,
        [UpdatedDate] datetime2 NULL,
        [UpdatedStaffNo] nvarchar(max) NULL,
        CONSTRAINT [PK_NRCTypes] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231102034045_initial')
BEGIN
    CREATE TABLE [Permission] (
        [Id] uniqueidentifier NOT NULL,
        [staffno] nvarchar(max) NOT NULL,
        [roleid] uniqueidentifier NOT NULL,
        [isdeleted] bit NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [CreatedStaffNo] nvarchar(max) NOT NULL,
        [UpdatedDate] datetime2 NULL,
        [UpdatedStaffNo] nvarchar(max) NULL,
        CONSTRAINT [PK_Permission] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231102034045_initial')
BEGIN
    CREATE TABLE [PremiumFees] (
        [Id] uniqueidentifier NOT NULL,
        [Duration] int NULL,
        [Fee] float NULL,
        [IsActive] bit NULL,
        [IsDeleted] bit NULL,
        [CreatedDate] datetime2 NOT NULL,
        [CreatedStaffNo] nvarchar(max) NOT NULL,
        [UpdatedDate] datetime2 NULL,
        [UpdatedStaffNo] nvarchar(max) NULL,
        CONSTRAINT [PK_PremiumFees] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231102034045_initial')
BEGIN
    CREATE TABLE [Roles] (
        [Id] uniqueidentifier NOT NULL,
        [RoleName] nvarchar(max) NOT NULL,
        [IsActive] bit NOT NULL,
        [IsDeleted] bit NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [CreatedStaffNo] nvarchar(max) NOT NULL,
        [UpdatedDate] datetime2 NULL,
        [UpdatedStaffNo] nvarchar(max) NULL,
        CONSTRAINT [PK_Roles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231102034045_initial')
BEGIN
    CREATE TABLE [Settings] (
        [Id] uniqueidentifier NOT NULL,
        [Banner] nvarchar(max) NOT NULL,
        [TabletBanner] nvarchar(max) NOT NULL,
        [MobileBanner] nvarchar(max) NOT NULL,
        [TermsAndConditions] nvarchar(max) NOT NULL,
        [IsActive] bit NULL,
        [IsDeleted] bit NULL,
        [CreatedDate] datetime2 NOT NULL,
        [CreatedStaffNo] nvarchar(max) NOT NULL,
        [UpdatedDate] datetime2 NULL,
        [UpdatedStaffNo] nvarchar(max) NULL,
        CONSTRAINT [PK_Settings] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231102034045_initial')
BEGIN
    CREATE TABLE [Speakers] (
        [Id] int NOT NULL IDENTITY,
        [SpeakerName] nvarchar(100) NOT NULL,
        [Qualification] nvarchar(100) NOT NULL,
        [Experience] int NOT NULL,
        [SpeakingDate] datetime2 NOT NULL,
        [SpeakingTime] datetime2 NOT NULL,
        [Venue] nvarchar(255) NOT NULL,
        [ProfilePicture] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Speakers] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231102034045_initial')
BEGIN
    CREATE TABLE [StateCodes] (
        [Id] uniqueidentifier NOT NULL,
        [StateCodeEN] nvarchar(max) NOT NULL,
        [StateCodeMM] nvarchar(max) NOT NULL,
        [IsActive] bit NULL,
        [IsDeleted] bit NULL,
        [CreatedDate] datetime2 NOT NULL,
        [CreatedStaffNo] nvarchar(max) NOT NULL,
        [UpdatedDate] datetime2 NULL,
        [UpdatedStaffNo] nvarchar(max) NULL,
        CONSTRAINT [PK_StateCodes] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231102034045_initial')
BEGIN
    CREATE TABLE [TownShipCodes] (
        [Id] uniqueidentifier NOT NULL,
        [StateCodeId] uniqueidentifier NOT NULL,
        [CodeEN] nvarchar(max) NOT NULL,
        [CodeMM] nvarchar(max) NOT NULL,
        [IsActive] bit NULL,
        [IsDeleted] bit NULL,
        [CreatedDate] datetime2 NOT NULL,
        [CreatedStaffNo] nvarchar(max) NOT NULL,
        [UpdatedDate] datetime2 NULL,
        [UpdatedStaffNo] nvarchar(max) NULL,
        CONSTRAINT [PK_TownShipCodes] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231102034045_initial')
BEGIN
    CREATE TABLE [TransactionLogs] (
        [Id] uniqueidentifier NOT NULL,
        [username] nvarchar(max) NOT NULL,
        [TDateTime] datetime2 NOT NULL,
        [Events] nvarchar(max) NOT NULL,
        [FormName] nvarchar(max) NOT NULL,
        [Remark] nvarchar(max) NULL,
        CONSTRAINT [PK_TransactionLogs] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231102034045_initial')
BEGIN
    CREATE TABLE [Travellers] (
        [Id] uniqueidentifier NOT NULL,
        [OrderId] uniqueidentifier NULL,
        [Name] nvarchar(max) NOT NULL,
        [NRCType] int NULL,
        [NRCOrPassportData] nvarchar(max) NOT NULL,
        [Age] int NULL,
        CONSTRAINT [PK_Travellers] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231102034045_initial')
BEGIN
    CREATE TABLE [Users] (
        [Id] uniqueidentifier NOT NULL,
        [UserName] nvarchar(max) NOT NULL,
        [RoleId] uniqueidentifier NULL,
        [Email] nvarchar(max) NOT NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NOT NULL,
        [PhoneNumber] nvarchar(max) NOT NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        [IsActive] bit NOT NULL,
        [IsDeleted] bit NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [CreatedStaffNo] nvarchar(max) NOT NULL,
        [UpdatedDate] datetime2 NULL,
        [UpdatedStaffNo] nvarchar(max) NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20231102034045_initial')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20231102034045_initial', N'6.0.6');
END;
GO

COMMIT;
GO

