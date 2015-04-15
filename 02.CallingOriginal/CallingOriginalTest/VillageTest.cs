using CallingOriginal;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Prig;
using System.Threading;
using Urasandesu.Prig.Framework;

namespace CallingOriginalTest
{
    [TestFixture]
    public class VillageTest
    {
        [Test]
        public void Constructor_shall_not_call_within_short_timeframe_to_generate_unique_information()
        {
            using (new IndirectionsContext())
            {
                // Arrange
                var seeds = new HashSet<int>();
                PRandom.ConstructorInt32().Body = (@this, seed) =>
                {
                    IndirectionsContext.ExecuteOriginal(() =>
                    {
                        var ctor = typeof(Random).GetConstructor(new[] { typeof(int) });
                        ctor.Invoke(@this, new object[] { seed });
                    });
                    seeds.Add(seed);
                };
                new Random();  // preparing JIT
                seeds.Clear();


                // Act
                var vil1 = new Village();
                Thread.Sleep(TimeSpan.FromSeconds(1));
                var vil2 = new Village();


                // Assert
                Assert.AreEqual(2, seeds.Count);
            }
        }
    }
}
