--LAST DATE EDITED: November 26, 2019
--LAST EDITED BY: Brenton Unger

--Use SPECIFIC database
--Could be changed in the future
USE iUngerTime
GO

----------------------------------------------------------------------------------------------
--DROP TABLES SHOULD NOT BE USED WHEN WE HAVE IMPORTANT DATA IN TABLES ALTER SHOULD BE USED.--
----------------------------------------------------------------------------------------------
DROP TABLE IF EXISTS PANTRY
GO

DROP TABLE IF EXISTS PERSON
GO

DROP TABLE IF EXISTS INGREDIENT
GO

----------------------------------------------------------------
--Columns should be edited later to connect to google sign in--
----------------------------------------------------------------
CREATE TABLE PERSON(
	UserID INT IDENTITY(1,1) PRIMARY KEY,
	Handle VARCHAR(25) NOT NULL,
	Email VARCHAR(64) UNIQUE NOT NULL
);

--------------------------------------------------
--THIS IS A TEMPORARY TABLE FOR PROOF OF CONCEPT--
--------------------------------------------------
CREATE TABLE INGREDIENT(
	IngredientID INT IDENTITY(1,1) PRIMARY KEY,
	CommonName VARCHAR(50) NOT NULL
);

---------------------------------------------------------------------------------
--PANTRY is a many -> one relationship of (many users have one pantry per user)--
---------------------------------------------------------------------------------
CREATE TABLE PANTRY(
	UserID INT FOREIGN KEY REFERENCES PERSON(UserID),
	IngredientID INT FOREIGN KEY REFERENCES INGREDIENT(IngredientID),
	Number INT DEFAULT 1,
	CommonName VARCHAR(50) NOT NULL,
	CONSTRAINT PkPantry PRIMARY KEY (UserID, IngredientID)
);