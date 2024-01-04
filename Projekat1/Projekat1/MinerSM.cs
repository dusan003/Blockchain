using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Blockchain
{
    public class MinerSM : IMinerSM
    {
        public static int Id { get; set; }

        public static int reward { get; set; } = 0;

        static readonly object Identity = new object();
        public static Common.Blockchain Blockchain { get; set; } = new Common.Blockchain();
        public void AddToChainConfirmed(Client client)
        {
            Block block = new Block(client);
            lock (Identity)
            {
                if (Blockchain.Blocks.Count > 0)
                {
                    block.PreviousId = Blockchain.Blocks[Blockchain.Blocks.Count - 1].Id;
                }
                else
                {
                    block.PreviousId = 0;
                }
                Blockchain.AddToChain(block);
            }
        }

        public void CalculateTask(Client client)
        {
            Console.WriteLine("Calculating task!!!");
            Console.WriteLine("Client data : " + client.FormattedString);

            // calculate
            int solution;
            Block block = new Block(client);
            bool isSolved = block.Solve(out solution);
            if (!isSolved)
            {
                Console.WriteLine("Failed to solve task");
                return;
            }

            Console.WriteLine("Solved successfully task with solution " + solution);

            string address = "net.tcp://localhost:4000/ISmartContractMiner";
            NetTcpBinding binding = new NetTcpBinding();

            ChannelFactory<ISmartContractMiner> channel = new ChannelFactory<ISmartContractMiner>(binding, address);
            ISmartContractMiner proxy = channel.CreateChannel();
            List<Miner> miners = proxy.ReturnAllMiners(Id);
            channel.Close();

            bool isValid = false;
            foreach (var item in miners)
            {
                address = item.Address + "/IMinerSM";
                binding = new NetTcpBinding();

                ChannelFactory<IMinerSM> channelMiner = new ChannelFactory<IMinerSM>(binding, address);

                IMinerSM proxyMiner = channelMiner.CreateChannel();

                isValid = proxyMiner.ValidateTask(client, solution);
                if (!isValid)
                {
                    Console.WriteLine("Not valid solution, can't solve.");
                    return;
                }
            }
            foreach (var item in miners)
            {
                address = item.Address + "/IMinerSM";
                binding = new NetTcpBinding();

                ChannelFactory<IMinerSM> channelMiner = new ChannelFactory<IMinerSM>(binding, address);

                IMinerSM proxyMiner = channelMiner.CreateChannel();
                proxyMiner.AddToChainConfirmed(client);
            }

            Console.WriteLine("Adding to chain");
            lock (Identity)
            {
                if (Blockchain.Blocks.Count > 0)
                {
                    block.PreviousId = Blockchain.Blocks[Blockchain.Blocks.Count - 1].Id;
                }
                else
                {
                    block.PreviousId = 0;
                }
                Blockchain.AddToChain(block);
            }
            reward++;
            Console.WriteLine("Added bitcoin: 1");
            Console.WriteLine("Total bitcoins: " + reward);
        }

        public bool ValidateTask(Client client, int value)
        {
            Console.WriteLine("Check validity :" + client.FormattedString);

            Block block = new Block(client);

            bool validity = block.ValidateBlock(value);
            if (validity)
            {
                Console.WriteLine("Valid block.");
            }
            else
            {
                Console.WriteLine("Not valid block");
            }

            return validity;
        }
    }
}
