using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookComputing.XmlRpc;

namespace SOA_Client
{
    //Объявление прокси-класса для работы по протоколу XML-RPC
    [XmlRpcUrl("http://myservice.ru/xmlrpc.api.php")]
    public interface IMyProxy : IXmlRpcProxy
    {
        [XmlRpcMethod("myservice:CreatePerson")]
        int CreatePerson(XMLRPC_Person Person);

        [XmlRpcMethod("myservice:ListPeople")]
        XMLRPC_Person[] ListPeople();

        [XmlRpcMethod("myservice:ReadPerson")]
        XMLRPC_Person ReadPerson(int id);

        [XmlRpcMethod("myservice:UpdatePerson")]
        bool UpdatePerson(int id, XMLRPC_Person Person);

        [XmlRpcMethod("myservice:DeletePerson")]
        bool DeletePerson(int id);

        //---------------------------------------------

        [XmlRpcMethod("myservice:CreateLanguage")]
        int CreateLanguage(XMLRPC_Language language);

        [XmlRpcMethod("myservice:ListLanguages")]
        XMLRPC_Language[] ListLanguages();

        [XmlRpcMethod("myservice:ReadLanguage")]
        XMLRPC_Language ReadLanguage(int id);
        
        [XmlRpcMethod("myservice:UpdateLanguage")]
        bool UpdateLanguage(int id, XMLRPC_Language Language);
        
        [XmlRpcMethod("myservice:DeleteLanguage")]
        bool DeleteLanguage(int id);
    }

    //Объявление необходимых структур данных
    public struct XMLRPC_Language
    {
        public int ID;
        public string Name;
    }

    public struct XMLRPC_Person
    {
        public int ID;
        public string Name;
        public int Age;
        public string Mail;
        [XmlRpcMissingMapping(MappingAction.Ignore)]
        public int LanguageID;
        [XmlRpcMissingMapping(MappingAction.Ignore)]
        public string Language;        
    }
}
