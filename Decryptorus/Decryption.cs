using Authentificator;
using EASendMail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Decryptorus
{
    public class Decryption
    {

        public string AlgoXor(string text, string key)
        {
            var result = new StringBuilder();

            for (int c = 0; c < text.Length; c++)
            {
                // take next character from string
                char character = text[c];

                // cast to a uint
                uint charCode = (uint)character;

                // figure out which character to take from the key
                int keyPosition = c % key.Length; // use modulo to "wrap round"

                // take the key character
                char keyChar = key[keyPosition];

                // cast it to a uint also
                uint keyCode = (uint)keyChar;

                // perform XOR on the two character codes
                uint combinedCode = charCode ^ keyCode;

                // cast back to a char
                char combinedChar = (char)combinedCode;

                // add to the result
                result.Append(combinedChar);
            }

            return result.ToString();
        }

        public STG ParallelDecrypt(STG message)
        {
            List<Task> tasks = new List<Task>();
            Decryption d = new Decryption();
            KeyGenerator k = new KeyGenerator();
            string txt = message.Data.GetValue(1).ToString();
            string fileName = message.Data.GetValue(0).ToString();

            ParallelOptions options = new ParallelOptions();
            options.MaxDegreeOfParallelism = Environment.ProcessorCount;
            int count = 0;
            int numberOfKeyPossible = 600000;
            object locky = new object();
            string result = "";


            string key;
            //while (key != "ZZZZ")
            //{
            //    string tmp = d.AlgoXor(txt, key);
            //    key = k.IncrementKey();
            //    count++;
            //}
            Parallel.For(0, numberOfKeyPossible, options, (index, state) =>
            {
               
                lock (locky)
                {
                    key = k.IncrementKey();
                }

                string tmp = d.AlgoXor(txt, key);
            try
            {

                    JAXWS.CheckerEndpointClient platform = new JAXWS.CheckerEndpointClient();
                //JAXWS.checkDecryptRequest insert = new JAXWS.checkDecryptRequest();
                JAXWS.checkDecryptResponse response = new JAXWS.checkDecryptResponse();
                //JAXWS.checkDecryptResponse response = new JAXWS.checkDecryptResponse();
                    response = platform.checkDecrypt(AppToken.APPTOKEN, tmp);
                }
                catch (Exception e)
                {
                    message.Info = e.Message;
                }
                Interlocked.Increment(ref count);
                if (key == "ZZZZ")
                {
                    message.StatusOp = true;
                    message.OperationVersion = result;
                   // message.Info = platform.Endpoint.ToString();
                    state.Stop();
                }
            });
            key = k.IncrementKey();
            //message.Info = result;

            //message.OperationVersion = "ZZZZ";
            return message;
        }

        //public string sendEmail()
        //{
        //    SmtpMail oMail = new SmtpMail("TryIt");
        //    oMail.From = "groupeprojetdevnonmobile@gmail.com";
        //    oMail.To = "theo.fombasso@gmail.comt";
        //    oMail.Subject = "test email from c# project";
        //    // Set email body
        //    oMail.TextBody = "this is a test email sent from c# project, do not reply";

        //    // SMTP server address
        //    SmtpServer oServer = new SmtpServer("smtp.emailarchitect.net");

        //    // User and password for ESMTP authentication
        //    oServer.User = "test@emailarchitect.net";
        //    oServer.Password = "testpassword";

        //    // Most mordern SMTP servers require SSL/TLS connection now.
        //    // ConnectTryTLS means if server supports SSL/TLS, SSL/TLS will be used automatically.
        //    oServer.ConnectType = SmtpConnectType.ConnectTryTLS;

        //    // If your SMTP server uses 587 port
        //    // oServer.Port = 587;

        //    // If your SMTP server requires SSL/TLS connection on 25/587/465 port
        //    // oServer.Port = 25; // 25 or 587 or 465
        //    // oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

        //    Console.WriteLine("start to send email ...");

        //    SmtpClient oSmtp = new SmtpClient();
        //    oSmtp.SendMail(oServer, oMail);


        //}




    }
}