﻿using StackExchange.Profiling;
using StackExchange.Profiling.EntityFramework6;
using StackExchange.Profiling.Mvc;
using StackExchange.Profiling.Storage;
using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CTS_Analytics
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            BundleConfig.RegisterBundles(BundleTable.Bundles);
            MiniProfiler.Configure(new MiniProfilerOptions
            {
                RouteBasePath = "~/profiler",
                PopupRenderPosition = RenderPosition.Right,
                Storage = new MultiStorageProvider(
                    new MemoryCacheStorage(new TimeSpan(1, 0, 0)))
            }).AddViewProfiling();
            MiniProfilerEF6.Initialize();
        }

        protected void Application_BeginRequest()
        {
            MiniProfiler profiler = null;
            if (Request.IsLocal) 
            {
                profiler = MiniProfiler.StartNew();
            }
        }

        protected void Application_EndRequest()
        {
            MiniProfiler.Current?.Stop();
        }

    }
}
