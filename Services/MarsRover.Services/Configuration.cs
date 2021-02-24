using MarsRover.Core;
using MarsRover.Services.Interfaces;
using System;
using System.Configuration;

namespace MarsRover.Services
{
    public class Configuration : IConfiguration
    {
        public string ApiKey => ConfigurationManager.AppSettings[AppSettings.ApiKey];
        public Uri ApiBaseUrl => new Uri(ConfigurationManager.AppSettings[AppSettings.ApiBaseUrl]);
    }
}
