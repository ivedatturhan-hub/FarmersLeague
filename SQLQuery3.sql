-- 1. Delete the old, incomplete table
DROP TABLE IF EXISTS Player;

-- 2. Build the complete table with all your stats!
CREATE TABLE Player (
    PlayerID INT IDENTITY(1,1) PRIMARY KEY,
    TeamID INT, 
    Name NVARCHAR(100) NOT NULL,
    Age INT,
    Position NVARCHAR(50),
    BaseAttack INT,
    BaseDefence INT,
    MarketValue DECIMAL(18,2),
    IsStarting BIT,
    IsAvailable BIT DEFAULT 1,
    Condition INT,
    Happiness INT,    -- Here is your missing Happiness column!
    Composure INT,
    Aggression INT,
    SeasonGoals INT,
    SeasonAssists INT,
    YellowCards INT,
    RedCards INT
);