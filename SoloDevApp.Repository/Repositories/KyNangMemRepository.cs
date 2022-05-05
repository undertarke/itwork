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
    public interface IKyNangMemRepository : IRepository<KyNangMem>
    {
       /* Task<KyNangMem> Off_CheckUserFace(string idFace);*/

    }

    public class KyNangMemRepository : RepositoryBase<KyNangMem>, IKyNangMemRepository
    {
        private readonly IConfiguration _configuration;

        public KyNangMemRepository(IConfiguration configuration)
            : base(configuration)
        {
        }


     /*   public async Task<KyNangMem> Off_CheckUserFace(string idFace)
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