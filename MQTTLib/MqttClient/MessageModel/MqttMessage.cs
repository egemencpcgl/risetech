using MqttClient.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MqttClient.MessageModel
{
    public class MqttMessage
    {
        public MessageTopic MessageTopic { get; set; }
        public MessageType MessageType { get; set; }
        public byte[] Data { get; set; }

    }
}
