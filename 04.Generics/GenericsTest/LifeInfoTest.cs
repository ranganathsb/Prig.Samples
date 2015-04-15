using Generics;
using NUnit.Framework;
using System;
using System.Collections;
using System.Prig;
using UntestableLibrary.Prig;
using Urasandesu.Prig.Framework;

namespace GenericsTest
{
    [TestFixture]
    public class LifeInfoTest
    {
        [Test]
        [TestCaseSource(typeof(IsTodayHolidayTestSource), "TestCases")]
        public bool IsTodayHoliday_should_consider_a_set_day_and_the_previous_day_as_holiday(DateTime today, DayOfWeek holiday)
        {
            using (new IndirectionsContext())
            {
                // Arrange
                PDateTime.TodayGet().Body = () => today;
                PULConfigurationManager.GetPropertyOfTStringT<DayOfWeek>().Body = (key, defaultValue) => holiday;

                // Act, Assert
                return LifeInfo.IsTodayHoliday();
            }
        }

        class IsTodayHolidayTestSource
        {
            public static IEnumerable TestCases
            {
                get
                {
                    yield return new TestCaseData(new DateTime(2013, 11, 16), DayOfWeek.Sunday).Returns(true);
                    yield return new TestCaseData(new DateTime(2013, 11, 17), DayOfWeek.Sunday).Returns(true);
                    yield return new TestCaseData(new DateTime(2013, 11, 18), DayOfWeek.Sunday).Returns(false);

                    yield return new TestCaseData(new DateTime(2013, 11, 17), DayOfWeek.Monday).Returns(true);
                    yield return new TestCaseData(new DateTime(2013, 11, 18), DayOfWeek.Monday).Returns(true);
                    yield return new TestCaseData(new DateTime(2013, 11, 19), DayOfWeek.Monday).Returns(false);

                    yield return new TestCaseData(new DateTime(2013, 11, 18), DayOfWeek.Tuesday).Returns(true);
                    yield return new TestCaseData(new DateTime(2013, 11, 19), DayOfWeek.Tuesday).Returns(true);
                    yield return new TestCaseData(new DateTime(2013, 11, 20), DayOfWeek.Tuesday).Returns(false);

                    yield return new TestCaseData(new DateTime(2013, 11, 19), DayOfWeek.Wednesday).Returns(true);
                    yield return new TestCaseData(new DateTime(2013, 11, 20), DayOfWeek.Wednesday).Returns(true);
                    yield return new TestCaseData(new DateTime(2013, 11, 21), DayOfWeek.Wednesday).Returns(false);

                    yield return new TestCaseData(new DateTime(2013, 11, 20), DayOfWeek.Thursday).Returns(true);
                    yield return new TestCaseData(new DateTime(2013, 11, 21), DayOfWeek.Thursday).Returns(true);
                    yield return new TestCaseData(new DateTime(2013, 11, 22), DayOfWeek.Thursday).Returns(false);

                    yield return new TestCaseData(new DateTime(2013, 11, 21), DayOfWeek.Friday).Returns(true);
                    yield return new TestCaseData(new DateTime(2013, 11, 22), DayOfWeek.Friday).Returns(true);
                    yield return new TestCaseData(new DateTime(2013, 11, 23), DayOfWeek.Friday).Returns(false);
                    
                    yield return new TestCaseData(new DateTime(2013, 11, 22), DayOfWeek.Saturday).Returns(true);
                    yield return new TestCaseData(new DateTime(2013, 11, 23), DayOfWeek.Saturday).Returns(true);
                    yield return new TestCaseData(new DateTime(2013, 11, 24), DayOfWeek.Saturday).Returns(false);

                    yield return new TestCaseData(new DateTime(2013, 11, 23), (DayOfWeek)99).Returns(true);
                    yield return new TestCaseData(new DateTime(2013, 11, 24), (DayOfWeek)99).Returns(true);
                    yield return new TestCaseData(new DateTime(2013, 11, 25), (DayOfWeek)99).Returns(false);
                }
            }
        }
    }
}
