CREATE TABLE [Translate].[Dictionary] (
    [StringID]                   INT           IDENTITY (1, 1) NOT NULL,
    [VendorCode]                 VARCHAR (100) NULL,
    [OriginalString]             VARCHAR (900) NULL,
    [OriginalNormalizedString]   VARCHAR (900) NULL,
    [TranslatedString]           VARCHAR (900) NULL,
    [DimensionSuffix]            VARCHAR (100) NULL,
    [CreatedDate]                DATETIME      NULL,
    [ModifiedDate]               DATETIME      NULL,
    [TranslatedNormalizedString] VARCHAR (900) NULL,
    CONSTRAINT [PK_Translate.Dictionary] PRIMARY KEY CLUSTERED ([StringID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [orig_str_index]
    ON [Translate].[Dictionary]([OriginalString] ASC);


GO
CREATE NONCLUSTERED INDEX [tr_str_index]
    ON [Translate].[Dictionary]([TranslatedString] ASC);


GO
CREATE NONCLUSTERED INDEX [vcode_index]
    ON [Translate].[Dictionary]([VendorCode] ASC);

