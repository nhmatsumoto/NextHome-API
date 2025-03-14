-- Verifica se o banco de dados já existe antes de criar
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'NextHomeDB')
BEGIN
    CREATE DATABASE NextHomeDB;
    PRINT 'Banco de dados RealEstateDB criado com sucesso!';
END
ELSE
    PRINT 'Banco de dados RealEstateDB já existe.';
GO

-- Selecionando o banco de dados
USE NextHomeDB;
GO

-- Criando a tabela de Imobiliárias
BEGIN TRANSACTION;
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='RealEstateAgency' AND xtype='U')
BEGIN
    CREATE TABLE RealEstateAgency (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(255) NOT NULL, -- Nome da imobiliária (不動産会社名)
        BusinessRegistrationNumber NVARCHAR(50) NOT NULL UNIQUE, -- Número de registro comercial (事業者登録番号)
        RealEstateLicense NVARCHAR(50) NOT NULL UNIQUE, -- Licença imobiliária (宅地建物取引業免許)
        Phone NVARCHAR(20), -- Telefone (電話番号)
        Email NVARCHAR(255) UNIQUE, -- E-mail (メールアドレス)
        Address NVARCHAR(500) -- Endereço (住所)
    );
    PRINT 'Tabela RealEstateAgency criada com sucesso!';
END
ELSE
    PRINT 'Tabela RealEstateAgency já existe.';
COMMIT TRANSACTION;
GO

-- Criando a tabela de Endereços dos Imóveis
BEGIN TRANSACTION;
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='PropertyAddress' AND xtype='U')
BEGIN
    CREATE TABLE PropertyAddress (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Street NVARCHAR(255), -- Rua (通り)
        City NVARCHAR(100), -- Cidade (市町村)
        Prefecture NVARCHAR(100), -- Província (都道府県)
        PostalCode NVARCHAR(20), -- Código postal (郵便番号)
        NearestStation NVARCHAR(255), -- Estação mais próxima (最寄り駅)
        MinutesToStation INT -- Minutos até a estação (駅までの分数)
    );
    PRINT 'Tabela PropertyAddress criada com sucesso!';
END
ELSE
    PRINT 'Tabela PropertyAddress já existe.';
COMMIT TRANSACTION;
GO

-- Criando a tabela de Imóveis
BEGIN TRANSACTION;
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Property' AND xtype='U')
BEGIN
    CREATE TABLE Property (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Title NVARCHAR(255) NOT NULL, -- Título do imóvel (物件タイトル)
        Description NVARCHAR(MAX), -- Descrição do imóvel (物件説明)
        Price DECIMAL(18,2) NOT NULL, -- Preço do imóvel (価格)
        ManagementFee DECIMAL(18,2), -- Taxa de administração (管理費)
        DepositShikikin DECIMAL(18,2), -- Depósito (敷金)
        KeyMoneyReikin DECIMAL(18,2), -- Luvas (礼金)
        Bedrooms INT CHECK (Bedrooms >= 0), -- Número de quartos (寝室数)
        Bathrooms INT CHECK (Bathrooms >= 0), -- Número de banheiros (バスルーム数)
        ParkingSpaces INT CHECK (ParkingSpaces >= 0), -- Número de vagas de estacionamento (駐車スペース数)
        FloorArea FLOAT CHECK (FloorArea > 0), -- Área total (面積)
        YearBuilt INT CHECK (YearBuilt > 1800), -- Ano de construção (築年数)
        IsAvailable BIT NOT NULL DEFAULT 1, -- Disponibilidade (利用可能かどうか)
        Type INT NOT NULL, -- Tipo de imóvel (物件の種類)
        Category INT NOT NULL, -- Categoria do anúncio (掲載カテゴリ)
        AddressId INT FOREIGN KEY REFERENCES PropertyAddress(Id) ON DELETE CASCADE,
        RealEstateAgencyId INT NOT NULL FOREIGN KEY REFERENCES RealEstateAgency(Id) ON DELETE CASCADE
    );
    PRINT 'Tabela Property criada com sucesso!';
END
ELSE
    PRINT 'Tabela Property já existe.';
COMMIT TRANSACTION;
GO

-- Criando a tabela de Fotos dos Imóveis
BEGIN TRANSACTION;
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='PropertyPhoto' AND xtype='U')
BEGIN
    CREATE TABLE PropertyPhoto (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Url NVARCHAR(500) NOT NULL, -- URL da foto (写真のURL)
        PropertyId INT NOT NULL FOREIGN KEY REFERENCES Property(Id) ON DELETE CASCADE,
        Type NVARCHAR(50) NOT NULL CHECK (Type IN ('Main', 'Thumbnail', 'Gallery')) -- Tipo da foto (主な写真、サムネイル、ギャラリー)
    );
    PRINT 'Tabela PropertyPhoto criada com sucesso!';
END
ELSE
    PRINT 'Tabela PropertyPhoto já existe.';
COMMIT TRANSACTION;
GO


-- Criando a tabela de Plantas dos Imóveis
BEGIN TRANSACTION;
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='PropertyFloorPlan' AND xtype='U')
BEGIN
    CREATE TABLE PropertyFloorPlan (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        ImageUrl NVARCHAR(500) NOT NULL, -- URL da planta (間取り図のURL)
        PropertyId INT NOT NULL FOREIGN KEY REFERENCES Property(Id) ON DELETE CASCADE
    );
    PRINT 'Tabela PropertyFloorPlan criada com sucesso!';
END
ELSE
    PRINT 'Tabela PropertyFloorPlan já existe.';
COMMIT TRANSACTION;
GO

-- Criando a tabela de Contatos de Interessados
BEGIN TRANSACTION;
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Inquiry' AND xtype='U')
BEGIN
    CREATE TABLE Inquiry (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(255) NOT NULL, -- Nome do interessado (問い合わせ者の名前)
        Email NVARCHAR(255), -- E-mail do interessado (問い合わせ者のメール)
        Phone NVARCHAR(20), -- Telefone do interessado (問い合わせ者の電話番号)
        InquiryDate DATETIME NOT NULL DEFAULT GETDATE(), -- Data do contato (問い合わせ日)
        PropertyId INT NOT NULL FOREIGN KEY REFERENCES Property(Id) ON DELETE CASCADE
    );
    PRINT 'Tabela Inquiry criada com sucesso!';
END
ELSE
    PRINT 'Tabela Inquiry já existe.';
COMMIT TRANSACTION;
GO

-- Criando Índices para Melhor Desempenho
BEGIN TRANSACTION;
CREATE INDEX IDX_Property_Address ON Property(AddressId);
CREATE INDEX IDX_Property_RealEstateAgency ON Property(RealEstateAgencyId);
CREATE INDEX IDX_Property_Price ON Property(Price);
CREATE INDEX IDX_Property_Type_Category ON Property(Type, Category);
CREATE INDEX IDX_Inquiry_PropertyId ON Inquiry(PropertyId);
CREATE INDEX IDX_PropertyPhoto_PropertyId ON PropertyPhoto(PropertyId);
CREATE INDEX IDX_PropertyFloorPlan_PropertyId ON PropertyFloorPlan(PropertyId);
PRINT 'Índices criados com sucesso!';
COMMIT TRANSACTION;
GO
