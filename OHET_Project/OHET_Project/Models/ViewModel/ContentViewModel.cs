using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OHET_Project.Models.models;

namespace OHET_Project.Models.ViewModel
{
	public class ContentViewModel
	{

        public IEnumerable<FavCon> FavCons { get; set; }

        public IEnumerable<Content> Contents { get; set; }

    }
}