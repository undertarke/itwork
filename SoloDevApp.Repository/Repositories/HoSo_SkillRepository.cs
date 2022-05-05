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
    public interface IHoSo_SkillRepository : IRepository<HoSo_Skill>
    {
       /* Task<HoSo_Skill> Off_CheckUserFace(string idFace);*/

    }

    public class HoSo_SkillRepository : RepositoryBase<HoSo_Skill>, IHoSo_SkillRepository
    {
        private readonly IConfiguration _configuration;

        public HoSo_SkillRepository(IConfiguration configuration)
            : base(configuration)
        {
        }


     /*   public async Task<HoSo_Skill> Off_CheckUserFace(string idFace)
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