using System;
using System.Windows.Forms;

namespace IsolatorMigration
{
    public class UserOfSomeClass
    {
        public void DoSomething()
        {
            try
            {
                SomeClass.MyMethod();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception caught: " + ex.Message);
            }
        }
    }
}
