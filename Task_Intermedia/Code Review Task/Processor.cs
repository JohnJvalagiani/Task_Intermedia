using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Task_Intermedia.Code_Review_Task
{
    public class Processor
    {
        //The code uses static members for CurrentUserNameSlashPassword and To.
        //This might cause issues in a multi-threaded environment.
        //Consider using instance variables and proper encapsulation.
        public static string CurrentUserNameSlashPassword;
        public static string To;

        //Consider using dependency injection to inject instances of SkypeUtils
        //and viber_utils rather than creating them within the Processor class.
        //This will make the code more modular and easier to test.
        SkypeUtils skype = new SkypeUtils();
        viber_utils vbr = new viber_utils();

        public static void Set(string name, string pwd)
        {
            CurrentUserNameSlashPassword = name + "/" + pwd;
        }
        //The code contains hard-coded protocol names ("VIBER" and "SKYPE").
        //It would be more maintainable to define these as constants or enums
        //to avoid potential typos and improve code readability.
        public void SendMessage(string protocol, string message)
        {
            if (protocol.ToUpper() == "VIBER")
            {
                vbr.SendViber(CurrentUserNameSlashPassword, To, message);
            }
            else if (protocol.ToUpper() == "SKYPE")
            {
                skype.SendSkype(CurrentUserNameSlashPassword, To, message);
            }
        }
        //The code uses a try-catch block with a generic exception in the Receive method.
        //It's better to specify the specific exceptions that can be thrown so that you can handle them appropriately.
        //For example, you could catch specific exceptions related to Skype or Viber operations.

        //The code lacks comments and documentation.
        //It's important to document the purpose and usage of classes and methods.
        public void Receive(string protocol, ref string message)
        {
            try
            {
                if (protocol.ToUpper() == "SKIPE")
                {
                    message = skype.Skype_recv(CurrentUserNameSlashPassword).ToString();
                }
                else
                {
                    message = viber.getMessage(CurrentUserNameSlashPassword).ToString();
                }
            }
            catch (Exception)
            {
                throw new Exception("Processor Error!");
            }
        }
    }

    public class SkypeUtils
    {
        //The URL "news.com" is used in the VerifyInternetConnection method.
        //It's better to define this URL as a constant or a configuration setting to make it more flexible.

        //The VerifyInternetConnection method throws an ArgumentException if the ping fails.
        //It's better to create a custom exception or provide more information
        //in the exception message to make it clear why the exception was thrown.
        public void VerifyInternetConnection()
        {
            if (new Ping().Send("news.com").Status != IPStatus.Success)
            {
                throw new ArgumentException();
            }
        }

        public void SendSkype(string str_up, string str_to, string str_message)
        {
            VerifyInternetConnection();
            str_message = str_message + " from " + str_up.ToUpper();
            var parts = str_up.Split('/');
            SkypeApi.Send(str_to, str_message, parts[0], parts[1]);
        }

        public object Skype_recv(string str_usrNameAndPass)
        {
            VerifyInternetConnection();
            var parts = str_usrNameAndPass.Split('/');
            string message = SkypeApi.GetNextMessage(parts[0], parts[1]);
            return message;
        }
    }

    internal class viber_utils : SkypeUtils
    {
        //The code references SkypeApi and ViberApi, but these classes are not provided in the code snippet.
        //Ensure that these classes exist and are accessible.

        //Consider adding logging to the code to track what's happening during message sending and receiving.
        //It can be helpful for debugging and monitoring.
        public void SendViber(string str_usrNameAndPass, string str_to, string str_message)
        {
            base.VerifyInternetConnection();
            var parts = str_usrNameAndPass.Split('/');
            ViberApi.Send(str_to, str_message, parts[0], parts[1]);
        }

        //The code should follow a consistent naming and formatting convention.
        //For example, some method names start with uppercase letters, while others start with lowercase letters.
        //It's a good practice to follow a consistent naming convention like PascalCase for method names.

        //There's some duplication in error handling and internet connection verification in both the SkypeUtils and viber_utils classes.
        //Consider refactoring to avoid code duplication.
        public object getMessage(string str_usrNameAndPass)
        {
            base.VerifyInternetConnection();
            var parts = str_usrNameAndPass.Split('/');
            string message = ViberApi.RecieveMessage(parts[0], parts[1]);
            return message;
        }
    }

}
