using Generics;
using NUnit.Framework;
using System.Collections.Generic;
using System.Collections.Generic.Prig;
using System.Linq;
using System.Prig;
using Urasandesu.Prig.Framework;

namespace GenericsTest
{
    [TestFixture]
    public class VillageTest
    {
        [Test]
        public void GetShortestRoute_should_consider_routes_in_order_from_small_distance()
        {
            using (new IndirectionsContext())
            {
                // Arrange
                var slot = 0;
                var numAndDistances = new[] { 4, 2, 4, 3, 1, 6, 7 };
                PRandom.NextInt32().Body = (@this, maxValue) => numAndDistances[slot++];

                var vil = new Village();

                var considerations = new List<RicePaddy>();
                PList<RicePaddy>.AddT().Body = (@this, item) =>
                {
                    IndirectionsContext.ExecuteOriginal(() =>
                    {
                        considerations.Add(item);
                        @this.Add(item);
                    });
                };


                // Act
                var result = vil.GetShortestRoute(vil.RicePaddies.ElementAt(2), vil.RicePaddies.ElementAt(0));


                // Assert
                Assert.AreEqual(3, result.TotalDistance);
                Assert.AreEqual(4, considerations.Count);
                Assert.AreEqual(2, considerations[0].Identifier);
                Assert.AreEqual(1, considerations[1].Identifier);
                Assert.AreEqual(0, considerations[2].Identifier);
                Assert.AreEqual(3, considerations[3].Identifier);
            }
        }
    }
}
