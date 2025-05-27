using Microsoft.Data.Sqlite;
using Dapper;
using EvidenceSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EvidenceSystem.Data
{
    public class EvidenceDbContext
    {
        private readonly string _connectionString = "Data Source=evidence.db";

        public EvidenceDbContext()
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Execute(@"
                CREATE TABLE IF NOT EXISTS Evidence (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Description TEXT
                );
            ");
        }

        public async Task<List<Evidence>> GetAllAsync()
        {
            using var connection = new SqliteConnection(_connectionString);
            var result = await connection.QueryAsync<Evidence>("SELECT * FROM Evidence");
            return result.AsList();
        }

        public async Task<Evidence?> GetByIdAsync(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<Evidence>("SELECT * FROM Evidence WHERE Id = @id", new { id });
        }

        public async Task AddAsync(Evidence evidence)
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.ExecuteAsync("INSERT INTO Evidence (Name, Description) VALUES (@Name, @Description)", evidence);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            var affected = await connection.ExecuteAsync("DELETE FROM Evidence WHERE Id = @id", new { id });
            return affected > 0;
        }
    }
}