CREATE TABLE Team (
    -- IDENTITY(1,1) tells SQL to count the IDs automatically (1, 2, 3...)
    TeamID INT IDENTITY(1,1) PRIMARY KEY,
    
    LeagueID INT NOT NULL,
    TeamName NVARCHAR(100) NOT NULL,
    
    -- DECIMAL(18,2) means an 18-digit number with 2 decimal places for money
    Budget DECIMAL(18,2) NOT NULL,
    
    Points INT NOT NULL,
    
    -- We save Tactics as text. Your C# Enum will translate it later!
    Tactics NVARCHAR(50) NOT NULL,
    
    -- BIT is SQL's version of a boolean (1 = True, 0 = False)
    IsUserControlled BIT NOT NULL
);