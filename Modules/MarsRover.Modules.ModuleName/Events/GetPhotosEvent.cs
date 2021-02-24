using MarsRover.Core.Models;
using Prism.Events;

namespace MarsRover.Module.Events
{
    public class GetPhotosEvent : PubSubEvent<RetrievePhotosData>
    {
    }
}
