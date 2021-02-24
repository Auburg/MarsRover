using MarsRover.Core.Models;
using System.Threading.Tasks;

namespace MarsRover.Services.Interfaces
{
    public interface IMarsRoverApi
    {
        Task<RoverManifest> GetRoverManifestAsync(string roverName);
        Task<RootPhotoData> GetRoverPhotosAsync(string roverName, int sol, string camera);
    }
}
