using Orleans;
namespace Grains
{
    public class OrleansClient
    {
            public static IClusterClient ClusterClient { get; set; }
    }
}
