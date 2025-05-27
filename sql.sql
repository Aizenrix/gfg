using System;
using System.Data.SQLite;

class Program
{
    static void Main()
    {
        using var connection = new SQLiteConnection("Data Source=database.db");
        connection.Open();

        string sql = "CREATE TABLE IF NOT EXISTS Test (Id INTEGER PRIMARY KEY, Name TEXT)";
        using var command = new SQLiteCommand(sql, connection);
        command.ExecuteNonQuery();

        Console.WriteLine("Таблица создана!");
    }
}

-- Роли пользователей
CREATE TABLE Roles (
    RoleId INTEGER PRIMARY KEY AUTOINCREMENT,
    RoleName TEXT NOT NULL UNIQUE
);

-- Пользователи
CREATE TABLE Users (
    UserId INTEGER PRIMARY KEY AUTOINCREMENT,
    FullName TEXT NOT NULL,
    Email TEXT NOT NULL UNIQUE,
    PasswordHash TEXT NOT NULL,
    RoleId INTEGER,
    FOREIGN KEY (RoleId) REFERENCES Roles(RoleId)
);

-- Один к одному: лог входа
CREATE TABLE AccessLog (
    LogId INTEGER PRIMARY KEY AUTOINCREMENT,
    UserId INTEGER UNIQUE,
    LastLogin TEXT,
    IPAddress TEXT,
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

-- Уголовные дела
CREATE TABLE Cases (
    CaseId INTEGER PRIMARY KEY AUTOINCREMENT,
    CaseNumber TEXT NOT NULL UNIQUE,
    Description TEXT
);

-- Доказательства
CREATE TABLE Evidence (
    EvidenceId INTEGER PRIMARY KEY AUTOINCREMENT,
    Title TEXT,
    Description TEXT,
    FilePath TEXT,
    DateAdded TEXT DEFAULT (datetime('now')),
    CaseId INTEGER,
    AddedBy INTEGER,
    FOREIGN KEY (CaseId) REFERENCES Cases(CaseId),
    FOREIGN KEY (AddedBy) REFERENCES Users(UserId)
);

-- История действий
CREATE TABLE EvidenceHistory (
    HistoryId INTEGER PRIMARY KEY AUTOINCREMENT,
    EvidenceId INTEGER,
    UserId INTEGER,
    Action TEXT,
    ActionDate TEXT DEFAULT (datetime('now')),
    FOREIGN KEY (EvidenceId) REFERENCES Evidence(EvidenceId),
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

-- Теги
CREATE TABLE Tags (
    TagId INTEGER PRIMARY KEY AUTOINCREMENT,
    TagName TEXT UNIQUE
);

-- Связь многие ко многим
CREATE TABLE Evidence_Tags (
    EvidenceId INTEGER,
    TagId INTEGER,
    PRIMARY KEY (EvidenceId, TagId),
    FOREIGN KEY (EvidenceId) REFERENCES Evidence(EvidenceId),
    FOREIGN KEY (TagId) REFERENCES Tags(TagId)
);
INSERT INTO Roles (RoleName) VALUES ('Investigator'), ('Judge'), ('Notary');

INSERT INTO Users (FullName, Email, PasswordHash, RoleId)
VALUES ('Иван Петров', 'ivan@example.com', 'some_hash', 1);

INSERT INTO Cases (CaseNumber, Description)
VALUES ('UD-2025-001', 'Кража со взломом');

INSERT INTO Evidence (Title, Description, FilePath, CaseId, AddedBy)
VALUES ('Отпечатки пальцев', 'Следы на двери', 'files/print1.jpg', 1, 1);