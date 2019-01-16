using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OHET_Project.Controllers
{
    public class ImageController : Controller
    {
        private Persistence.DbContext db = new Persistence.DbContext();

        public ActionResult Show(int id)
        {
            var img = db.classes.Where(x => x.IDClass == id).Select(y => y.image).SingleOrDefault();

            return img != null ? File(img, "image/jpg") : null;
        }
    }
}