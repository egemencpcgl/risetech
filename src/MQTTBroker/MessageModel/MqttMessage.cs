using MQTTBroker.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTTBroker.MessageModel
{
    public class MqttMessage
    {
        public Guid MessageId { get; set; }
        public MessageTopic MessageTopic { get; set; }
        public MessageType MessageType { get; set; }
        public byte[] MessageData { get; set; }

    }
}
