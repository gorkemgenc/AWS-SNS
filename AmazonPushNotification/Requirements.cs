using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonPushNotification
{
    public class Requirements
    {
        /// <summary>
        /// This contains information about client
        /// </summary>
        private List<Client> clients;

        /// <summary>
        /// List of ApplicationInformation
        /// </summary>
        private List<Application> applications;

        /// <summary>
        /// StructureInformation
        /// </summary>
        private Structures structures;

        /// <summary>
        /// Three parameter constructor
        /// </summary>
        /// <param name="clientsInformation"></param>
        /// <param name="platformArnAndTypes"></param>
        /// <param name="structureInformation"></param>
        public Requirements(List<Client> clients, List<Application> applications, Structures structures)    {
            applications = new List<Application>();
            this.clients = clients;
            this.applications = applications;
            this.structures = structures;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Client> getClients()    {
            return this.clients;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Application> getApplications()  {
            return this.applications;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Structures getStructures()   {
            return this.structures;
        }

        /// <summary>
        /// Obtaining application information
        /// </summary>
        /// <param name="platform"></param>
        /// <returns></returns>
        public Application application(Platfroms platform)  {
            if (Platfroms.IOS_PATFORM == platform)
                return applications[0];
            else if (Platfroms.ANDROID_PLATFORM == platform)
                return applications[1];
            else
                return null;
        }
    }

    /// <summary>
    /// userID => BT_USER_ID (LMS_MOBILE_TOKENS)
    /// token => BT_PUSH_TOKEN
    /// mobilOSType => BT_MOBIL_OS: 0 - IOS, 1 - Android
    /// </summary>
    public class Client
    {
        /// <summary>
        /// clientID for obtaining push token.
        /// </summary>
        private int clientID = -1;

        /// <summary>
        /// This is obtaining by GCM or IOS Services.
        /// </summary>
        private string pushToken;

        /// <summary>
        /// 0 => IOS, 1 => Android
        /// </summary>
        private int mobileType;

        /// <summary>
        /// For saving click operation we prefer Guid notificationID
        /// </summary>
        private Guid pushID;

        /// <summary>
        /// identitity ID for monitoring which token is used for used. Notice one user can have one more mobile push tokens.
        /// </summary>
        private int identityID;

        /// <summary>
        /// Four parameter constructor
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="token"></param>
        /// <param name="mobilOSType"></param>
        /// <param name="pushNotificationID"></param>
        public Client(int clientID, string pushToken, int mobileType, Guid pushID, int identityID)  {
            this.clientID = clientID;
            this.pushToken = pushToken;
            this.mobileType = mobileType;
            this.pushID = pushID;
            this.identityID = identityID;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int getClientID()    {
            return this.clientID;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string getPushToken()    {
            return this.pushToken;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int getMobileType()  {
            return this.mobileType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Guid getPushID() {
            return this.pushID;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int getIdentityID()  {
            return this.identityID;
        }
    }

    /// <summary>
    /// This shows SNS application information.
    /// </summary>
    public class Application
    {
        /// <summary>
        /// PlatformTypes
        /// </summary>
        private Platfroms platform { get; set; }

        /// <summary>
        /// applicationArn
        /// </summary>
        private string SNSarn { get; set; }

        /// <summary>
        /// Two parameter constructor
        /// </summary>
        /// <param name="platformTypes"></param>
        /// <param name="applicationArn"></param>
        public Application(Platfroms platform, string SNSarn)  {
            this.platform = platform;
            this.SNSarn = SNSarn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Platfroms getPlatform()
        {
            return this.platform;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string getArn()
        {
            return this.SNSarn;
        }
    }

    /// <summary>
    /// This determine which service and structure is used.
    /// factory == 1 then Push Notification
    /// service == 1 => then Amazon SNS
    /// </summary>
    public class Structures
    {
        /// <summary>
        /// FactoryType
        /// </summary>
        private Factories factory;

        /// <summary>
        /// ServiceType
        /// </summary>
        private Services service;
        
        /// <summary>
        /// This is contructors. Default we use push notification factory and Amazon SNS services
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="service"></param>
        public Structures(Factories factory = Factories.PUSH_NOTIFICATION, Services service = Services.AMAZON_SNS)  {
            this.factory = factory;
            this.service = service;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Factories getFactory()   {
            return this.factory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Services getService()     {
            return this.service;
        }
    }

    /// <summary>
    /// This shows which factory type is used.
    /// </summary>
    public enum Factories   {
        PUSH_NOTIFICATION = 1
        // And others such as SMS_NOTIFICATION
    }

    /// <summary>
    /// This shows which push notification service is used.
    /// </summary>
    public enum Services    {
        AMAZON_SNS = 1,
        ONE_SIGNAL = 2
        // And others such as PushWoosh, one signal or others.
    }

    /// <summary>
    /// Platform type
    /// </summary>
    public enum Platfroms   {
        IOS_PATFORM = 0,
        ANDROID_PLATFORM = 1
        // And other platforms such as Windows operating systems.
    }
}
