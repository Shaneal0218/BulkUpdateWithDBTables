using BulkUpdateWithDBTables.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BulkUpdateWithDBTables.Repository.IRepository
{
    public interface IUpdateRepository
    {
        void BulkUpdate(List<ValuePair> values);
        void RowUpdate(List<ValuePair> values);
    }
}