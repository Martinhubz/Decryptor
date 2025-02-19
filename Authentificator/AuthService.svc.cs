﻿using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLibrary;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IdentityModel.Tokens;
using System.Diagnostics;

namespace Authentificator
{

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class AuthService : IAuthService
    {

        //[System.Security.Permissions.PrincipalPermission(
        //    System.Security.Permissions.SecurityAction.Demand,Role = @"BUILTIN\Users")]
        public UserToken AuthUser(string usrname, string hashedPwd, string appToken)
        {

            AUTHEntities1 _db = new AUTHEntities1();
            ILogger logger = new Logger();

            LogEntry attemptLog = new LogEntry();
            attemptLog.Issuer = (int)Issuer.Auth;
            
            try
            {
                
                if (_db.Users.Count() < 1)
                {
                    attemptLog.Type = (int)EntryType.Error;
                    attemptLog.Message = "Empty user list";

                    logger.AddLogEntry(attemptLog);
                    return null;
                }
                else if (!_db.Users.Any(o => o.Username == usrname))
                {
                    attemptLog.Type = (int)EntryType.Error;
                    attemptLog.Message = "User " + usrname + " doesn't exist";

                    logger.AddLogEntry(attemptLog);
                    return null;
                }
                
                var user = _db.Users.First(o => o.Username == usrname);
                

                //TODO: Check appToken validity
                if (user != null 
                    && user.Password == hashedPwd
                    && appToken == AppToken.APPTOKEN)
                {
                    //Generate user token
                    attemptLog.Type = (int)EntryType.Info;
                    attemptLog.Message = "User " + usrname + " successfully logged in";

                    logger.AddLogEntry(attemptLog);

                    
                    DateTime now = DateTime.Now;
                    string uniqueId = Guid.NewGuid().ToString();

                    UserToken usrToken;

                    if (user.UsrToken != null)
                    {
                        usrToken = new UserToken(uniqueId, now, now.AddDays(32), user.UsrToken);
                    }
                    else
                    {
                        usrToken = new UserToken(uniqueId, now, now.AddDays(32), uniqueId.GetHashCode().ToString());
                    }

                    return usrToken;
                    
                }
                else
                {
                    attemptLog.Type = (int)EntryType.Info;
                    attemptLog.Message = "User doesn't exist or invalid password entered";

                    logger.AddLogEntry(attemptLog);
                    return null;
                }
            }
            catch (Exception e)
            {
                attemptLog.Type = (int)EntryType.Error;
                attemptLog.Message = "Exception catched: " + e;

                logger.AddLogEntry(attemptLog);

                Debug.WriteLine(e);
                return null;
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
