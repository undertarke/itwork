﻿using Dapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SoloDevApp.Repository.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading.Tasks;

namespace SoloDevApp.Repository.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> InsertAsync(T entity);

        Task<T> UpdateAsync(dynamic id, T entity);
        Task<T> UpdateAsyncHasArrayNull(dynamic id, T entity);


        Task<IEnumerable<T>> GetMultiByIdAsync(List<dynamic> listId);

        Task<T> GetSingleByListIdAsync(List<dynamic> listId);

        Task<T> GetSingleByIdAsync(dynamic id);

        Task<T> GetSingleByListConditionAsync(List<KeyValuePair<string, dynamic>> columns);

        Task<IEnumerable<T>> GetMultiByListConditionAsync(List<KeyValuePair<string, dynamic>> columns);
        Task<IEnumerable<T>> GetMultiByListConditionAndAsync(List<KeyValuePair<string, dynamic>> columns);

        Task<T> GetSingleByConditionAsync(string column, dynamic value);

        Task<IEnumerable<T>> GetMultiByConditionAsync(string column, dynamic value);

        Task<int> DeleteByIdAsync(List<dynamic> listId);

        Task<bool> CheckValidByConditionAsync(string column, dynamic value);
    }

    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        protected readonly string _connectionString = "";
        protected readonly string _table = "";

        public RepositoryBase(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
            _table = typeof(T).Name; // Lấy tên của đối tượng => tên bảng
        }
        protected string GenerateInsertQuery(T entity)
        {
            try
            {
                var props = entity.GetType().GetProperties();
                var columns = "";
                var values = "";

                var index = props[0].PropertyType.Name == "String" ? 0 : 1;
                for (var i = index; i < props.Length; i++)
                {
                    if (!props[i].PropertyType.IsPrimitive
                        && props[i].PropertyType.Name != "String"
                        && props[i].PropertyType.Name != "DateTime") continue;
                    columns += $"{props[i].Name}, ";
                    values += $"N'{props[i].GetValue(entity)}', ";
                }

                columns = columns.Remove(columns.Length - 2, 2);
                values = values.Remove(values.Length - 2, 2);

                var query = $"INSERT INTO {_table} ({columns}) VALUES ({values});";
                query += $"SELECT TOP 1 * FROM {_table} ORDER BY NgayTao DESC;";
                return query;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        protected string GenerateUpdateQuery(dynamic id, T entity)
        {
            try
            {
                var props = entity.GetType().GetProperties();
                var query = $"UPDATE {_table} SET ";
                for (var i = 1; i < props.Length; i++)
                {
                    if (props[i].GetValue(entity) != null)
                    {
                        query += $"{props[i].Name} = N'{props[i].GetValue(entity)}', ";
                    }
                }
                query = query.Remove(query.Length - 2, 2);
                query += $" WHERE Id = '{id}'";
                return query;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        protected SqlConnection CreateConnection()
        {
            var conn = new SqlConnection(_connectionString);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            return conn;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                using (var conn = CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@tableName", _table);
                    return await conn.QueryAsync<T>("GET_ALL_DATA", parameters, null, null, CommandType.StoredProcedure);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

      
        public virtual async Task<T> InsertAsync(T entity)
        {
            var query = GenerateInsertQuery(entity);
            using (var conn = CreateConnection())
            {
                try
                {
                    return await conn.QuerySingleOrDefaultAsync<T>(query, null, null, null, CommandType.Text);
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
            }
        }

        public virtual async Task<T> UpdateAsync(dynamic id, T entity)
        {
            string query = GenerateUpdateQuery(id, entity);
            using (var conn = CreateConnection())
            {
                try
                {
                    await conn.ExecuteAsync(query, null, null, null, CommandType.Text);
                    return entity;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
            }
        }
        public virtual async Task<T> UpdateAsyncHasArrayNull(dynamic id, T entity)
        {
            try
            {
                string columns = GenerateTableColumnsUpdateHasArrayNull(entity);
                using (var conn = CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@id", id);
                    parameters.Add("@tableName", _table);
                    parameters.Add("@listColumn", columns);
                    await conn.ExecuteAsync("UPDATE_DATA", parameters, null, null, CommandType.StoredProcedure);
                }
                return entity;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public virtual async Task<IEnumerable<T>> GetMultiByIdAsync(List<dynamic> listId)
        {
            try
            {
                using (var conn = CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@tableName", _table);
                    parameters.Add("@listId", JsonConvert.SerializeObject(listId));
                    return await conn.QueryAsync<T>("GET_DATA_BY_ID", parameters, null, null, CommandType.StoredProcedure);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public virtual async Task<T> GetSingleByIdAsync(dynamic id)
        {
            try
            {
                string query = $"SELECT * FROM {_table} WHERE Id = N'{id}'";
                using (var conn = CreateConnection())
                {
                    return await conn.QueryFirstOrDefaultAsync<T>(query, null, null, null, CommandType.Text);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public virtual async Task<T> GetSingleByListIdAsync(List<dynamic> listId)
        {
            try
            {
                string ids = JsonConvert.SerializeObject(listId);
                using (var conn = CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@tableName", _table);
                    parameters.Add("@listId", ids);
                    return await conn.QueryFirstOrDefaultAsync<T>("GET_DATA_BY_ID", parameters, null, null, CommandType.StoredProcedure);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        
        public virtual async Task<T> GetSingleByListConditionAsync(List<KeyValuePair<string, dynamic>> columns)
        {
            try
            {
                using (var conn = CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@tableName", _table);
                    parameters.Add("@listColumn", JsonConvert.SerializeObject(columns));
                    return await conn.QueryFirstOrDefaultAsync<T>("GET_SINGLE_DATA", parameters, null, null, CommandType.StoredProcedure);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<T>> GetMultiByListConditionAsync(List<KeyValuePair<string, dynamic>> columns)
        {
            try
            {
                using (var conn = CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@tableName", _table);
                    parameters.Add("@listColumn", JsonConvert.SerializeObject(columns));
                    return await conn.QueryAsync<T>("GET_MULTI_DATA", parameters, null, null, CommandType.StoredProcedure);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<T>> GetMultiByListConditionAndAsync(List<KeyValuePair<string, dynamic>> columns)
        {
            try
            {
                using (var conn = CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@tableName", _table);
                    parameters.Add("@listColumn", JsonConvert.SerializeObject(columns));
                    return await conn.QueryAsync<T>("GET_MULTI_DATA_AND", parameters, null, null, CommandType.StoredProcedure);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public async Task<T> GetSingleByConditionAsync(string column, dynamic value)
        {
            var columns = new List<KeyValuePair<string, dynamic>>();
            columns.Add(new KeyValuePair<string, dynamic>(column, value));
            try
            {
                using (var conn = CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@tableName", _table);
                    parameters.Add("@listColumn", JsonConvert.SerializeObject(columns));
                    return await conn.QueryFirstOrDefaultAsync<T>("GET_SINGLE_DATA", parameters, null, null, CommandType.StoredProcedure);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<T>> GetMultiByConditionAsync(string column, dynamic value)
        {
            var columns = new List<KeyValuePair<string, dynamic>>();
            columns.Add(new KeyValuePair<string, dynamic>(column, value));
            try
            {
                using (var conn = CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@tableName", _table);
                    parameters.Add("@listColumn", JsonConvert.SerializeObject(columns));
                    return await conn.QueryAsync<T>("GET_MULTI_DATA", parameters, null, null, CommandType.StoredProcedure);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public virtual async Task<int> DeleteByIdAsync(List<dynamic> listId)
        {
            try
            {
                using (var conn = CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@tableName", _table);
                    parameters.Add("@listId", JsonConvert.SerializeObject(listId));
                    return await conn.ExecuteAsync("DELETE_DATA_BY_ID", parameters, null, null, CommandType.StoredProcedure);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public async Task<bool> CheckValidByConditionAsync(string column, dynamic value)
        {
            var columns = new List<KeyValuePair<string, dynamic>>();
            columns.Add(new KeyValuePair<string, dynamic>(column, value));
            try
            {
                using (var conn = CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@tableName", _table);
                    parameters.Add("@listColumn", JsonConvert.SerializeObject(columns));
                    return await conn.ExecuteScalarAsync<bool>("CHECK_VALID", parameters, null, null, CommandType.StoredProcedure);
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        protected string GenerateTableColumnsInsert(T entity)
        {
            List<KeyValuePair<string, dynamic>> columns = new List<KeyValuePair<string, dynamic>>();
            PropertyInfo[] props = entity.GetType().GetProperties();

            // Nếu cột id có kiểu string thì thêm vào (index = 0)
            // Ngược lại bỏ qua (index = 1)
            int index = props[0].PropertyType.Name == "String" ? 0 : 1;
            for (int i = index; i < props.Length; i++)
            {
                columns.Add(new KeyValuePair<string, dynamic>(props[i].Name, props[i].GetValue(entity)));
            }
            return JsonConvert.SerializeObject(columns);
        }

        //protected string GenerateTableColumnsUpdate(T entity)
        //{
        //    List<KeyValuePair<string, dynamic>> columns = new List<KeyValuePair<string, dynamic>>();
        //    PropertyInfo[] props = entity.GetType().GetProperties();
        //    for (int i = 1; i < props.Length; i++)
        //    {
        //        var value = props[i].GetValue(entity);
        //        if(value != null && value.ToString() != "[]")
        //            columns.Add(new KeyValuePair<string, dynamic>(props[i].Name, value));
        //    }
        //    return JsonConvert.SerializeObject(columns);
        //}

        protected string GenerateTableColumnsUpdate(T entity)
        {
            List<KeyValuePair<string, dynamic>> columns = new List<KeyValuePair<string, dynamic>>();
            PropertyInfo[] props = entity.GetType().GetProperties();
            for (int i = 1; i < props.Length; i++)
            {
                var value = props[i].GetValue(entity);
                if (value != null && value.ToString() != "[]" )
                    columns.Add(new KeyValuePair<string, dynamic>(props[i].Name, value));
            }
            return JsonConvert.SerializeObject(columns);
        }


        public string GenerateTableColumnsUpdateHasArrayNull(T entity)
        {
            List<KeyValuePair<string, dynamic>> columns = new List<KeyValuePair<string, dynamic>>();
            PropertyInfo[] props = entity.GetType().GetProperties();
            for (int i = 1; i < props.Length; i++)
            {
                var value = props[i].GetValue(entity);
                if (value != null)
                    columns.Add(new KeyValuePair<string, dynamic>(props[i].Name, value));
            }
            return JsonConvert.SerializeObject(columns);
        }
    }
}