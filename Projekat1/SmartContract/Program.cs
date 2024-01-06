using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SmartContract
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Start server for client
            using (ServiceHost host = new ServiceHost(typeof(SmartContractClient)))
            {
                string address = "net.tcp://localhost:4000/ISmartContractClient";
                NetTcpBinding binding = new NetTcpBinding();

                host.AddServiceEndpoint(typeof(ISmartContractClient), binding, address);

                host.Open();
                Console.WriteLine($"The service was successfully started at the address : {address}");

                // Start server for miner
                using (ServiceHost hostMiner = new ServiceHost(typeof(SmartContractMiner)))
                {
                    string addressMiner = "net.tcp://localhost:4000/ISmartContractMiner";
                    NetTcpBinding bindingMiner = new NetTcpBinding();

                    hostMiner.AddServiceEndpoint(typeof(ISmartContractMiner), bindingMiner, addressMiner);

                    hostMiner.Open();
                    Console.WriteLine($"The service was successfully started at the address : {addressMiner}");
                    Console.ReadLine();
                    hostMiner.Close();
                }

                host.Close();

            }


        }
    }
}
