using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocialMediaDesktopBackgroundRotator
{
    class Program
    {
        const string GAG_URL = "http://9gag.com/";
        const string GAG_FIRST_POST_XPATH = "//div[@class='badge-post-container post-container']/a";
        const string GAG_POST_IMAGE_URL_XPATH = "//img[@class='badge-item-img']";
        const string GAG_NEXT_POST_XPATH = "//div[@class='post-nav']/a";

        public static void Main()
        {

            HtmlAgilityPack.HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = web.Load(GAG_URL);

            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(GAG_FIRST_POST_XPATH);
            
            string nextPost = GAG_URL + nodes[0].GetAttributeValue("href", "");

            while (true) {
                doc = web.Load(nextPost);

                try
                {
                    nodes = doc.DocumentNode.SelectNodes(GAG_POST_IMAGE_URL_XPATH);
                    string image = nodes[0].GetAttributeValue("src", "");

                    Wallpaper.Set(new Uri(image, false), Wallpaper.Style.Centered);

                    Thread.Sleep(10000);
                }
                catch (Exception e) { }

                nodes = doc.DocumentNode.SelectNodes(GAG_NEXT_POST_XPATH);
                nextPost = GAG_URL + nodes[1].GetAttributeValue("href", "");
            }
        }
    }
}
