using IsolatorMigration;
using IsolatorMigration.Prig;
using Moq;
using NUnit.Framework;
using System;
using System.Windows.Forms;
using System.Windows.Forms.Prig;
using Urasandesu.Prig.Delegates;
using Urasandesu.Prig.Framework;

namespace IsolatorMigrationTest
{
    [TestFixture]
    public class Test0_Tutorial
    {
        [Test]
        public void MessageBoxShow_should_be_callable_indirectly()
        {
            using (new IndirectionsContext())
            {
                // Arrange
                var mockMessageBox = new Mock<IndirectionFunc<string, DialogResult>>();
                mockMessageBox.Setup(_ => _(string.Empty)).Returns(DialogResult.OK);

                PMessageBox.ShowString().Body = mockMessageBox.Object;

                // Act
                MessageBox.Show("This is a message");

                // Assert
                mockMessageBox.Verify(_ => _("This is a message"));
            }
        }



        [Test]
        public void UserOfSomeClassDoSomething_should_show_MessageBox_if_an_exception_is_thrown()
        {
            using (new IndirectionsContext())
            {
                // Arrange
                PSomeClass.MyMethod().Body = () => { throw new Exception("foo"); };

                var mockMessageBox = new Mock<IndirectionFunc<string, DialogResult>>();
                mockMessageBox.Setup(_ => _(string.Empty)).Returns(DialogResult.OK);

                PMessageBox.ShowString().Body = mockMessageBox.Object;

                // Act
                var user = new UserOfSomeClass();
                user.DoSomething();

                // Assert
                mockMessageBox.Verify(_ => _("Exception caught: foo"));
            }
        }
    }
}
