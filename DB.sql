CREATE DATABASE API;
USE API;
CREATE TABLE TblCrudNetCore(ID INT IDENTITY(1,1) PRIMARY KEY,Name VARCHAR(100),Email VARCHAR(100),IsActive INT,CreatedOn Datetime);
SELECT * FROM TblCrudNetCore;