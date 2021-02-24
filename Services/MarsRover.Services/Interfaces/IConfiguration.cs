using System;

namespace MarsRover.Services.Interfaces
{
    public interface IConfiguration
    {
        public string ApiKey { get; }

        public Uri ApiBaseUrl { get; }
    }
}
