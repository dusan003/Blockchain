using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [ServiceContract]
    public interface IMinerBlockhain
    {
        [OperationContract]
        void CalculateTask(Client client);
        [OperationContract]
        bool ValidateTask(Client client, int value);
        [OperationContract]
        void AddToChainConfirmed(Client client);
    }
}
