using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat1
{

    public class Blockchain
    {
        private List<Block> _blocks;
        private Hasher _hasher;

        public Blockchain(Hasher hasher)
        {
            _blocks = new List<Block>();
            _hasher = hasher;
        }

        public void AddBlock(Block block)
        {
        }

        public bool ValidateChain()
        {
            return true;
        }
    }

}
