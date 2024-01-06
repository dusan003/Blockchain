using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [DataContract]
    public class Client
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int Value { get; set; }
        [DataMember]
        public DateTime LocalDate { get; set; }
        [DataMember]
        public string FormattedString { get; set; }

        public Client()
        {
            Id = 0;
            Value = 0;
            LocalDate = DateTime.Now;
            FormattedString = "id : " + Id + "  " + "Value : " + Value + " " + "Local date : " + LocalDate.ToString();
        }

        public Client(int id, int value)
        {
            Id = id;
            Value = value;
            LocalDate = DateTime.Now;
            FormattedString = "id : " + Id + "  " + "Value : " + Value + " " + "Local date : " + LocalDate.ToString();
        }
    }

}
