using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;

namespace OHET_Project.DAL
{
    public class Post
    {
        public string PostId { get; set; }
        public string PostStory { get; set; }
        public string PostMessage { get; set; }
        public string PostPictureUri { get; set; }
        public Image PostImage { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
    }

}