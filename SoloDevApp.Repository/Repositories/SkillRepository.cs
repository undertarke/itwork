using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using SoloDevApp.Repository.Infrastructure;
using SoloDevApp.Repository.Models;

namespace SoloDevApp.Repository.Repositories
{
    public interface ISkillRepository : IRepository<Skill>
    {
       /* Task<Skill> Off_CheckUserFace(string idFace);*/

    }

    public class SkillRepository : RepositoryBase<Skill>, ISkillRepository
    {
        private readonly IConfiguration _configuration;

        public SkillRepository(IConfiguration configuration)
            : base(configuration)
        {
        }


     /*   public async Task<Skill> Off_CheckUserFace(string idFace)
        {

            string query = $"SELECT * FROM NguoiDung WHERE FacebookId = '{idFace}' AND DaXoa = 0";

            using (var conn = CreateConnection())
            {
                try
                {
                    return await conn.QueryFirstOrDefaultAsync<NguoiDung>(query, null, null, null, CommandType.Text);
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
            }
        }*/

  
    }
}