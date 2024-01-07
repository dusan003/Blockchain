using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Block
    {
        public int Id { get; set; }
        public int PreviousId { get; set; }
        public Client ClientData { get; set; }
        public DateTime Timestamp { get; set; }
        public int Nonce { get; set; }
        public int Solution { get; set; }
        public string Hash { get; private set; }

        public Block(Client client)
        {
            Id = 0;
            ClientData = client;
            Timestamp = DateTime.UtcNow;
            Nonce = 0;
            Solution = 0;
            Hash = CalculateHash();
        }
        public Random random = new Random();
        public bool Solve(out int solution)
        {
            do
            {
                Solution = random.Next(int.MaxValue);
                Hash = CalculateHash();
            } while (!Hash.StartsWith("0"));

            solution = Solution;
            return true;
        }

        public bool ValidateBlock(int value)
        {
            Hash = CalculateHash();
            return Hash.StartsWith("0");
        }

        private string CalculateHash()
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                string dataToHash = $"{Id}-{PreviousId}-{ClientData}-{Timestamp}-{Nonce}-{Solution}";
                byte[] bytes = Encoding.UTF8.GetBytes(dataToHash);
                byte[] hashBytes = sha256.ComputeHash(bytes);

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
