CREATE TABLE [Translate].[TempDictionary] (
    [StringID]        INT           IDENTITY (1, 1) NOT NULL,
    [VendorCode]      VARCHAR (100) NULL,
    [ItalianString]   VARCHAR (900) NULL,
    [UkrainianString] VARCHAR (900) NULL,
    [DimensionSuffix] VARCHAR (100) NULL
);

