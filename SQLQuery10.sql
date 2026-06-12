USE FarmersLeague;
GO

-- This finds any player with a blank 'IsStarting' box and sets it to False (0)
UPDATE Player SET IsStarting = 0 WHERE IsStarting IS NULL;

-- Let's do the same for IsAvailable just in case!
UPDATE Player SET IsAvailable = 0 WHERE IsAvailable IS NULL;