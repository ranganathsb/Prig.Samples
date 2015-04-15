using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CallingOriginal
{
    public class Route
    {
        public Route(int identifier)
        {
            Identifier = identifier;
            TotalDistance = int.MaxValue;
            Roads = Enumerable.Empty<FarmRoad>();
        }

        public int Identifier { get; private set; }
        public int TotalDistance { get; internal set; }
        public IEnumerable<FarmRoad> Roads { get; internal set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(string.Format("Route: {0}, Total Distance: {1}", Identifier.ToString("D3"), TotalDistance));
            foreach (var road in Roads)
                sb.AppendLine(string.Format("    {0}", road));
            return sb.ToString();
        }
    }
}
