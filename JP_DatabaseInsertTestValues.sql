-----------------------
--Inserts into Person--
-----------------------
INSERT INTO PERSON 
	(Handle, Email)
VALUES
	('iUngerTime', 'brent.unger@oit.edu'),
	('jDod', 'jareth.dodson@oit.edu'),
	('tEb', 'thomas.eberhart@oit.edu'),
	('nSac', 'nicholas.sack@oit.edu');

---------------------------
--Inserts into Ingredient--
---------------------------
INSERT INTO INGREDIENT 
	(CommonName)
VALUES
	('Milk'),
	('Eggs'),
	('Cookies'),
	('Jolly Ranchers');

-----------------------
--Inserts into Pantry--
-----------------------
INSERT INTO PANTRY 
	(UserID)
VALUES
	(1),
	(2),
	(3),
	(4)
	
-----------------------------------
--Inserts into PANTRY_INGREDIENTS--
-----------------------------------
INSERT INTO PANTRY_INGREDIENTS
	(IngredientID, PantryID, Number, CommonName)
VALUES
	(1, 1, 2, 'Milk'),
	(2, 1, 2, 'Eggs'),
	(3, 2, 2, 'Cookies'),
	(4, 2, 2, 'Jolly Ranchers'),
	(1, 3, 2, 'Milk'),
	(2, 3, 2, 'Eggs'),
	(3, 4, 2, 'Cookies'),
	(4, 4, 2, 'Jolly Ranchers');
