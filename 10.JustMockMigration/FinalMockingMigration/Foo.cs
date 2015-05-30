using System;

namespace FinalMockingMigration
{
    public class Foo
    {
        public int Execute(int arg1, int arg2)
        {
            throw new NotImplementedException();
        }

        public int Execute(int arg1)
        {
            throw new NotImplementedException();
        }

        public int Echo(int arg1)
        {
            return arg1;
        }

        public string FooProp { get; set; }

        public delegate void EchoEventHandler(bool echoed);
        public event EchoEventHandler OnEchoCallback;
    }

    public class FooGeneric
    {
        public TRet Echo<T, TRet>(T arg1)
        {
            throw new NotImplementedException();
        }
    }
}
