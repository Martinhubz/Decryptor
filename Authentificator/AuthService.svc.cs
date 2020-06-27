using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLibrary;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Authentificator
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class AuthService : IAuthService
    {
        AUTHEntities1 _db;
        ILogger logger;

        public AuthService()
        {
            _db = new AUTHEntities1();
            logger = new Logger();
            
        }

        //TODO: change return type to token type (prevoir return échec)
        public string AuthUser(string usrname, string hashedPwd, string appToken)
        {
            LogEntry attemptLog = new LogEntry();
            attemptLog.Issuer = (int)Issuer.Auth;

            if (_db.Users.Count() < 1)
            {
                attemptLog.Type = (int)EntryType.Error;
                attemptLog.Message = "Empty user list";

                logger.AddLogEntry(attemptLog);
                return "";
            }

            var user = _db.Users.First(m => m.Username == usrname);

            //TODO: Check appToken validity
            if(user != null && user.Password == hashedPwd)
            {
                //Generate user token
                attemptLog.Type = (int)EntryType.Info;
                attemptLog.Message = "User " + usrname + " successfully logged in" ;

                logger.AddLogEntry(attemptLog);

                ITokenBuilder Builder = new TokenBuilder();

                return Builder.BuildToken();
            }
            else
            {
                attemptLog.Type = (int)EntryType.Info;
                attemptLog.Message = "User doesn't exist or invalid password entered";

                logger.AddLogEntry(attemptLog);
                return "";
            }

        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
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
