using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using Newtonsoft.Json;
using CORE.Models;
using System.Diagnostics;

namespace CORE.Utilities
{
    public static class Utilities
    {
        public static Configuration GetConfiguration()
        {
            Configuration configuration = null;
            try
            {
                configuration = GetConfiguration<Configuration>();
            }
            catch (Exception ex)
            {
                throw new AppException("GetConfiguration failed|" + ex.Message);
            }
            return configuration;
        }
        public static T GetConfiguration<T>()
        {
            IConfigurationRoot configurationRoot = null;
            string configFilePath = "";
            try
            {
                var configurationBuilder = new ConfigurationBuilder();
                var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
                configurationBuilder.AddJsonFile(path, false);
                configurationRoot = configurationBuilder.Build();
                if (configurationRoot.GetSection("ConfigFilesPath").Exists())
                {
                    configFilePath = configurationRoot.GetSection("ConfigFilesPath").Value;
                    if (configFilePath != "")
                    {
                        configurationBuilder.AddJsonFile(configFilePath, false);
                        configurationRoot = configurationBuilder.Build();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new AppException("GetConfiguration:" + ex.Message);
            }
            return configurationRoot.Get<T>();
        }
        // normalde başka bir metodun içinden çağrılıyor ve private bir fonksiyon
        public static void LogError(Exception ex, string specificMessage)
        {
            try
            {
                string strLogMessage = "\nMessage : " + ex.Message +
               "\nSpecificMessage : " + specificMessage +
               "\nSource : " + ex.Source +
               "\nTarget Site : " + ex.TargetSite +
               "\nStack Trace : " + ex.StackTrace;
                string logName = "";
                logName = "Application";
                EventLog log = new EventLog();
                log.Source = logName;
                strLogMessage += "\r\n\r\n--------------------------\r\n\r\n" + ex.ToString();
                log.WriteEntry(strLogMessage, EventLogEntryType.Error, 65534);
            }
            catch
            {
                //do nothing
            }
        }

    }
}
