using Authentificator;
using EASendMail;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using MailAddress = System.Net.Mail.MailAddress;
using SmtpClient = System.Net.Mail.SmtpClient;

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

            // attribut needed to work
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
            object result = "";
            string key;
            try
            {
                message.Info = "toto a la plage";
                // ServiceDecrypt.CheckerEndpointClient platform = new ServiceDecrypt.CheckerEndpointClient();
                //string ret = platform.checkDecrypt("apptok", "decryptInfo");
                //System.Diagnostics.Debug.WriteLine(ret);

            }
            catch (Exception e)
            {
                message.Info = e.Message;
            }
            //result = platform.checkDecrypt(AppToken.APPTOKEN, txt);
            // Launch each key for the file 
            //Parallel.For(0, numberOfKeyPossible, options, (index, state) =>
            //{
            //    // lock the increment to not access it at the same time optimize
            //    lock (locky)
            //    {
            //        // increment the key
            //        key = k.IncrementKey();
            //    }
            //    // Algo XOR 
            //    string tmp = d.AlgoXor(txt, key);
            //    try
            //    {
            //        // send request to Jax to verify
                    

            //    }
            //    catch (Exception e)
            //    {
            //        message.Info = e.Message;
            //        System.Diagnostics.Debug.WriteLine(e.Message);
            //    }

            //    Interlocked.Increment(ref count);
            //    if (key == "ZZZZ" ) // mettre condition result pour stoper la boucle
            //    {
            //        if (true)// mettre condition result pour stoper la boucle
            //        {
            //            try
            //            {
            //               // createPdf(fileName, txt);
            //                //sendEmail(fileName);
            //            }catch(Exception e){
            //                message.Info = e.Message;
            //            }
            //        }
            //        message.StatusOp = true;

            //        state.Stop();
            //    }
            //});
            
            return message;
        }

        public static string sendEmail(string path)
        {
            string result = "ok";
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new System.Net.Mail.SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("groupeprojetdevnonmobile@gmail.com");
                mail.To.Add("theo.fombasso@gmail.com");
                mail.Subject = "Test Mail - 1";
                mail.Body = "mail with attachment";

                path = path.Split('\\').Last();
                path = path.Replace("txt", "pdf");
                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment("C:\\Users\\AzureUser\\Desktop\\"+path);
                mail.Attachments.Add(attachment);


                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("groupeprojetdevnonmobile@gmail.com", "ProjetDev123");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            }
            catch (Exception e)
            {
                result = e.Message;
            }
            return result;
        }

        public static string createPdf(string path, string txt)
        {
            string result = "j'ai pdf";
            path = path.Split('\\').Last();
            path = path.Replace("txt", "pdf");
            PdfWriter writer = new PdfWriter("C:\\Users\\AzureUser\\Desktop\\"+path);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);
            Paragraph header = new Paragraph(txt)
               .SetTextAlignment(TextAlignment.LEFT)
               .SetFontSize(12);

            document.Add(header);
            document.Close();

            return result;
        }



    }
}