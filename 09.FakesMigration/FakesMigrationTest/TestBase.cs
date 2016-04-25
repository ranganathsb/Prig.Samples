using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace FakesMigrationTest
{
    public abstract class TestBase
    {
        static TestBase()
        {
            // This is a trick to bind old version to new version for the Moq instread of `assemblyBinding` in the App.config, 
            // because NUnit that specified the option `/domain=None` will ignore the configuration.
            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            {
                if (!args.Name.Contains("Moq,"))
                    return null;

                var moqAsm = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(_ => _.FullName.Contains("Moq,"));
                if (moqAsm != null)
                    return moqAsm;

                var executingAsm = Assembly.GetExecutingAssembly();
                var executingDir = Path.GetDirectoryName(executingAsm.Location);
                return Assembly.LoadFrom(Path.Combine(executingDir, "Moq.dll"));
            };
        }
    }
}
