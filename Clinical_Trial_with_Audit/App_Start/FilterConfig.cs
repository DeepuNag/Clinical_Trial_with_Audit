using System.Web;
using System.Web.Mvc;

namespace Clinical_Trial_with_Audit
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
