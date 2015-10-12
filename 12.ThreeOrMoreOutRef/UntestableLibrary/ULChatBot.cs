using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UntestableLibrary
{
    public enum ULActions
    {
        Unknown,
        Discard,
        ForwardToManagement,
        ForwardToDeveloper
    }

    public enum ULTypeOfFeedback
    {
        Complaint,
        Praise,
        Suggestion,
        Incomprehensible
    }

    public class ULChatBot
    {
        public ULTypeOfFeedback Recommendation { get; private set; }

        public bool ReplyInformation(ULTypeOfFeedback input, out string reply, ref ULActions action, out ULTypeOfFeedback recommendation)
        {
            Thread.Sleep(10000); // simulate connecting an external service and filling repltyText by using the data that are retrieved from some APIs
            var returnReply = false;
            var replyText = "Your feedback has been forwarded to the product manager.";

            reply = string.Empty;
            switch (input)
            {
                case ULTypeOfFeedback.Complaint:
                case ULTypeOfFeedback.Praise:
                    action = ULActions.ForwardToManagement;
                    reply = "Thank you. " + replyText;
                    returnReply = true;
                    break;
                case ULTypeOfFeedback.Suggestion:
                    action = ULActions.ForwardToDeveloper;
                    reply = replyText;
                    returnReply = true;
                    break;
                case ULTypeOfFeedback.Incomprehensible:
                default:
                    action = ULActions.Discard;
                    returnReply = false;
                    break;
            }
            Recommendation = recommendation = (ULTypeOfFeedback)(((int)input + 1) % 4);
            return returnReply;
        }
    }
}
