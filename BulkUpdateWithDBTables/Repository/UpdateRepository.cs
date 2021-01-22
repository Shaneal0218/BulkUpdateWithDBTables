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
    public class UpdateRepository : IUpdateRepository
    {
        public IDbConnection GetConnection()
        {
            var connectionString = WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            return new SqlConnection(connectionString);
        }

        public void BulkUpdate(List<ValuePair> values)
        {
            using (IDbConnection connection = GetConnection())
            {
                string sql = CreateSQLQueryStringToBulkUpdate(values);
                connection.Execute(sql);
            }
        }
        public void RowUpdate(List<ValuePair> values)
        {
            using (IDbConnection connection = GetConnection())
            {
                string sql = CreateSQLQueryStringToRowUpdate(values);
                connection.Execute(sql);
            }
        }

        private string CreateSQLQueryStringToBulkUpdate(List<ValuePair> values)
        {
            var tname = values[0].Value;
            var first = false;
            string sql = @"UPDATE " + tname;

            for (var i = 1; i < values.Count(); i++)
            {
                var name = values[i].Property;
                var value = values[i].Value;
                if (first == false)
                {
                    sql = sql + " SET " + name + " = '" + value + "'";
                    first = true;
                }
                else
                {
                    sql = sql + ", " + name + " = '" + value + "'";
                }
            }
            return sql;
        }
        private string CreateSQLQueryStringToRowUpdate(List<ValuePair> values)
        {
            var tname = values[0].Value;
            var first = false;
            string sql = @"UPDATE " + tname;

            for (var i = 2; i < values.Count(); i++)
            {
                var name = values[i].Property;
                var value = values[i].Value;
                if (first == false)
                {
                    sql = sql + " SET " + name + " = '" + value + "'";
                    first = true;
                }
                else
                {
                    sql = sql + ", " + name + " = '" + value + "'";
                }
            }
            sql = sql + " WHERE " + values[1].Property + " = '" + values[1].Value + "'";
            return sql;
        }
    }
}