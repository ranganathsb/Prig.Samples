
using System;

namespace SealedMockingMigration
{
    public sealed class FooSealed
    {
        public int Echo(int arg1)
        {
            return arg1;
        }
    }

    public sealed class FooSealedInternal
    {
        internal FooSealedInternal()
        {

        }

        public int Echo(int arg1)
        {
            return arg1;
        }
    }

    public interface IFoo
    {
        void Execute();
        void Execute(int arg1);
    }

    public sealed class Foo : IFoo
    {
        public void Execute()
        {
            throw new NotImplementedException();
        }

        void IFoo.Execute(int arg1)
        {
            throw new NotImplementedException();
        }
    }
}
