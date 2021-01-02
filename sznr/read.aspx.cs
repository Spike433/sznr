using Itenso.TimePeriod;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sznr
{
    public partial class read : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            NameValueCollection n = Request.QueryString;

            const int delay = 30;
            
            if (n.HasKeys())
            {
                string k = n.GetKey(0);
                string v = n.Get(0);

                string k1 = n.GetKey(1);
                string v1 = n.Get(1);

                string k2 = n.GetKey(2);
                string v2 = n.Get(2);

                //?param1=20190614T041236&param2=car&api_key=M6N015C8W6ALJ
                //C:\Users\PC\source\repos\SZNR\sznr

                if (v2 == "M6N015C8W6ALJ")
                {

                    Response.Write("Received DateTime is " + v+ "<br/> Received data is " + v1);            
                                      
                    
                    String formatFromAndroid = "yyyyMMddTHHmmss";
                    String formatFOrAndroidStudio= "dd.MM.yyyy.HH.mm.ss";

                DateTime dateTime=DateTime.ParseExact(v, formatFromAndroid,null );

                    ITimePeriod timePeriod = new TimeBlock(dateTime);

                    String dateTimeString = dateTime.ToString(formatFOrAndroidStudio);

                    String path1 = @"C:\Users\PC\source\repos\SZNR\sznr\data.txt";

                    TimePeriodCollection timePeriods = new TimePeriodCollection();



                    using (StreamReader reader = new StreamReader(path1, true))
                    {
                        while (true)
                        {
                            string line = reader.ReadLine();
                            if (line == null)
                            {
                                break;
                            }

                            DateTime parsedLine= DateTime.ParseExact(line, formatFOrAndroidStudio, null);

                            timePeriods.Add(new TimeBlock(parsedLine, Duration.Minutes(delay)));

                            if (timePeriods.Last().OverlapsWith(timePeriod))
                            {
                                Response.Write("KUME IMAMO GA");
                            }

                            
                            
                        }
                    }


                        using (System.IO.StreamWriter file =
               new System.IO.StreamWriter(path1, true))
                        {
                            file.WriteLine(dateTimeString);
                        }
                    }
            }
        }
    }
}