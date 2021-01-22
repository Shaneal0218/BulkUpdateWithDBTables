using BulkUpdateWithDBTables.DTO;
using BulkUpdateWithDBTables.Models;
using BulkUpdateWithDBTables.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BulkUpdateWithDBTables.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataRepository _data;
        private readonly UpdateRepository _update;
        public HomeController()
        {
            _data = new DataRepository();
            _update = new UpdateRepository();
        }

        public ActionResult Index()
        {
            return View();
        }
        [Route("getTableNames")]
        [HttpGet]
        public JsonResult GetTableNames()
        {
            List<string> names = _data.GetTableNames();
            return new JsonResult() { Data = names, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [Route("getColumnNames")]
        [HttpGet]
        public JsonResult GetColumnNames(string tname)
        {
            List<ColumnDTO> columns = _data.GetColumnNames(tname);
            return new JsonResult() { Data = columns, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [Route("bulkUpdate")]
        [HttpPost]
        public JsonResult BulkUpdate(List<ValuePair> values)
        {

            _update.BulkUpdate(values);
            var result = new { statusCode = 200, message = "Bulk update has finished!" };
            return new JsonResult() { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [Route("rowUpdate")]
        [HttpPost]
        public JsonResult RowUpdate(List<ValuePair> values)
        {

            _update.RowUpdate(values);
            var result = new { statusCode = 200, message = "Bulk update has finished!" };
            return new JsonResult() { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        [Route("getData")]
        [HttpGet]
        public JsonResult GetData(string tname)
        {

            List<object> data = _data.GetData(tname);
            return new JsonResult() { Data = data.Take(200), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}