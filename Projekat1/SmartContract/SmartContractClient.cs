using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SmartContract
{
    internal class SmartContractClient : ISmartContractClient
    {
        public void Calculate(Client client)
        {
            Console.WriteLine("Calculate" + client.FormattedString);

            Random rand = new Random();

            int numberOfMiners = SmartContractMiner.Miners.Count;
            int minerNumber = rand.Next(numberOfMiners);

            Console.WriteLine("Choosing miner with id : " + SmartContractMiner.Miners[minerNumber].Id);
            string address = SmartContractMiner.Miners[minerNumber].Address + "/IMinerSM";
            NetTcpBinding binding = new NetTcpBinding();

            ChannelFactory<IMinerSM> channel = new ChannelFactory<IMinerSM>(binding, address);

            IMinerSM proxy = channel.CreateChannel();
            proxy.CalculateTask(client);
        }
    }
}
