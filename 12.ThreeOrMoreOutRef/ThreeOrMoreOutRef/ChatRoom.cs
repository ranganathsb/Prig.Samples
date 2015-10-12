using System.Text;
using UntestableLibrary;

namespace ThreeOrMoreOutRef
{
    public class ChatRoom
    {
        public string Log { get; private set; }

        public void Start(ULTypeOfFeedback input, ULChatBot[] bots)
        {
            var reply = default(string);
            var action = default(ULActions);
            var recommendation = input;
            var result = default(bool);

            var sb = new StringBuilder(Log);

            foreach (var bot in bots)
            {
                result = bot.ReplyInformation(recommendation, out reply, ref action, out recommendation);
                sb.AppendFormat("Reply: {0} Action: {1} return? {2}\r\n", reply, action, result);
            }

            Log = sb.ToString();
        }
    }
}
