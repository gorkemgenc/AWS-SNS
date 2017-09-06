# Amazon-SNS-PushNotification-Service-CSharp

- Explanations

AWS SDK push notification service is used for sending push notification to users. With SNS you can send messages to a large number of subscribers, including distributed systems and services, and mobile devices. Also you can watch the usage of Amazon SNS service with Amazon CloudWatch platform.  

- Implementations

 => JSON format for sending push notification to APNS and GCM      
            
// Constructing Google GCM message          
public string getGCMMessage(object notification, string title)        
{      

     Dictionary<string, object> payload = new Dictionary<string, object>();                                         
     payload.Add("message", title);                   
     payload.Add("objectInfo", notification);             
     Dictionary<string, object> androidMessageMap = new Dictionary<string, object>();       
     androidMessageMap.Add("collapse_key", "Welcome");        
     androidMessageMap.Add("data", payload);              
     androidMessageMap.Add("delay_while_idle", true);      
     androidMessageMap.Add("time_to_live", 0);          
     androidMessageMap.Add("dry_run", false);            
     return JsonConvert.SerializeObject(androidMessageMap);      
}       
      
/// Constructing Apple APNS message content      
public string getAPNSMessage(object notification, string title)      
{         

     Dictionary<string, object> appleMessageMap = new Dictionary<string, object>();         
     Dictionary<string, object> appMessageMap = new Dictionary<string, object>();          
     appMessageMap.Add("alert", title);         
     appMessageMap.Add("badge", 1);         
     appMessageMap.Add("sound", "default");          
     appMessageMap.Add("objectInfo", notification);          
     appleMessageMap.Add("aps", appMessageMap);        
     return JsonConvert.SerializeObject(appleMessageMap);           
}       
          
- This project was implemented with Abstract Factory Design Pattern for adding another sending message service if necessary.
     
- OneSignal vs AWS-SNS

OneSignal is free to use and you can implement onesignal with any language. There are some difference between AmazonSNS and OneSignal. The
first difference is Amazon SNS has much more document than OneSignal. Additionally, AWS-SDK is not free but price of using AWS-SDK is cheap. Notice that, if you have distributed system and different projects, you can easily monitor the using AWS SDK on the Amazon CloudWatch. On the other hand, Onesignal Monitoring System is a harder than Amazon CloudWatch for different projects.
