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
            return View();
        }

        [HttpPost]
        public JsonResult Get([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            //var requestModel = new DataTablesRequest();

            // get all records
            var segments = _repo.All();
            var filteredResults = segments;

            // this is so we're not sending back everything;
            filteredResults = filteredResults.Where(x => x.FIPS == "069").Take(20);

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
