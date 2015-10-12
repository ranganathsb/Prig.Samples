using NUnit.Framework;
using System.Collections.Generic;
using ThreeOrMoreOutRef;
using UntestableLibrary;
using UntestableLibrary.Prig;
using Urasandesu.Prig.Framework;

namespace ThreeOrMoreOutRefTest
{
    [TestFixture]
    public class ChatRoomTest
    {
        [Test]
        public void Start_should_continue_bots_to_chat_using_the_recommendation_of_previous_bot_as_the_input_of_next_bot()
        {
            using (new IndirectionsContext())
            {
                // Arrange
                var bots = new List<ULChatBot>();
                var actualInputs = new List<ULTypeOfFeedback>();

                var proxy1 = new PProxyULChatBot();
                proxy1.ReplyInformationULTypeOfFeedbackStringRefULActionsRefULTypeOfFeedbackRef().Body = 
                    (ULChatBot @this, ULTypeOfFeedback input, out string reply, ref ULActions action, out ULTypeOfFeedback recommendation) => 
                    {
                        actualInputs.Add(input);
                        reply = "1";
                        recommendation = ULTypeOfFeedback.Suggestion;
                        return true;
                    };
                bots.Add(proxy1);

                var proxy2 = new PProxyULChatBot();
                proxy2.ReplyInformationULTypeOfFeedbackStringRefULActionsRefULTypeOfFeedbackRef().Body = 
                    (ULChatBot @this, ULTypeOfFeedback input, out string reply, ref ULActions action, out ULTypeOfFeedback recommendation) =>
                    {
                        actualInputs.Add(input);
                        reply = "2";
                        recommendation = ULTypeOfFeedback.Incomprehensible;
                        return true;
                    };
                bots.Add(proxy2);


                // Act
                new ChatRoom().Start(ULTypeOfFeedback.Praise, bots.ToArray());


                // Assert
                var expectedInputs = new[] { ULTypeOfFeedback.Praise, ULTypeOfFeedback.Suggestion };
                CollectionAssert.AreEqual(expectedInputs, actualInputs);
            }
        }
    }
}
