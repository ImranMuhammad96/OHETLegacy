using Facebook;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OHET_Project.DAL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace OHET_Project.BBL
{
    public class FacebookData
    {

        private WebClient client = new WebClient();
        private string oauthUrl;
        private string accessToken;

        string AppId;
        string AppSecret;
        string kuglarzPage;

        public FacebookData()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"C:\Users\Marcin.Marcin-Komputer\Desktop\OHET I\ohet_project\OHET_Project\OHET_Project\stored.xml");

            AppId = doc.DocumentElement.SelectSingleNode("/stored/AppId").InnerText;
            AppSecret = doc.DocumentElement.SelectSingleNode("/stored/AppSecret").InnerText;
            kuglarzPage = doc.DocumentElement.SelectSingleNode("/stored/kuglarzPage").InnerText;

            oauthUrl = string.Format("https://graph.facebook.com/oauth/access_token?type=client_cred&client_id={0}&client_secret={1}", AppId, AppSecret);
            accessToken = client.DownloadString(oauthUrl).ToString();
            dynamic data = JObject.Parse(accessToken);
            accessToken = data.access_token.ToString();
        }

        public List<Post> GetFbPosts()
        { 
            var fbClient = new FacebookClient(accessToken);
            var version = fbClient.Version;

            var parameters = new Dictionary<string, object>();
            parameters["fields"] = "id,message,picture";

            dynamic result = fbClient.Get(kuglarzPage + "/posts", parameters);

            List<Post> postsList = new List<Post>();
            int count = result.data.Count;

            for(int i=0; i < result.data.Count; i++)
            {
                Post post = new Post();
                post.PostId = result.data[i].id;
                post.PostPictureUri = result.data[i].picture;
                post.PostMessage = result.data[i].message;

                var request = WebRequest.Create(post.PostPictureUri);
                using (var response = request.GetResponse())
                using (var stream = response.GetResponseStream())
                {
                    post.PostImage = Bitmap.FromStream(stream);
                }
                postsList.Add(post);
            }

            return postsList;
        }

    }
}