using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonPushNotification
{
    public abstract class AbstractFactory   {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        /// <param name="notification"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public abstract IPush getPush(Requirements requirement, object notification, string title);
    }
}
