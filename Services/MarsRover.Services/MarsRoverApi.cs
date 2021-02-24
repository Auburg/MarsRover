using MarsRover.Core.Models;
using MarsRover.Services.Interfaces;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MarsRover.Services
{
    public class MarsRoverApi : IMarsRoverApi
    {
        private readonly IConfiguration configuration;
        private HttpClient client = new HttpClient();

        public MarsRoverApi(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<RoverManifest> GetRoverManifestAsync(string roverName)
        {            
            var streamTask = client.GetStreamAsync($"{configuration.ApiBaseUrl}/manifests/{roverName}/?api_key={configuration.ApiKey}");
            return await DeserializeType<RoverManifest>(streamTask);
        }

        public async Task<RootPhotoData> GetRoverPhotosAsync(string roverName, int sol, string camera)
        {
            var streamTask = client.GetStreamAsync($"{configuration.ApiBaseUrl}/rovers/{roverName}/photos?sol={sol}&camera={camera}&api_key={configuration.ApiKey}");
            return await DeserializeType<RootPhotoData>(streamTask);
        }

        private async Task<T> DeserializeType<T>(Task<System.IO.Stream> streamTask)
        {
            return await JsonSerializer.DeserializeAsync<T>(await streamTask, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
        }
    }
}
