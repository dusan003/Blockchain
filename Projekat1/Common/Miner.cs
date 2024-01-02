using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [DataContract]
    public class Miner
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public bool Started { get; set; }
        [DataMember]
        public string Address { get; set; }

        public Miner(int id, int currentPort)
        {
            Id = id;
            Address = "net.tcp://localhost:" + currentPort;
            Started = true;
        }
    }
}
