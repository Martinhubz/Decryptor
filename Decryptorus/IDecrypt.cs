using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Decryptorus
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDecrypt" in both code and config file together.
    [ServiceContract]
    public interface IDecrypt
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        [OperationContract]
        STG m_service(STG msg);
        // TODO: Add your service operations here
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

    [DataContract]
    public class STG 
    {
        bool statusOp;
        string operationVersion;
        string appVersion;
        string operationName;
        string info;
        string tokenUser;
        string tokenApp;
        object[] data;

        public STG(bool statusOp, string operationVersion, string appVersion, string operationName,string info, string tokenUser, string tokenApp, object[] data)
        {
            this.statusOp = statusOp;
            this.operationVersion = operationVersion;
            this.appVersion = appVersion;
            this.operationName = operationName;
            this.info = info;
            this.tokenUser = tokenUser;
            this.tokenApp = tokenApp;
            this.data = data;
        }




        [DataMember]
        public bool StatusOp
        {
            get { return statusOp; }
            set { statusOp = value; }
        }

        [DataMember]
        public string OperationVersion
        {
            get { return operationVersion; }
            set { operationVersion = value; }
        }

        [DataMember]
        public string AppVersion
        {
            get { return appVersion; }
            set { appVersion = value; }
        }

        [DataMember]
        public string OperationName
        {
            get { return operationName; }
            set { operationName = value; }
        }
        [DataMember]
        public string Info
        {
            get { return info; }
            set { info = value; }
        }
        [DataMember]
        public string TokenUser
        {
            get { return tokenUser; }
            set { tokenUser = value; }
        }
        [DataMember]
        public string TokenApp
        {
            get { return tokenApp; }
            set { tokenApp = value; }
        }
        [DataMember]
        public object[] Data
        {
            get { return data; }
            set { data = value; }
        }


    }
}
