using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonPushNotification
{
    public interface IPush
    {
        /// <summary>
        /// main function for sending notifications
        /// </summary>
        void sendNotification();

        /// <summary>
        /// getting GCM message and content
        /// </summary>
        /// <param name="messageContent"></param>
        /// <param name="title"></param>
        /// <param name="pushNotificationID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        string getGCMMessage(Object messageContent, string title, Guid pushNotificationID, int clientID);

        /// <summary>
        /// getting APNS message and content
        /// </summary>
        /// <param name="messageContent"></param>
        /// <param name="title"></param>
        /// <param name="pushNotificationID"></param>
        /// <param name="clientID"></param>
        /// <returns></returns>
        string getAPNSMessage(Object messageContent, string title, Guid pushNotificationID, int clientID);
    }
}
