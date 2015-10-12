using Urasandesu.Prig.Framework;

namespace MyDelegates
{
    [IndirectionDelegate]
    public delegate TResult IndirectionInOutOutOutOutFunc<in T1, TOut1, TOut2, TOut3, TOut4, TResult>(T1 arg1, out TOut1 out1, out TOut2 out2, out TOut3 out3, out TOut4 out4);

    [IndirectionDelegate]
    public delegate TResult IndirectionInInOutRefOutFunc<in T1, in T2, TOut1, TRef1, TOut2, TResult>(T1 arg1, T2 arg2, out TOut1 out1, ref TRef1 ref1, out TOut2 out2);
}
