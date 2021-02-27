using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HelloWebAPI.Controller;

namespace sznr
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HelloController helloController = new HelloController();
            
            List<Customer> customers = new List<Customer>();
            customers=helloController.GetList();
                      
            

            lv_Members.DataSource = customers;
            lv_Members.DataBind();

        }
    }
}