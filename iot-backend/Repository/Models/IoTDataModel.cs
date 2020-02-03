using System;
using System.Collections.Generic;

namespace iot_backend.Repository.Models
{
    public class IoTDataModel
    {   
        public int MessageId { get; set; }
        
        public string DeviceId { get; set; }
        
        public DateTime MessageDate { get; set; }

        public float Temp { get; set; }
        
        public float Humidity { get; set; }
    }
}