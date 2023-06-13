create table [User](
	ID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[Login] VARCHAR(50) NOT NULL,
	[Password] VARCHAR(50) NOT NULL,
	Email VARCHAR(50) NOT NULL,
	RegisterDate smalldatetime NOT NULL,
)

create table LogType(
	ID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[Type] VARCHAR(50) NOT NULL,
)

create table OrderType(
	ID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[Type] VARCHAR(50) NOT NULL,
)

create table [Log](
	ID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	FK_LogType INT REFERENCES LogType (ID) NOT NULL,
	FK_User INT REFERENCES [User] (ID) NOT NULL,
	[Message] VARCHAR(50) NOT NULL,
	SendDate smalldatetime NOT NULL,
)

create table SpotOrder(
	ID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	FK_OrderType INT REFERENCES OrderType (ID) NOT NULL,
	FK_User INT REFERENCES [User] (ID) NOT NULL,
	FirstCoin VARCHAR(50) NOT NULL,
	SecondCoin VARCHAR(50) NOT NULL,
	Amount decimal(19, 5) NOT NULL,
	Price decimal(19, 5) NOT NULL,
	OrderDate smalldatetime NOT NULL,
)

create table TakeProfitStopLossOrder(
	ID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	FK_User INT REFERENCES [User] (ID) NOT NULL,
	SellCoin VARCHAR(50) NOT NULL,
	BuyCoin VARCHAR(50) NOT NULL,
	Amount decimal(19, 5) NOT NULL,
	Price decimal(19, 5) NOT NULL,
	OrderDate smalldatetime NOT NULL,
)

create table ArbitrageOrder(
	ID INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	FK_User INT REFERENCES [User] (ID) NOT NULL,
	SellCoin VARCHAR(50) NOT NULL,
	BuyCoin VARCHAR(50) NOT NULL,
	Amount decimal(19, 5) NOT NULL,
	BuyPrice decimal(19, 5) NOT NULL,
	SellPrice decimal(19, 5) NOT NULL,
	ProfitPercentage decimal(19, 5) NOT NULL,
	FirstExchange VARCHAR(50) NOT NULL,
	SecondExchange VARCHAR(50) NOT NULL,
	OrderDate smalldatetime NOT NULL,
)