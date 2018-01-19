using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace IO
{
    class Zadanie14
    {

        public static void execute()
        {
            getWebPage();
        }

        static async void getWebPage()
        {
            string pageCode = "";
            WebClient webClient = new WebClient();
            pageCode = await webClient.DownloadStringTaskAsync("http://www.feedforall.com/sample.xml");

            Console.Write(pageCode);
        }
        
    }
}
