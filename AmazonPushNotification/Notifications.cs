using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Newtonsoft.Json;

namespace AmazonPushNotification
{
    public class Notifications : IPush
    {
        /// <summary>
        /// List of applicationInformation 
        /// </summary>
        private List<Application> applications;

        /// <summary>
        /// List of clientInformation
        /// </summary>
        private List<Client> clients;

        /// <summary>
        /// notificationContent -> (Sending mobile object)
        /// </summary>
        private object notification;

        /// <summary>
        /// Notification title
        /// </summary>
        private string title;

        /// <summary>
        /// Amazon SNS client
        /// </summary>
        private Amazon.SimpleNotificationService.AmazonSimpleNotificationServiceClient _client { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requirement"></param>
        /// <param name="notification"></param>
        /// <param name="title"></param>
        public Notifications(Requirements requirement, object notification, string title)   {

            this.applications = requirement.getApplications();
            this.clients = requirement.getClients();
            this.notification = notification;
            this.title = title;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Client> getClients()    {
            return this.clients;
        }

        /// <summary>
        /// Constructing android gcm message
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="title"></param>
        /// <param name="pushID"></param>
        /// <param name="clientID"></param>
        /// <returns></returns>
        public string getGCMMessage(object notification, string title, Guid pushID, int clientID)
        {
            Dictionary<string, object> payload = new Dictionary<string, object>();
            payload.Add("message", title);
            payload.Add("objectInfo", notification);
            payload.Add("pushNo", pushID);
            payload.Add("clientID", clientID);
            Dictionary<string, object> androidMessageMap = new Dictionary<string, object>();
            androidMessageMap.Add("collapse_key", "Welcome");
            androidMessageMap.Add("data", payload);
            androidMessageMap.Add("delay_while_idle", true);
            androidMessageMap.Add("time_to_live", 0);
            androidMessageMap.Add("dry_run", false);
            return JsonConvert.SerializeObject(androidMessageMap);
        }

        /// <summary>
        /// Constructing Apple APNS message content
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="title"></param>
        /// <param name="pushID"></param>
        /// <param name="clientID"></param>
        /// <returns></returns>
        public string getAPNSMessage(object notification, string title, Guid pushID, int clientID)
        {
            Dictionary<string, object> appleMessageMap = new Dictionary<string, object>();
            Dictionary<string, object> appMessageMap = new Dictionary<string, object>();
            appMessageMap.Add("alert", title);
            appMessageMap.Add("badge", 1);
            appMessageMap.Add("sound", "default");
            appMessageMap.Add("objectInfo", notification);
            appMessageMap.Add("pushNo", pushID);
            appMessageMap.Add("userID", clientID);
            appleMessageMap.Add("aps", appMessageMap);
            return JsonConvert.SerializeObject(appleMessageMap);
        }

        /// <summary>
        /// Amazon sns creating endpoint ARN
        /// </summary>
        /// <param name="pushToken"></param>
        /// <param name="applicationArn"></param>
        /// <returns></returns>
        private string createEndPointArn(string pushToken, string applicationArn)
        {
            if (string.IsNullOrEmpty(pushToken))
            {
                return null;
            }

            try
            {
                var response = _client.CreatePlatformEndpoint(new CreatePlatformEndpointRequest()
                {
                    PlatformApplicationArn = applicationArn,
                    Token = pushToken
                });

                return response.EndpointArn;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// This is the main function for sending notification
        /// </summary>
        public void sendNotification()
        {
            foreach (Client client in clients)  {

                int clientID = client.getClientID();

                #region AWS connection - This code block is same for every push message type - This is the basic block.

                _client = new AmazonSimpleNotificationServiceClient("XXXXXXX", "XXXXXXXXXX", Amazon.RegionEndpoint.EUCentral1);
                string token = client.getPushToken();
                Platfroms mobilOsType = client.getMobileType() == 0 ? Platfroms.IOS_PATFORM : Platfroms.ANDROID_PLATFORM;

                try
                {
                    string applicationArn = "";

                    try
                    {
                        applicationArn = applications[(int)mobilOsType].getArn();
                    }
                    catch
                    {
                        continue;
                    }

                    string endPointArn = createEndPointArn(token, applicationArn);

                    if (string.IsNullOrEmpty(endPointArn))
                    {
                        continue;
                    }
                    else
                    {
                        PublishRequest publishRequest = new PublishRequest();
                        Dictionary<string, string> messageMap = new Dictionary<string, string>();
                        messageMap.Add("default", "default message");
                        string platform = mobilOsType == 0 ? "APNS_SANDBOX" : "GCM";

                        // This application developed for IOS and Android 
                        if (mobilOsType != Platfroms.IOS_PATFORM && mobilOsType != Platfroms.ANDROID_PLATFORM)
                        {
                            continue;
                        }

                        int clientsID = client.getClientID();
                        Guid pushesID = client.getPushID(); 
                        string message = mobilOsType == 0 ? getAPNSMessage(notification, title, pushesID, clientsID) : getGCMMessage(notification, title, pushesID, clientsID);
                        messageMap.Add(platform, message);

                        publishRequest.TargetArn = endPointArn;
                        publishRequest.MessageStructure = "json";

                        string messageJSON = "";

                        try
                        {
                            messageJSON = JsonConvert.SerializeObject(messageMap);
                        }
                        catch
                        {
                            continue;
                        }

                        try
                        {
                            publishRequest.Message = messageJSON;
                            _client.Publish(publishRequest);
                        }
                        catch
                        {
                            continue;
                        }

                        try
                        {
                            // for deleting endPointArn 
                            DeleteEndpointRequest _request = new DeleteEndpointRequest();
                            _request.EndpointArn = endPointArn;
                            _client.DeleteEndpoint(_request);
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
                catch
                {
                    continue;
                }

                #endregion
            }
        }
    }
}

