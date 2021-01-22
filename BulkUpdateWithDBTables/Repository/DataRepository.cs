using BulkUpdateWithDBTables.DTO;
using BulkUpdateWithDBTables.Models;
using BulkUpdateWithDBTables.Repository.IRepository;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace BulkUpdateWithDBTables.Repository
{
    public class DataRepository : IDataRepository
    {
        public IDbConnection GetConnection()
        {
            var connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            return new SqlConnection(connectionString);
        }

        public List<string> GetTableNames()
        {
            using (IDbConnection connection = GetConnection())
            {
                string sql = @"SELECT TABLE_NAME
                               FROM INFORMATION_SCHEMA.TABLES
                               WHERE TABLE_TYPE = 'BASE TABLE'";

                List<string> names = connection.Query<string>(sql).ToList();
                return names;
            }
        }
      
        public List<ColumnDTO> GetColumnNames(string tname)
        {
            using (IDbConnection connection = GetConnection())
            {
                string sql = @"SELECT COLUMN_NAME,
                                      IS_NULLABLE, 
                                      DATA_TYPE, 
                                      CHARACTER_MAXIMUM_LENGTH
                               FROM INFORMATION_SCHEMA.COLUMNS
                               WHERE TABLE_NAME = @tname";

                List<ColumnDTO> columns = connection.Query<ColumnDTO>(sql, new { tname = tname }).ToList();
                foreach (ColumnDTO c in columns)
                {
                    if (c.DATA_TYPE == "smallint" || c.DATA_TYPE == "bigint")
                    {
                        c.DATA_TYPE = "int";
                    }
                    if (c.DATA_TYPE == "varchar" || c.DATA_TYPE == "nvarchar" || c.DATA_TYPE == "nchar"
                        || c.DATA_TYPE == "ntext" || c.DATA_TYPE == "image" || c.DATA_TYPE == "char")
                    {
                        c.DATA_TYPE = "string";
                    }
                    if(c.DATA_TYPE == "float" || c.DATA_TYPE == "money")
                    {
                        c.DATA_TYPE = "float";
                    }
                }
                return columns;
            }
        }
        public List<object> GetData(string tname)
        {
            using (IDbConnection connection = GetConnection())
            {
                string sql = @"SELECT * FROM " + tname;

                List<object> data = connection.Query<object>(sql).ToList();
                return data;
            }
        }
    }
}