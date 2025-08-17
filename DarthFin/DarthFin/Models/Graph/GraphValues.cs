using System.Reflection.Metadata.Ecma335;

namespace DarthFin.Models.Graph
{
    public class GraphValues
    {
        public string Name { get; set; }
        public string Color { get; set; } = "#888888";
        public List<double> Values { get; set; }
    }
}
