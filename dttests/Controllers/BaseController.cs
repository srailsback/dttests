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
        protected IRepository _repo;
        public BaseController(IRepository repo)
        {
            this._repo = repo;
        }

    }
}
