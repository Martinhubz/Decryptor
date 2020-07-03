using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Decryptorus
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Decrypt : IDecrypt
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public STG m_service(STG msg)
        {
            Decryption d = new Decryption();
            STG message = d.ParallelDecrypt(msg); 

            //STG message = msg; 

            //List<Task> tasks = new List<Task>();
            
            //KeyGenerator k = new KeyGenerator();
            //string key;
            //string txt = message.Data.GetValue(1).ToString();
            //string fileName = message.Data.GetValue(0).ToString();

            //ParallelOptions options = new ParallelOptions();
            //options.MaxDegreeOfParallelism = Environment.ProcessorCount;

            //int i = 0;

            //int numberOfKeyPossible = 600000;
            //    Parallel.For(0, numberOfKeyPossible,options, (index,state) => {

            //        key = k.IncrementKey();
            //        string tmp = d.AlgoXor(txt, key);

            //        message.Info = key;
            //       // message.OperationName = tmp;
            //        //Debug.WriteLine(key);
            //        i++;
            //        //message.OperationName = "j'ai fais le zzzz";
            //        if (key == "ZZZZ" )
            //        {
            //            message.OperationName = "j'ai fais le zzzzi";
            //            state.Stop();
            //        }
            //    });
            //message.OperationVersion = i.ToString();
            ////    key = k.IncrementKey();

            return message;
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
