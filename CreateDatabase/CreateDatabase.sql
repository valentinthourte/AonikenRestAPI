IF not EXISTS (SELECT name FROM master.sys.databases WHERE name = N'Aoniken')
BEGIN
CREATE DATABASE Aoniken
END
GO


use [Aoniken]
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'POSTS')
CREATE TABLE POSTS (
	ID_POST int IDENTITY(1,1),
	STATUS int NOT NULL,
	AUTHOR varchar(40) NOT NULL,
	PRIMARY KEY (ID_POST)
)
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'USERS')
CREATE TABLE USERS (
	ID_USER int IDENTITY(1,1),
	USERNAME varchar(30) NOT NULL,
	PASS varchar(30) NOT NULL,
	USER_TYPE int NOT NULL,
	PRIMARY KEY (ID_USER)
)
GO

INSERT INTO USERS (USERNAME, PASS, USER_TYPE) VALUES ('writer', '1234', 1)
INSERT INTO USERS (USERNAME, PASS, USER_TYPE) VALUES ('editor', '1234', 2)
GO
