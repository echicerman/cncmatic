using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace LogManager
{
    public  class LogManager
    {
        private static ILog logFile = null;
        public static ILog GetLogger()
        {
            if (logFile == null)
            {
                log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"config\CNCmatic.Logger.Config.xml"));
                logFile = log4net.LogManager.GetLogger("CNCmatic");
            }
            return logFile;
        }
    }
}
