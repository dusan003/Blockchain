using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Blockchain
    {
        public List<Block> Blocks { get; private set; }

        public Blockchain()
        {
            Blocks = new List<Block>();
        }

        public void AddToChain(Block block)
        {
            block.Id = Blocks.Count + 1;
            Blocks.Add(block);
        }
    }
}
