
namespace CallingOriginal
{
    public class FarmRoad
    {
        public FarmRoad(RicePaddy a, RicePaddy b, int distance)
        {
            A = a;
            B = b;
            Distance = distance;
        }

        public RicePaddy A { get; private set; }
        public RicePaddy B { get; private set; }
        public int Distance { get; private set; }

        public override string ToString()
        {
            return string.Format("Farm Road [A: {0}, B: {1}, Distance: {2}]", A, B, Distance);
        }
    }
}
