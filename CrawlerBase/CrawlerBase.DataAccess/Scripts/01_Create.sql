CREATE TABLE [dbo].[RawData](  
    [Id] [int] IDENTITY(1,1) NOT NULL,  
    [RawDataTypeId] int not null,  
    [SourceUrl] [varchar](1024) not NULL,  
    [DateDefined] [datetime] not null,  
    [Data] [varchar](max) not null,
PRIMARY KEY CLUSTERED   
(  
    [Id] ASC  
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]  
) ON [PRIMARY]  