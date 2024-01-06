using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blockchain
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string address = "net.tcp://localhost:4000/ISmartContractMiner";
            NetTcpBinding binding = new NetTcpBinding();

            ChannelFactory<ISmartContractMiner> channel = new ChannelFactory<ISmartContractMiner>(binding, address);

            ISmartContractMiner proxy = channel.CreateChannel();
            Miner miner = proxy.LoginMiner();

            // Start server for miner
            using (ServiceHost host = new ServiceHost(typeof(MinerBlockchain)))
            {
                address = miner.Address + "/IMinerBlockchain";
                binding = new NetTcpBinding();
                MinerBlockchain.Id = miner.Id;

                host.AddServiceEndpoint(typeof(IMinerBlockhain), binding, address);

                host.Open();
                Console.WriteLine($"Miner started successfully with id " + miner.Id + " on address : " + address);
                Console.ReadLine();
                host.Close();
            }
        }
    }
}
