using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonPushNotification
{
    public class PushFactory : AbstractFactory
    {
        /// <summary>
        /// for getting push notification
        /// </summary>
        /// <param name="serviceInformation"></param>
        /// <param name="notificationInformation"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        override
        public IPush getPush(Requirements service, object notification, string title)
        {
            Requirements requirement = service;

            if (requirement.getStructures().getService() == Services.AMAZON_SNS)
                return new Notifications(service, notification, title);

            return null;
            
        }
    }
}
