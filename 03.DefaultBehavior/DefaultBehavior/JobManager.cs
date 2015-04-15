using System;

namespace DefaultBehavior
{
    public static class JobManager
    {
        public static void NotifyStartJob(CommunicationContext ctx, string[] args)
        {
            int err = 0;

            if ((err = ctx.VerifyRuntimeVersion(args)) != 0)
                goto fail;
            if ((err = ctx.VerifyPrereqOf3rdParty(args)) != 0)
                goto fail;
            if ((err = ctx.VerifyUserAuthority(args)) != 0)
                goto fail;
            // No longer need checking product license.
            //if ((err = ctx.VerifyProductLicense(args)) != 0)
            //    goto fail;

            int mode = 0;
            // ... Imagine that there are many instructions to calculate 'mode'...
            if (err != 0)
                goto fail;

            bool notifiesError = false;
            // ... Imagine that there are many instructions to calculate 'notifiesError'...
            if (err != 0)
                goto fail;

            string hash = null;
            // ... Imagine that there are many instructions to calculate 'hash'...
            if (err != 0)
                goto fail;

            UpdateJobParameterFile(err, mode, notifiesError, hash);
            return;

        fail:
            Log(string.Format("Code: {0}, MachineName: {1}, CurrentDirectory: {2}", err, Environment.MachineName, Environment.CurrentDirectory), Environment.StackTrace);
            UpdateJobParameterFile(err, 0, false, null);
        }

        static void UpdateJobParameterFile(int code, int mode, bool notifiesError, string hash)
        {
            throw new NotImplementedException();
        }

        static void Log(string msg, string stackTrace)
        {
        }
    }
}
