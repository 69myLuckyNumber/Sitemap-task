using SitemapTask.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SitemapTask
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			DependencyResolver.SetResolver(new NinjectDependencyResolver());
		}
		protected void Application_Error(object sender, EventArgs e)
		{
			// An error has occured on a .Net page.
			var serverError = Server.GetLastError() as HttpException;

			if (null != serverError)
			{
				int errorCode = serverError.GetHttpCode();

				if (404 == errorCode)
				{
					Server.ClearError();

					Response.Redirect("/Error/NotFound");
				}
				else
				{
					Response.Redirect("/Error/Index");
				}
			}
		}
	}
}
