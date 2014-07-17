using DTS.DataTables.MVC;
using dttests.Helpers;
using dttests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace dttests.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IGRDMSRepository repo, ISegmentEditsRepository editRepo) : base(repo, editRepo) { }

        public ActionResult Index(string id = "")
        {
            if (id == "SegmentEdits")
            {
                ViewBag.Columns = GetColumns(true);
            }
            else
            {
                ViewBag.Columns = GetColumns();
            }

            ViewBag.FipsChoices = _repo.All().GroupBy(x => x.FIPS).Select(x => x.Key).OrderBy(x => x);
            ViewBag.TableChoices = new string[] { "Segments", "SegmentsOrig", "GRDMS", "SegmentEdits" };
            ViewBag.SelectedTable = id;
            return View();
        }

        [HttpPost]
        public JsonResult Get([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            bool isSegmentEdit = requestModel.Columns.Any(x => x.Name == "ChangeId");
            if (!isSegmentEdit)
            {
                var data = _repo.All();
                var filteredResults = data.OrderBy(GetOrder(requestModel.Columns));
                foreach (Column col in requestModel.Columns.Where(x => !string.IsNullOrWhiteSpace(x.Search.Value)))
                {
                    filteredResults = filteredResults.FilterByValue(col.Name, col.Search.Value);
                }
                var pagedResults = filteredResults.Skip(requestModel.Start).Take(requestModel.Length);
                var result = Json(new DataTablesResponse(requestModel.Draw, pagedResults, filteredResults.Count(), data.Count()), JsonRequestBehavior.AllowGet);
                return result;

            }
            else
            {
                var data = _editRepo.All();
                var filteredResults = data.OrderBy(GetOrder(requestModel.Columns));
                foreach (Column col in requestModel.Columns.Where(x => !string.IsNullOrWhiteSpace(x.Search.Value)))
                {
                    filteredResults = filteredResults.FilterByValue(col.Name, col.Search.Value);
                }
                var pagedResults = filteredResults.Skip(requestModel.Start).Take(requestModel.Length);
                var result = Json(new DataTablesResponse(requestModel.Draw, pagedResults, filteredResults.Count(), data.Count()), JsonRequestBehavior.AllowGet);
                return result;
            }
        }

        public ActionResult Test()
        {
            ViewBag.Columns = GetColumns();
            ViewBag.Total = _repo.All().Count();
            return View();
        }

        private IList<ColumnHeader> GetColumns(bool getEditColumns = false) 
        {
            var list = new List<ColumnHeader>();
            if (!getEditColumns)
            {
                foreach (SegmentColumns item in Enum.GetValues(typeof(SegmentColumns)))
                {
                    list.Add(new ColumnHeader()
                    { 
                        data = item.ToString(), 
                        name = item.ToString(), 
                        target = list.Count(), 
                        //visible = item.HasAttribute<DataTablesColumnAttribute>() ? item.GetAttribute<DataTablesColumnAttribute>().Visible : true
                        visible = item.ToString() == "SegmentKey" ? false : true
                    });
                }
            }
            else
            {
                foreach (SegmentEditColumns item in Enum.GetValues(typeof(SegmentEditColumns)))
                {
                    list.Add(new ColumnHeader() { data = item.ToString(), name = item.ToString(), target = list.Count(), visible = true });
                }
            }
            return list;
        }

        private string GetOrder(ColumnCollection columns)
        {
            var orderBy = string.Join(", ", columns.GetSortedColumns()
                .Select(x => string.Format("{0} {1}", x.Data, x.SortDirection.ToString().ToLower().Contains("asc") ? "ASC" : "DESC")
            ).ToArray());
            return orderBy;
        }
    }

    public enum SegmentColumns
    {
        [DataTablesColumn(false)]
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

    public enum SegmentEditColumns
    {
        SegmentKey
        , ChangeId
        , ChangeType
        , FIPS
        , ROUTE
        , SEGMID
        , FieldName
        , OldValue
        , NewValue
        , Timestamp
        , UserName
    }

}
