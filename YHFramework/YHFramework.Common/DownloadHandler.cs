using System;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using YHFramework.Security;

namespace YHFramework.Common
{
	public class DownloadHandler : IHttpHandlerFactory, IReadOnlySessionState, IRequiresSessionState
	{
		public IHttpHandler GetHandler(HttpContext context, string requestType, string url, string pathTranslated)
		{
			PageHandlerFactory factory = (PageHandlerFactory)Activator.CreateInstance(typeof(PageHandlerFactory), true);
			if (!IPAllowUtility.IpIsAllowed(context))
			{
				url = HttpContext.Current.Request.ApplicationPath + "error.aspx";
			}
			else
			{
				string fileName = url.Substring(url.LastIndexOf('/') + 1);
				context.Items["fileName"] = fileName;
				url = context.Request.ApplicationPath.TrimEnd(new char[]
				{
					'/'
				}) + "/Download.aspx";
				pathTranslated = context.Server.MapPath(context.Request.ApplicationPath.TrimEnd(new char[]
				{
					'/'
				}) + "/Download.aspx");
			}
			return factory.GetHandler(context, requestType, url, pathTranslated);
		}

		public void ReleaseHandler(IHttpHandler handler)
		{
		}
	}
}
