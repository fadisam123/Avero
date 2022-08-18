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

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [catagory] (
    [Id] bigint NOT NULL IDENTITY,
    [name] nvarchar(max) NOT NULL,
    [img_name] nvarchar(max) NULL,
    CONSTRAINT [PK_catagory] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [city] (
    [Id] bigint NOT NULL IDENTITY,
    [name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_city] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [neighborhood] (
    [Id] bigint NOT NULL IDENTITY,
    [name] nvarchar(max) NOT NULL,
    [city_id] bigint NOT NULL,
    CONSTRAINT [PK_neighborhood] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_neighborhood_city_city_id] FOREIGN KEY ([city_id]) REFERENCES [city] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [name] nvarchar(max) NOT NULL,
    [img_name] nvarchar(max) NULL,
    [latitude] float NOT NULL,
    [longitude] float NOT NULL,
    [street_name] nvarchar(max) NULL,
    [registered_at] datetime2 NOT NULL,
    [last_login] datetime2 NOT NULL,
    [rating] int NULL,
    [rated_people_count] int NOT NULL,
    [marker_map_address] nvarchar(max) NULL,
    [neighborhood_id] bigint NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUsers_neighborhood_neighborhood_id] FOREIGN KEY ([neighborhood_id]) REFERENCES [neighborhood] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [order] (
    [Id] bigint NOT NULL IDENTITY,
    [order_date] datetime2 NOT NULL,
    [retailer_id] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_order] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_order_AspNetUsers_retailer_id] FOREIGN KEY ([retailer_id]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [product] (
    [Id] bigint NOT NULL IDENTITY,
    [name] nvarchar(max) NOT NULL,
    [desc] nvarchar(max) NULL,
    [quantity_available] int NOT NULL,
    [price_per_unit] float NOT NULL,
    [offer_price] float NOT NULL,
    [offer_price_start_date] datetime2 NULL,
    [offer_price_end_date] datetime2 NULL,
    [created_at] datetime2 NULL,
    [rating] int NULL,
    [rated_people_count] int NOT NULL,
    [wholesealer_id] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_product] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_product_AspNetUsers_wholesealer_id] FOREIGN KEY ([wholesealer_id]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [order_details] (
    [Id] bigint NOT NULL IDENTITY,
    [order_id] bigint NULL,
    [product_id] bigint NULL,
    [quantity] int NOT NULL,
    [processing_state] int NOT NULL,
    CONSTRAINT [PK_order_details] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_order_details_order_order_id] FOREIGN KEY ([order_id]) REFERENCES [order] ([Id]),
    CONSTRAINT [FK_order_details_product_product_id] FOREIGN KEY ([product_id]) REFERENCES [product] ([Id])
);
GO

CREATE TABLE [product_catagory] (
    [Id] bigint NOT NULL IDENTITY,
    [catagory_id] bigint NULL,
    [product_id] bigint NULL,
    CONSTRAINT [PK_product_catagory] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_product_catagory_catagory_catagory_id] FOREIGN KEY ([catagory_id]) REFERENCES [catagory] ([Id]),
    CONSTRAINT [FK_product_catagory_product_product_id] FOREIGN KEY ([product_id]) REFERENCES [product] ([Id])
);
GO

CREATE TABLE [Product_imgs] (
    [Id] bigint NOT NULL IDENTITY,
    [img_name] nvarchar(max) NULL,
    [product_id] bigint NOT NULL,
    CONSTRAINT [PK_Product_imgs] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Product_imgs_product_product_id] FOREIGN KEY ([product_id]) REFERENCES [product] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [product_review] (
    [Id] bigint NOT NULL IDENTITY,
    [created_at] datetime2 NOT NULL,
    [content] nvarchar(max) NOT NULL,
    [product_id] bigint NOT NULL,
    [user_id] nvarchar(450) NULL,
    CONSTRAINT [PK_product_review] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_product_review_AspNetUsers_user_id] FOREIGN KEY ([user_id]) REFERENCES [AspNetUsers] ([Id]),
    CONSTRAINT [FK_product_review_product_product_id] FOREIGN KEY ([product_id]) REFERENCES [product] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
GO

CREATE INDEX [IX_AspNetUsers_neighborhood_id] ON [AspNetUsers] ([neighborhood_id]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

CREATE INDEX [IX_neighborhood_city_id] ON [neighborhood] ([city_id]);
GO

CREATE INDEX [IX_order_retailer_id] ON [order] ([retailer_id]);
GO

CREATE INDEX [IX_order_details_order_id] ON [order_details] ([order_id]);
GO

CREATE INDEX [IX_order_details_product_id] ON [order_details] ([product_id]);
GO

CREATE INDEX [IX_product_wholesealer_id] ON [product] ([wholesealer_id]);
GO

CREATE INDEX [IX_product_catagory_catagory_id] ON [product_catagory] ([catagory_id]);
GO

CREATE INDEX [IX_product_catagory_product_id] ON [product_catagory] ([product_id]);
GO

CREATE INDEX [IX_Product_imgs_product_id] ON [Product_imgs] ([product_id]);
GO

CREATE INDEX [IX_product_review_product_id] ON [product_review] ([product_id]);
GO

CREATE INDEX [IX_product_review_user_id] ON [product_review] ([user_id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220726032526_initial', N'6.0.7');
GO

COMMIT;
GO

