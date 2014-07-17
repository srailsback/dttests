using dttests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dttests.Controllers
{
    public class BaseController : Controller
    {
        protected IGRDMSRepository _repo;
        protected ISegmentEditsRepository _editRepo;
        public BaseController(IGRDMSRepository repo, ISegmentEditsRepository editRepo)
        {
            this._repo = repo;
            this._editRepo = editRepo;
        }

    }
}
