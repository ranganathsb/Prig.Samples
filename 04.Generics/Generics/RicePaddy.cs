using System;

namespace Generics
{
    public class RicePaddy
    {
        internal RicePaddy(int identifier, Random r)
        {
            Identifier = identifier;
            var yield = r.Next();
            m_yield = yield % 10 == 0 ? default(int?) : yield * 1000;
        }

        public int Identifier { get; private set; }

        int? m_yield;
        public void SimulateHarvest()
        {
            if (!m_yield.HasValue)
            {
                Console.WriteLine("decimate entire villages...");
                return;
            }

            var r = new Random();
            for (int i = 0; i < 12; i++)
                Console.WriteLine("{0}: {1}", new DateTime().AddMonths(i).ToString("MMMM"), r.Next(10) * m_yield.Value);
        }

        public override string ToString()
        {
            return string.Format("Rice Paddy: {0}", Identifier.ToString("D3"));
        }
    }
}
