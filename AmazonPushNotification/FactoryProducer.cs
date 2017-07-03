using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonPushNotification
{
    public class FactoryProducer    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="structure"></param>
        /// <returns></returns>
        public static AbstractFactory getFactory(Structures structure)  {

            if (structure.getFactory() == Factories.PUSH_NOTIFICATION)
                return new PushFactory();

            return null;
        }
    }
}
