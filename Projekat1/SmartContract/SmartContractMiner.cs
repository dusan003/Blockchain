using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartContract
{
    public class SmartContractMiner : ISmartContractMiner
    {
        static readonly object Identity = new object();

        public static List<Miner> Miners { get; set; } = new List<Miner>();
        private static int currentPort = 4001;
        public Miner LoginMiner()
        {
            Miner miner;
            lock (Identity)
            {
                int id = 0;
                foreach (var item in Miners)
                {
                    if (item.Id > id)
                    {
                        id = item.Id;
                    }
                }

                id += 1;
                miner = new Miner(id, currentPort);
                Miners.Add(miner);

                currentPort += 1;
            }
            return miner;
        }

        public List<Miner> ReturnAllMiners(int id)
        {
            List<Miner> miners = new List<Miner>();
            lock (Identity)
            {
                foreach (var item in Miners)
                {
                    if (item.Id != id)
                    {
                        miners.Add(item);
                    }
                }
            }
            return miners;
        }
    }
}
