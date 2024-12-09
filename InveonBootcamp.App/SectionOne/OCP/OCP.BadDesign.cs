using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.App.SectionOne.OCP
{
    public class MessageSender
    {

        public void SendMessage(string message, string messageType)
        {
            if (messageType == "SMS")
            {
                Console.WriteLine($"Sending SMS: {message}");
            }
            else if (messageType == "Email")
            {
                Console.WriteLine($"Sending Email: {message}");
            }
            else if (messageType == "MMS")
            {
                Console.WriteLine($"Sending MMS: {message}");
            }
            else
            {
                Console.WriteLine($"Message type is not highlighted.");
            }
        }

    }
}
