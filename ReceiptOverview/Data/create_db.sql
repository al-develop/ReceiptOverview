-- This script generates a sqlite database, which then needs to be moved to the /bin/Data/ directory.
-- use DBBrowser for SQLite to create the Database

BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "Position" (
	"ID"	INTEGER NOT NULL UNIQUE,
	"Date"	TEXT NOT NULL,
	"Total"	REAL NOT NULL,
	PRIMARY KEY("ID" AUTOINCREMENT)
);

CREATE TABLE IF NOT EXISTS "Entry" (
	"ID"	INTEGER NOT NULL UNIQUE,
	"Position_ID"	INTEGER NOT NULL,
	"Item"	TEXT NOT NULL,
	"Category"	TEXT,
	"Price"	REAL NOT NULL,
	PRIMARY KEY("ID" AUTOINCREMENT),
	FOREIGN KEY("Position_ID") REFERENCES "Position"("ID")
);

COMMIT;
