using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTS.DataTables.MVC;
using dttests.Models;

namespace dttests.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IRepository repo) : base(repo) { }

        public ActionResult Index()
        {
            ViewBag.Columns = GetColumns();
            ViewBag.FipsChoices = _repo.All().GroupBy(x => x.FIPS).Select(x => x.Key).OrderBy(x => x);
            ViewBag.TableChoices = new string[] { "Segments", "Segments Orig", "GRDMS" };
            return View();
        }

        [HttpPost]
        public JsonResult Get([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            // get all records
            var segments = _repo.All();
            var filteredResults = segments;

            // filter fips
            List<Column> columns = requestModel.Columns.ToList();
            if (!string.IsNullOrWhiteSpace(columns[1].Search.Value))
            {
                filteredResults = filteredResults.Where(x => x.FIPS == columns[1].Search.Value);
            }


            // order the results get all cols where IsOrderable is true and order these but the OrderNumber
            //if (columns.Any(x => x.IsOrdered))
            //{
            //    var orderedCols = columns.Where(x => x.IsOrdered == true)
            //        .OrderBy(x => x.OrderNumber)
            //        .Select(x => new { propertyName = x.Data, sortDir = x.SortDirection }).ToArray();

            //    filteredResults = filteredResults.OrderBy(orderedCols);

                
            //}

            // loop throu



            // page filteredResults
            var pagedResults = filteredResults.Skip(requestModel.Start).Take(requestModel.Length);

            // json-ize the result
            var result = Json(new DataTablesResponse(requestModel.Draw, pagedResults, filteredResults.Count(), segments.Count()), JsonRequestBehavior.AllowGet);
            return result;
        }

        public ActionResult Test()
        {
            ViewBag.Columns = GetColumns();
            ViewBag.Total = _repo.All().Count();
            return View();
        }


        private IList<ColumnHeader> GetColumns() 
        {
            var list = new List<ColumnHeader>();
            foreach (SegmentColumns item in Enum.GetValues(typeof(SegmentColumns)))
            {
                list.Add(new ColumnHeader() { data = item.ToString(), name = item.ToString(), target = list.Count(), visible = true });
            }
            return list;
        }
    }

    public enum SegmentColumns
    {
        SegmentKey
        , FIPS
        , ROUTE
        , SEGMID
        , LENGTH
        , UPDATEYR
        , FIPSCOUNTY
        , FUNCCLASSID
        , GOVLEVEL
        , ADMINCLASS
        , POPULATION
        , URBAN
        , NAAQSID
        , NHSDESIG
        , SPECIALSYS
        , ACCESS
        , TRKRESTRICT
        , PRIIRI
        , PRIIRIDATE
        , PRIPSI
        , PROJYR
        , BUILTYR
        , INSPYR
        , PRITREATMENTDEPTH
        , PRISURF
        , PRISURFWD
        , THRULNQTY
        , PRITHRULNWD
        , OPERATION
        , RRXID
        , STRID
        , REGION
        , TPRID
        , TERRAIN
        , FORESTROUTE
        , ROUTESIGN
        , ROUTESIGNQUAL
        , JURSPLIT
        , ROUTENAME
        , FROMFEATURE
        , TOFEATURE
        , SEGMDIR
        , SEGMPREFIX
        , GISID
        , COUNTSTATIONID
        , PRIYRREHAB
        , ISDIVIDED
        , LRSROUTE
        , FROMMEAS
        , TOMEAS
    }

}
