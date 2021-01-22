using BulkUpdateWithDBTables.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BulkUpdateWithDBTables.Repository.IRepository
{
    public interface IDataRepository
    {
        List<string> GetTableNames();
        List<ColumnDTO> GetColumnNames(string tname);
        List<object> GetData(string tname);
    }
}