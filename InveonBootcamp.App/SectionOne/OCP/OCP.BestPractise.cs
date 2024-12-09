using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.App.SectionOne.OCP
{
    public interface IMessageSender
    {
        void SendMessage(string message);
    }

    public class SMSSender : IMessageSender
    {
        public void SendMessage(string message)
        {
            Console.WriteLine($"Sending SMS: {message}");
        }
    }

    public class EmailSender : IMessageSender
    {
        public void SendMessage(string message)
        {
            Console.WriteLine($"Sending Email: {message}");
        }
    }

    public class MMSSender : IMessageSender
    {
        public void SendMessage(string message)
        {
            Console.WriteLine($"Sending MMS: {message}");
        }
    }
}
