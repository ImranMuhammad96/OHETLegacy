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

        public ActionResult ShowClass(int id)
        {
            var img = db.classes.Where(x => x.IDClass == id).Select(y => y.image).SingleOrDefault();

            return img != null ? File(img, "image/jpg") : null;
        }

        public ActionResult ShowPost(int id)
        {
            var img = db.posts.Where(x => x.IDPost == id).Select(y => y.image).SingleOrDefault();

            return img != null ? File(img, "image/jpg") : null;
        }

        public ActionResult ShowSubpost(int id)
        {
            var img = db.subposts.Where(x => x.IDSubpost == id).Select(y => y.image).SingleOrDefault();

            return img != null ? File(img, "image/jpg") : null;
        }
    }
}