using Dapper;
using Npgsql;
using Otus.CqrsUpdater.Models.Domain;
using System.Data;

namespace Otus.CqrsUpdater.Services
{
    public class UserService
    {
        private readonly string _connectionString;

        public UserService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("default");
        }
        public async Task SaveAsync(UserEvent user)
        {


            if (await ExistsAsync(user.Id))
            {
                using (var con = CreateConnection())
                {
                    const string query = "update into public.users(id, fullName, documents) " +
               "values(@Id, @FullName, @Documents)";
                    var data = await con.ExecuteAsync(
                        query,
                        new { user.Id, user.FullName, Documents = string.Join(", ", user.Documents.Select(x => x.n) });
                }
            }
            else
            {
                using (var con = CreateConnection())
                {
                    const string query = "insert into public.users(id, fullName, documents) " +
               "values(@Id, @FullName, @Documents)";
                    var data = await con.ExecuteAsync(
                        query, 
                        new { user.Id,  user.FullName,Documents=string.Join(", " ,user.Documents.Select(x=>x.n) });
                }
            }
        }


        public async Task<List<UserEvent>> GetAllAsync()
        {
            const string query = "select id, fullName, documents from public.users";
            using (var con = CreateConnection())
            {
                var data = await con.QueryAsync<UserEvent>(query);
                return data.ToList();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            const string query = "select id from public.users where id = @Id";
            using (var con = CreateConnection())
            {
                var data = await con.QueryFirstOrDefaultAsync<int?>(query, new { Id = id });
                return data != null;
            }
        }

        public IDbConnection CreateConnection()
    => new NpgsqlConnection(_connectionString);
    }
}
