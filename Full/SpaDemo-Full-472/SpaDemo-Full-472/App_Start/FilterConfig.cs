﻿using System.Web;
using System.Web.Mvc;

namespace SpaDemo_Full_472
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
