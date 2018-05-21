using System;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using YHFramework.Security;

namespace YHFramework.Common
{
	public class Handler : IHttpHandlerFactory, IReadOnlySessionState, IRequiresSessionState
	{
		public IHttpHandler GetHandler(HttpContext context, string requestType, string url, string pathTranslated)
		{
			PageHandlerFactory factory = (PageHandlerFactory)Activator.CreateInstance(typeof(PageHandlerFactory), true);
			if (!IPAllowUtility.IpIsAllowed(context))
			{
				url = HttpContext.Current.Request.ApplicationPath + "error.aspx";
			}
			IHttpHandler handler;
			if (-1 == url.IndexOf(".rar"))
			{
				handler = factory.GetHandler(context, requestType, url, pathTranslated);
			}
			else
			{
				handler = null;
			}
			return handler;
		}

		public void ReleaseHandler(IHttpHandler handler)
		{
		}
	}
}
