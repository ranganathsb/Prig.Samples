using System;

namespace DefaultBehavior
{
    public class CommunicationContext
    {
        public virtual int VerifyRuntimeVersion(string[] args)
        {
            throw new NotImplementedException();
        }

        public virtual int VerifyPrereqOf3rdParty(string[] args)
        {
            throw new NotImplementedException();
        }

        public virtual int VerifyUserAuthority(string[] args)
        {
            throw new NotImplementedException();
        }

        public virtual int VerifyProductLicense(string[] args)
        {
            throw new NotImplementedException();
        }
    }
}
