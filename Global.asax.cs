using RMG.BusinessUnit.Domain.Api.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace RMG.BusinessUnit.Domain.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AutoMapperConfig.Initialize(); //initialize automapper source and destinations
            AutofacConfig.Initialize(); //initialize autofac dependency container 
            AppConfig.Intialize(); //initalize application defined methods


            GlobalConfiguration.Configure(WebApiConfig.Register);
        }



        /// <summary>
        /// Occurs just before ASP.NET sends HTTP headers to the client
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_PreSendRequestHeaders(object sender, EventArgs e)
        {
            //Remove un-necessary headers 
            string[] headers = { "X-Powered-By", "Server", "X-AspNet-Version" };
            if (!Response.HeadersWritten)
            {
                Response.AddOnSendingHeaders((c) =>
                {
                    if (c != null && c.Response != null && c.Response.Headers != null)
                    {
                        foreach (string header in headers)
                        {
                            if (c.Response.Headers[header] != null)
                            {
                                c.Response.Headers.Remove(header);
                            }
                        }
                    }
                });
            }

        }



    }
}
