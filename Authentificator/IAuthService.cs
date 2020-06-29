using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IdentityModel.Tokens;
using System.Collections.ObjectModel;

namespace Authentificator
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAuthService" in both code and config file together.
    [ServiceContract]
    public interface IAuthService
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here
        [OperationContract]
        UserToken AuthUser(string usrname, string hashedPwd, string appToken);

    }

    [DataContract]
    public class UserToken
    {
        DateTime validFrom;
        DateTime expiryTime;
        string id;
        string key;
        public UserToken()
        {

        }

        public UserToken(string tokId, DateTime start, DateTime end, string tokKey)
        {
            id = tokId;
            validFrom = start;
            expiryTime = end;
            key = tokKey;
        }


        [DataMember]
        public DateTime ValidFrom
        {
            get { return validFrom; }
            set { validFrom = value; }
        }

        [DataMember]
        public DateTime ExpiryTime
        {
            get { return expiryTime; }
            set { expiryTime = value; }
        }

        [DataMember]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public string Key
        {
            get { return key; }
            set { key = value; }
        }



        /*
        UserNameSecurityToken usrToken;

        public UserToken(string userName, string password)
        {
            usrToken = new UserNameSecurityToken(userName, password);
        }

        public UserToken(string userName, string password, string id)
        {
            usrToken = new UserNameSecurityToken(userName, password, id);
        }
        
        
        [DataMember]
        public DateTime ValidityStart { get { return usrToken.ValidFrom; } }*/
        /*
        [DataMember]
        public DateTime ExpiryTime { get { return usrToken.ValidTo; } }

        [DataMember]
        public string TokenID { get { return usrToken.Id; } }

        [DataMember]
        public List<SecurityKey> Keys
        {
            get { return new List<SecurityKey>(usrToken.SecurityKeys); }
        }*/


    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
