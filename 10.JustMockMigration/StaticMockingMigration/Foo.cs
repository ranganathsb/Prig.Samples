﻿using System;

namespace StaticMockingMigration
{
    public class Foo
    {
        static Foo()
        {
            //throw new NotImplementedException();
        }

        public static void Submit()
        {
        }

        public static int Execute(int arg)
        {
            throw new NotImplementedException();
        }

        public static int FooProp
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }

    internal class FooInternal
    {
        internal static void DoIt()
        {
            throw new NotImplementedException();
        }
    }

    public static class FooStatic
    {
        public static void Do()
        {
            throw new NotImplementedException();
        }
    }
}
