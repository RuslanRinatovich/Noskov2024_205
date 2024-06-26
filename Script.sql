USE [master]
GO
/****** Object:  Database [CarShowRoom]    Script Date: 17.04.2024 16:53:10 ******/
CREATE DATABASE [CarShowRoom]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CarShowRoom', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\CarShowRoom.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CarShowRoom_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\CarShowRoom_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [CarShowRoom] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CarShowRoom].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CarShowRoom] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CarShowRoom] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CarShowRoom] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CarShowRoom] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CarShowRoom] SET ARITHABORT OFF 
GO
ALTER DATABASE [CarShowRoom] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [CarShowRoom] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CarShowRoom] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CarShowRoom] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CarShowRoom] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CarShowRoom] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CarShowRoom] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CarShowRoom] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CarShowRoom] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CarShowRoom] SET  ENABLE_BROKER 
GO
ALTER DATABASE [CarShowRoom] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CarShowRoom] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CarShowRoom] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CarShowRoom] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CarShowRoom] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CarShowRoom] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CarShowRoom] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CarShowRoom] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [CarShowRoom] SET  MULTI_USER 
GO
ALTER DATABASE [CarShowRoom] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CarShowRoom] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CarShowRoom] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CarShowRoom] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CarShowRoom] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CarShowRoom] SET QUERY_STORE = OFF
GO
USE [CarShowRoom]
GO
/****** Object:  Table [dbo].[Air]    Script Date: 17.04.2024 16:53:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Air](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Air] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Brand]    Script Date: 17.04.2024 16:53:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Brand](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Brand] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Car]    Script Date: 17.04.2024 16:53:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Car](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[BrandId] [int] NOT NULL,
	[FuelTypeId] [int] NOT NULL,
	[AirId] [int] NOT NULL,
	[TypeId] [int] NOT NULL,
	[EngineCapacity] [float] NOT NULL,
	[FuelRate] [float] NOT NULL,
	[TrunkVolume] [int] NOT NULL,
	[HorsePower] [int] NOT NULL,
	[Year] [int] NOT NULL,
	[TransmissionId] [int] NOT NULL,
	[DoorCount] [int] NOT NULL,
	[ColorId] [int] NOT NULL,
	[Price] [int] NOT NULL,
	[IsEnabled] [bit] NULL,
 CONSTRAINT [PK_Car] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Client]    Script Date: 17.04.2024 16:53:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client](
	[UserName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[MiddleName] [nvarchar](50) NULL,
	[Phone] [nvarchar](30) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[PassportSeries] [nvarchar](50) NULL,
	[PassportNum] [nvarchar](50) NULL,
 CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Color]    Script Date: 17.04.2024 16:53:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Color](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Color] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FuelType]    Script Date: 17.04.2024 16:53:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FuelType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_FuelType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Option]    Script Date: 17.04.2024 16:53:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Option](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Price] [int] NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 17.04.2024 16:53:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CarId] [int] NOT NULL,
	[StatusId] [int] NOT NULL,
	[DateStart] [date] NULL,
	[DateEnd] [date] NULL,
	[Username] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Rent] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderContent]    Script Date: 17.04.2024 16:53:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderContent](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OptionId] [int] NOT NULL,
	[OrderId] [int] NOT NULL,
 CONSTRAINT [PK_Prokat] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Photo]    Script Date: 17.04.2024 16:53:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Photo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CarId] [int] NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Photo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 17.04.2024 16:53:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 17.04.2024 16:53:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status](
	[Id] [int] NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Color] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transmission]    Script Date: 17.04.2024 16:53:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transmission](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Transmission] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Type]    Script Date: 17.04.2024 16:53:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Type](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Type] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 17.04.2024 16:53:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](100) NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Air] ON 

INSERT [dbo].[Air] ([Id], [Title]) VALUES (1, N'нет       ')
INSERT [dbo].[Air] ([Id], [Title]) VALUES (2, N'кондиционер')
INSERT [dbo].[Air] ([Id], [Title]) VALUES (3, N'Климат-контроль 1-зонный')
INSERT [dbo].[Air] ([Id], [Title]) VALUES (4, N'Климат-контроль 2-зонный')
INSERT [dbo].[Air] ([Id], [Title]) VALUES (5, N'Климат-контроль многозонный')
SET IDENTITY_INSERT [dbo].[Air] OFF
GO
SET IDENTITY_INSERT [dbo].[Brand] ON 

INSERT [dbo].[Brand] ([Id], [Title]) VALUES (1, N'BMW')
INSERT [dbo].[Brand] ([Id], [Title]) VALUES (2, N'Chery')
INSERT [dbo].[Brand] ([Id], [Title]) VALUES (3, N'Citroen')
INSERT [dbo].[Brand] ([Id], [Title]) VALUES (4, N'Daewoo')
INSERT [dbo].[Brand] ([Id], [Title]) VALUES (5, N'Datsun')
INSERT [dbo].[Brand] ([Id], [Title]) VALUES (6, N'FAW')
INSERT [dbo].[Brand] ([Id], [Title]) VALUES (7, N'Fiat')
INSERT [dbo].[Brand] ([Id], [Title]) VALUES (8, N'Ford')
INSERT [dbo].[Brand] ([Id], [Title]) VALUES (9, N'Great Wall')
INSERT [dbo].[Brand] ([Id], [Title]) VALUES (10, N'Haval')
INSERT [dbo].[Brand] ([Id], [Title]) VALUES (11, N'LADA(ВАЗ)')
INSERT [dbo].[Brand] ([Id], [Title]) VALUES (12, N'KIA')
INSERT [dbo].[Brand] ([Id], [Title]) VALUES (13, N'Lifan')
INSERT [dbo].[Brand] ([Id], [Title]) VALUES (14, N'Renault')
INSERT [dbo].[Brand] ([Id], [Title]) VALUES (15, N'Skoda')
INSERT [dbo].[Brand] ([Id], [Title]) VALUES (16, N'Volkswagen')
INSERT [dbo].[Brand] ([Id], [Title]) VALUES (17, N'Hyundai')
INSERT [dbo].[Brand] ([Id], [Title]) VALUES (18, N'ГАз')
INSERT [dbo].[Brand] ([Id], [Title]) VALUES (19, N'УАЗ')
INSERT [dbo].[Brand] ([Id], [Title]) VALUES (20, N'ЗАЗ')
SET IDENTITY_INSERT [dbo].[Brand] OFF
GO
SET IDENTITY_INSERT [dbo].[Car] ON 

INSERT [dbo].[Car] ([Id], [Title], [BrandId], [FuelTypeId], [AirId], [TypeId], [EngineCapacity], [FuelRate], [TrunkVolume], [HorsePower], [Year], [TransmissionId], [DoorCount], [ColorId], [Price], [IsEnabled]) VALUES (1, N'Лада Гранта MT', 11, 1, 1, 1, 1.6, 8, 300, 96, 2024, 1, 5, 1, 794500, 0)
INSERT [dbo].[Car] ([Id], [Title], [BrandId], [FuelTypeId], [AirId], [TypeId], [EngineCapacity], [FuelRate], [TrunkVolume], [HorsePower], [Year], [TransmissionId], [DoorCount], [ColorId], [Price], [IsEnabled]) VALUES (2, N'Renault Logan Stepway', 14, 1, 2, 5, 1.6, 7, 510, 82, 2023, 1, 5, 1, 2290000, 0)
INSERT [dbo].[Car] ([Id], [Title], [BrandId], [FuelTypeId], [AirId], [TypeId], [EngineCapacity], [FuelRate], [TrunkVolume], [HorsePower], [Year], [TransmissionId], [DoorCount], [ColorId], [Price], [IsEnabled]) VALUES (4, N'Лада 4x4 MT', 11, 1, 1, 2, 1.6, 12, 350, 96, 2024, 1, 5, 8, 830000, 0)
INSERT [dbo].[Car] ([Id], [Title], [BrandId], [FuelTypeId], [AirId], [TypeId], [EngineCapacity], [FuelRate], [TrunkVolume], [HorsePower], [Year], [TransmissionId], [DoorCount], [ColorId], [Price], [IsEnabled]) VALUES (5, N'Рено Логан NEW AT', 14, 1, 1, 5, 1.6, 9, 360, 96, 2023, 1, 5, 1, 1890000, 1)
INSERT [dbo].[Car] ([Id], [Title], [BrandId], [FuelTypeId], [AirId], [TypeId], [EngineCapacity], [FuelRate], [TrunkVolume], [HorsePower], [Year], [TransmissionId], [DoorCount], [ColorId], [Price], [IsEnabled]) VALUES (6, N'Lada (ВАЗ) Largus Cross', 11, 1, 2, 6, 1.6, 7.8000000000000043, 560, 106, 2022, 1, 5, 4, 2159000, 0)
INSERT [dbo].[Car] ([Id], [Title], [BrandId], [FuelTypeId], [AirId], [TypeId], [EngineCapacity], [FuelRate], [TrunkVolume], [HorsePower], [Year], [TransmissionId], [DoorCount], [ColorId], [Price], [IsEnabled]) VALUES (7, N'Шкода Рапид AT', 15, 1, 4, 5, 1.6, 9, 400, 96, 2022, 2, 5, 1, 2200000, 1)
INSERT [dbo].[Car] ([Id], [Title], [BrandId], [FuelTypeId], [AirId], [TypeId], [EngineCapacity], [FuelRate], [TrunkVolume], [HorsePower], [Year], [TransmissionId], [DoorCount], [ColorId], [Price], [IsEnabled]) VALUES (8, N'Форд Фиеста AT', 8, 1, 4, 5, 1.6, 9, 350, 96, 2024, 2, 5, 3, 2223000, 1)
INSERT [dbo].[Car] ([Id], [Title], [BrandId], [FuelTypeId], [AirId], [TypeId], [EngineCapacity], [FuelRate], [TrunkVolume], [HorsePower], [Year], [TransmissionId], [DoorCount], [ColorId], [Price], [IsEnabled]) VALUES (9, N'BMW_x530d IV', 1, 2, 5, 1, 3, 8, 650, 298, 2024, 2, 5, 2, 16000000, 1)
INSERT [dbo].[Car] ([Id], [Title], [BrandId], [FuelTypeId], [AirId], [TypeId], [EngineCapacity], [FuelRate], [TrunkVolume], [HorsePower], [Year], [TransmissionId], [DoorCount], [ColorId], [Price], [IsEnabled]) VALUES (10, N'Лада 4x4 MT', 11, 1, 1, 1, 1.6, 12, 350, 96, 2024, 1, 3, 1, 795000, 1)
INSERT [dbo].[Car] ([Id], [Title], [BrandId], [FuelTypeId], [AirId], [TypeId], [EngineCapacity], [FuelRate], [TrunkVolume], [HorsePower], [Year], [TransmissionId], [DoorCount], [ColorId], [Price], [IsEnabled]) VALUES (11, N'Ford Mustang', 8, 1, 5, 2, 5, 12.1, 408, 450, 2023, 2, 2, 4, 9000000, 0)
SET IDENTITY_INSERT [dbo].[Car] OFF
GO
INSERT [dbo].[Client] ([UserName], [LastName], [FirstName], [MiddleName], [Phone], [Email], [PassportSeries], [PassportNum]) VALUES (N'admin', N'Носков', N'Богдан', N'Сергеевич', N'+7 (777) 777-77-77', N'bogdantop007@mail.ru', N'9999', N'999999')
INSERT [dbo].[Client] ([UserName], [LastName], [FirstName], [MiddleName], [Phone], [Email], [PassportSeries], [PassportNum]) VALUES (N'alisher', N'Мусинов', N'Алишер', N'Усманович', N'+7 (900) 745-32-34', N'alishtop@mail.ru', NULL, NULL)
INSERT [dbo].[Client] ([UserName], [LastName], [FirstName], [MiddleName], [Phone], [Email], [PassportSeries], [PassportNum]) VALUES (N'andrei', N'Гудихин', N'Андрей', N'Евгеньевич', N'89674547454', N'andrei@mail.ru', N'1111', N'1111111')
INSERT [dbo].[Client] ([UserName], [LastName], [FirstName], [MiddleName], [Phone], [Email], [PassportSeries], [PassportNum]) VALUES (N'dimon', N'Ефремов', N'Дмитрий', N'Антонович', N'+7 (917) 459-24-33', N'dimon2228@gmail.ru', N'9209', N'878787')
INSERT [dbo].[Client] ([UserName], [LastName], [FirstName], [MiddleName], [Phone], [Email], [PassportSeries], [PassportNum]) VALUES (N'dunkan', N'Астахов ', N'Дункан', N' Николаевич', N'+7 (939) 848-86-83', N'AstahovDunkan459@mail.ru', NULL, NULL)
INSERT [dbo].[Client] ([UserName], [LastName], [FirstName], [MiddleName], [Phone], [Email], [PassportSeries], [PassportNum]) VALUES (N'fedor', N'Федорова', N'Анна', N'Александровна', N'+7 (969) 325-95-89', N'anya188@mail.ru', NULL, NULL)
INSERT [dbo].[Client] ([UserName], [LastName], [FirstName], [MiddleName], [Phone], [Email], [PassportSeries], [PassportNum]) VALUES (N'kityy', N'Иванов', N'Иван', N'Иванович', N'+1 (111) 111-11-11', N'hello@kitty.ru', N'1111', N'111111')
INSERT [dbo].[Client] ([UserName], [LastName], [FirstName], [MiddleName], [Phone], [Email], [PassportSeries], [PassportNum]) VALUES (N'ladybag', N'BD', N'fds', N'fds', N'+7 (897) 897-98-79', N'fdsf@maifds', NULL, NULL)
INSERT [dbo].[Client] ([UserName], [LastName], [FirstName], [MiddleName], [Phone], [Email], [PassportSeries], [PassportNum]) VALUES (N'liza', N'Герасимова', N'Елизавета', N'Сергеевна', N'+7 (991) 240-73-10', N'lisabetta@yandex.ru', N'1', N'2')
INSERT [dbo].[Client] ([UserName], [LastName], [FirstName], [MiddleName], [Phone], [Email], [PassportSeries], [PassportNum]) VALUES (N'partina', N'Москаленко ', N'Партина', N' Геннадиевна', N'+7 (954) 343-27-62', N'MoskalenkoPartina240@mail.ru', NULL, NULL)
INSERT [dbo].[Client] ([UserName], [LastName], [FirstName], [MiddleName], [Phone], [Email], [PassportSeries], [PassportNum]) VALUES (N'ruzilya', N'Миндубаева', N'Рузиля', N'Рафисовна', N'+7 (942) 988-43-60', N'rusilya@mail.ru', NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Color] ON 

INSERT [dbo].[Color] ([Id], [Title]) VALUES (1, N'Белый')
INSERT [dbo].[Color] ([Id], [Title]) VALUES (2, N'Серый')
INSERT [dbo].[Color] ([Id], [Title]) VALUES (3, N'Черный')
INSERT [dbo].[Color] ([Id], [Title]) VALUES (4, N'Синий')
INSERT [dbo].[Color] ([Id], [Title]) VALUES (5, N'Серебристый')
INSERT [dbo].[Color] ([Id], [Title]) VALUES (6, N'Красный')
INSERT [dbo].[Color] ([Id], [Title]) VALUES (7, N'Зелёный')
INSERT [dbo].[Color] ([Id], [Title]) VALUES (8, N'Хаки')
INSERT [dbo].[Color] ([Id], [Title]) VALUES (9, N'Мокрый асфальт')
SET IDENTITY_INSERT [dbo].[Color] OFF
GO
SET IDENTITY_INSERT [dbo].[FuelType] ON 

INSERT [dbo].[FuelType] ([Id], [Title]) VALUES (1, N'бензин')
INSERT [dbo].[FuelType] ([Id], [Title]) VALUES (2, N'дизель')
INSERT [dbo].[FuelType] ([Id], [Title]) VALUES (3, N'пропан')
INSERT [dbo].[FuelType] ([Id], [Title]) VALUES (4, N'электричество')
SET IDENTITY_INSERT [dbo].[FuelType] OFF
GO
SET IDENTITY_INSERT [dbo].[Option] ON 

INSERT [dbo].[Option] ([Id], [Title], [Price]) VALUES (1, N'Сигнализация', 30000)
INSERT [dbo].[Option] ([Id], [Title], [Price]) VALUES (2, N'Пакет зимний', 60000)
INSERT [dbo].[Option] ([Id], [Title], [Price]) VALUES (3, N'Защита картера', 10000)
INSERT [dbo].[Option] ([Id], [Title], [Price]) VALUES (4, N'Сетка на радиатор', 5000)
INSERT [dbo].[Option] ([Id], [Title], [Price]) VALUES (5, N'Тонировка', 6000)
INSERT [dbo].[Option] ([Id], [Title], [Price]) VALUES (6, N'Антихром', 10000)
SET IDENTITY_INSERT [dbo].[Option] OFF
GO
SET IDENTITY_INSERT [dbo].[Order] ON 

INSERT [dbo].[Order] ([Id], [CarId], [StatusId], [DateStart], [DateEnd], [Username]) VALUES (27, 11, 2, CAST(N'2024-04-17' AS Date), CAST(N'2024-04-18' AS Date), N'alisher')
INSERT [dbo].[Order] ([Id], [CarId], [StatusId], [DateStart], [DateEnd], [Username]) VALUES (28, 2, 2, CAST(N'2024-04-17' AS Date), CAST(N'2024-04-18' AS Date), N'kityy')
INSERT [dbo].[Order] ([Id], [CarId], [StatusId], [DateStart], [DateEnd], [Username]) VALUES (29, 1, 1, CAST(N'2024-04-17' AS Date), NULL, N'fedor')
INSERT [dbo].[Order] ([Id], [CarId], [StatusId], [DateStart], [DateEnd], [Username]) VALUES (30, 4, 0, CAST(N'2024-04-17' AS Date), NULL, N'andrei')
INSERT [dbo].[Order] ([Id], [CarId], [StatusId], [DateStart], [DateEnd], [Username]) VALUES (31, 6, 0, CAST(N'2024-04-17' AS Date), NULL, N'partina')
SET IDENTITY_INSERT [dbo].[Order] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderContent] ON 

INSERT [dbo].[OrderContent] ([Id], [OptionId], [OrderId]) VALUES (7, 4, 27)
INSERT [dbo].[OrderContent] ([Id], [OptionId], [OrderId]) VALUES (8, 3, 27)
INSERT [dbo].[OrderContent] ([Id], [OptionId], [OrderId]) VALUES (9, 1, 27)
INSERT [dbo].[OrderContent] ([Id], [OptionId], [OrderId]) VALUES (10, 6, 27)
INSERT [dbo].[OrderContent] ([Id], [OptionId], [OrderId]) VALUES (11, 3, 29)
INSERT [dbo].[OrderContent] ([Id], [OptionId], [OrderId]) VALUES (12, 4, 29)
INSERT [dbo].[OrderContent] ([Id], [OptionId], [OrderId]) VALUES (13, 5, 29)
INSERT [dbo].[OrderContent] ([Id], [OptionId], [OrderId]) VALUES (14, 4, 28)
INSERT [dbo].[OrderContent] ([Id], [OptionId], [OrderId]) VALUES (15, 3, 28)
INSERT [dbo].[OrderContent] ([Id], [OptionId], [OrderId]) VALUES (16, 5, 28)
INSERT [dbo].[OrderContent] ([Id], [OptionId], [OrderId]) VALUES (17, 1, 28)
SET IDENTITY_INSERT [dbo].[OrderContent] OFF
GO
SET IDENTITY_INSERT [dbo].[Photo] ON 

INSERT [dbo].[Photo] ([Id], [CarId], [Title]) VALUES (1, 1, N'granta_black_1.jpg')
INSERT [dbo].[Photo] ([Id], [CarId], [Title]) VALUES (2, 1, N'granta_black_2.jpg')
INSERT [dbo].[Photo] ([Id], [CarId], [Title]) VALUES (5, 4, N'Lada44_1.jpg')
INSERT [dbo].[Photo] ([Id], [CarId], [Title]) VALUES (12, 9, N'BMW_x530dIV_1.jpg')
INSERT [dbo].[Photo] ([Id], [CarId], [Title]) VALUES (13, 9, N'BMW_x530dIV_2.jpg')
INSERT [dbo].[Photo] ([Id], [CarId], [Title]) VALUES (14, 9, N'BMW_x530dIV_3.jpg')
INSERT [dbo].[Photo] ([Id], [CarId], [Title]) VALUES (15, 7, N'Rapid_1.jpg')
INSERT [dbo].[Photo] ([Id], [CarId], [Title]) VALUES (16, 7, N'Rapid_2.jpg')
INSERT [dbo].[Photo] ([Id], [CarId], [Title]) VALUES (17, 7, N'Rapid_3.jpg')
INSERT [dbo].[Photo] ([Id], [CarId], [Title]) VALUES (18, 8, N'Fiesta1.jpg')
INSERT [dbo].[Photo] ([Id], [CarId], [Title]) VALUES (19, 8, N'Fiesta2.jpg')
INSERT [dbo].[Photo] ([Id], [CarId], [Title]) VALUES (20, 8, N'Fiesta3.jpg')
INSERT [dbo].[Photo] ([Id], [CarId], [Title]) VALUES (21, 8, N'Fiesta4.jpg')
INSERT [dbo].[Photo] ([Id], [CarId], [Title]) VALUES (22, 4, N'Niva_w_5.jpg')
INSERT [dbo].[Photo] ([Id], [CarId], [Title]) VALUES (23, 4, N'Niva_w_6.jpg')
INSERT [dbo].[Photo] ([Id], [CarId], [Title]) VALUES (24, 10, N'Niva_w_1.jpg')
INSERT [dbo].[Photo] ([Id], [CarId], [Title]) VALUES (25, 10, N'Niva_w_2.jpg')
INSERT [dbo].[Photo] ([Id], [CarId], [Title]) VALUES (26, 10, N'Niva_w_3.jpg')
INSERT [dbo].[Photo] ([Id], [CarId], [Title]) VALUES (27, 10, N'Niva_w_4.jpg')
INSERT [dbo].[Photo] ([Id], [CarId], [Title]) VALUES (28, 10, N'1Niva_w_5.jpg')
INSERT [dbo].[Photo] ([Id], [CarId], [Title]) VALUES (29, 10, N'1Niva_w_6.jpg')
INSERT [dbo].[Photo] ([Id], [CarId], [Title]) VALUES (30, 6, N'largus_1.jpg')
INSERT [dbo].[Photo] ([Id], [CarId], [Title]) VALUES (31, 6, N'largus_2.jpg')
INSERT [dbo].[Photo] ([Id], [CarId], [Title]) VALUES (32, 6, N'largus_3.jpg')
INSERT [dbo].[Photo] ([Id], [CarId], [Title]) VALUES (33, 2, N'stepway_1.jpg')
INSERT [dbo].[Photo] ([Id], [CarId], [Title]) VALUES (34, 2, N'stepway_2.jpg')
INSERT [dbo].[Photo] ([Id], [CarId], [Title]) VALUES (35, 2, N'stepway_3.jpg')
INSERT [dbo].[Photo] ([Id], [CarId], [Title]) VALUES (36, 2, N'stepway_4.jpg')
INSERT [dbo].[Photo] ([Id], [CarId], [Title]) VALUES (37, 11, N'ford_1.jpg')
INSERT [dbo].[Photo] ([Id], [CarId], [Title]) VALUES (38, 11, N'ford_2.jpg')
INSERT [dbo].[Photo] ([Id], [CarId], [Title]) VALUES (39, 11, N'ford_3.jpg')
INSERT [dbo].[Photo] ([Id], [CarId], [Title]) VALUES (40, 11, N'ford_4.jpg')
INSERT [dbo].[Photo] ([Id], [CarId], [Title]) VALUES (41, 11, N'ford_5.jpg')
INSERT [dbo].[Photo] ([Id], [CarId], [Title]) VALUES (42, 5, N'logan_1.jpeg')
INSERT [dbo].[Photo] ([Id], [CarId], [Title]) VALUES (43, 5, N'logan_2.jpeg')
INSERT [dbo].[Photo] ([Id], [CarId], [Title]) VALUES (44, 5, N'logan_3.jpeg')
SET IDENTITY_INSERT [dbo].[Photo] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([Id], [Title]) VALUES (1, N'admin')
INSERT [dbo].[Role] ([Id], [Title]) VALUES (2, N'user')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
INSERT [dbo].[Status] ([Id], [Title], [Color]) VALUES (0, N'забронирован', N'#FA8072')
INSERT [dbo].[Status] ([Id], [Title], [Color]) VALUES (1, N'оформляется', N'#F0E68C')
INSERT [dbo].[Status] ([Id], [Title], [Color]) VALUES (2, N'завершен', N'#ADFF2F')
GO
SET IDENTITY_INSERT [dbo].[Transmission] ON 

INSERT [dbo].[Transmission] ([Id], [Title]) VALUES (1, N'МКПП')
INSERT [dbo].[Transmission] ([Id], [Title]) VALUES (2, N'АКПП')
INSERT [dbo].[Transmission] ([Id], [Title]) VALUES (3, N'Робот')
INSERT [dbo].[Transmission] ([Id], [Title]) VALUES (4, N'Вариатор')
SET IDENTITY_INSERT [dbo].[Transmission] OFF
GO
SET IDENTITY_INSERT [dbo].[Type] ON 

INSERT [dbo].[Type] ([Id], [Title]) VALUES (1, N'Внедорожник')
INSERT [dbo].[Type] ([Id], [Title]) VALUES (2, N'Купе')
INSERT [dbo].[Type] ([Id], [Title]) VALUES (3, N'Минивен')
INSERT [dbo].[Type] ([Id], [Title]) VALUES (4, N'Пикап')
INSERT [dbo].[Type] ([Id], [Title]) VALUES (5, N'Седан')
INSERT [dbo].[Type] ([Id], [Title]) VALUES (6, N'Универсал')
INSERT [dbo].[Type] ([Id], [Title]) VALUES (7, N'Фургон')
INSERT [dbo].[Type] ([Id], [Title]) VALUES (8, N'Хэтчбек')
SET IDENTITY_INSERT [dbo].[Type] OFF
GO
INSERT [dbo].[User] ([UserName], [Password], [RoleId]) VALUES (N'admin', N'1', 1)
INSERT [dbo].[User] ([UserName], [Password], [RoleId]) VALUES (N'alisher', N'1', 2)
INSERT [dbo].[User] ([UserName], [Password], [RoleId]) VALUES (N'andrei', N'1', 2)
INSERT [dbo].[User] ([UserName], [Password], [RoleId]) VALUES (N'dimon', N'1', 2)
INSERT [dbo].[User] ([UserName], [Password], [RoleId]) VALUES (N'dunkan', N'1', 2)
INSERT [dbo].[User] ([UserName], [Password], [RoleId]) VALUES (N'fedor', N'1', 2)
INSERT [dbo].[User] ([UserName], [Password], [RoleId]) VALUES (N'kityy', N'2', 2)
INSERT [dbo].[User] ([UserName], [Password], [RoleId]) VALUES (N'ladybag', N'1', 2)
INSERT [dbo].[User] ([UserName], [Password], [RoleId]) VALUES (N'liza', N'1', 2)
INSERT [dbo].[User] ([UserName], [Password], [RoleId]) VALUES (N'partina', N'1', 2)
INSERT [dbo].[User] ([UserName], [Password], [RoleId]) VALUES (N'ruzilya', N'1', 2)
GO
ALTER TABLE [dbo].[Car]  WITH CHECK ADD  CONSTRAINT [FK_Car_Air] FOREIGN KEY([AirId])
REFERENCES [dbo].[Air] ([Id])
GO
ALTER TABLE [dbo].[Car] CHECK CONSTRAINT [FK_Car_Air]
GO
ALTER TABLE [dbo].[Car]  WITH CHECK ADD  CONSTRAINT [FK_Car_Brand] FOREIGN KEY([BrandId])
REFERENCES [dbo].[Brand] ([Id])
GO
ALTER TABLE [dbo].[Car] CHECK CONSTRAINT [FK_Car_Brand]
GO
ALTER TABLE [dbo].[Car]  WITH CHECK ADD  CONSTRAINT [FK_Car_Color] FOREIGN KEY([ColorId])
REFERENCES [dbo].[Color] ([Id])
GO
ALTER TABLE [dbo].[Car] CHECK CONSTRAINT [FK_Car_Color]
GO
ALTER TABLE [dbo].[Car]  WITH CHECK ADD  CONSTRAINT [FK_Car_FuelType] FOREIGN KEY([FuelTypeId])
REFERENCES [dbo].[FuelType] ([Id])
GO
ALTER TABLE [dbo].[Car] CHECK CONSTRAINT [FK_Car_FuelType]
GO
ALTER TABLE [dbo].[Car]  WITH CHECK ADD  CONSTRAINT [FK_Car_Transmission] FOREIGN KEY([TransmissionId])
REFERENCES [dbo].[Transmission] ([Id])
GO
ALTER TABLE [dbo].[Car] CHECK CONSTRAINT [FK_Car_Transmission]
GO
ALTER TABLE [dbo].[Car]  WITH CHECK ADD  CONSTRAINT [FK_Car_Type] FOREIGN KEY([TypeId])
REFERENCES [dbo].[Type] ([Id])
GO
ALTER TABLE [dbo].[Car] CHECK CONSTRAINT [FK_Car_Type]
GO
ALTER TABLE [dbo].[Client]  WITH CHECK ADD  CONSTRAINT [FK_Client_User] FOREIGN KEY([UserName])
REFERENCES [dbo].[User] ([UserName])
GO
ALTER TABLE [dbo].[Client] CHECK CONSTRAINT [FK_Client_User]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Car] FOREIGN KEY([CarId])
REFERENCES [dbo].[Car] ([Id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Car]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Client] FOREIGN KEY([Username])
REFERENCES [dbo].[Client] ([UserName])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Client]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Status] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Status] ([Id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Status]
GO
ALTER TABLE [dbo].[OrderContent]  WITH CHECK ADD  CONSTRAINT [FK_OrderContent_Option] FOREIGN KEY([OptionId])
REFERENCES [dbo].[Option] ([Id])
GO
ALTER TABLE [dbo].[OrderContent] CHECK CONSTRAINT [FK_OrderContent_Option]
GO
ALTER TABLE [dbo].[OrderContent]  WITH CHECK ADD  CONSTRAINT [FK_OrderContent_Order] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([Id])
GO
ALTER TABLE [dbo].[OrderContent] CHECK CONSTRAINT [FK_OrderContent_Order]
GO
ALTER TABLE [dbo].[Photo]  WITH CHECK ADD  CONSTRAINT [FK_Photo_Car] FOREIGN KEY([CarId])
REFERENCES [dbo].[Car] ([Id])
GO
ALTER TABLE [dbo].[Photo] CHECK CONSTRAINT [FK_Photo_Car]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Role]
GO
USE [master]
GO
ALTER DATABASE [CarShowRoom] SET  READ_WRITE 
GO
