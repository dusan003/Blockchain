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
                address = miner.Address + "/IMinerSM";
                binding = new NetTcpBinding();
                MinerBlockchain.Id = miner.Id;

                host.AddServiceEndpoint(typeof(IMinerBlockhain), binding, address);

                host.Open();
                Console.WriteLine($"Miner je uspesno pokrenut sa id-om " + miner.Id + " na adresi : " + address);
                Console.ReadLine();
                host.Close();
            }
        }
    }
}
