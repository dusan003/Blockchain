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
            Id = 0; // Initialize to zero, actual value will be set when added to the blockchain
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
                // Generisi random broj za Solution
                Solution = random.Next(int.MaxValue);

                // Ponovno izračunaj hash s novim Solution
                Hash = CalculateHash();

            } while (!Hash.StartsWith("0"));

            solution = Solution;
            return true;
        }

        public bool ValidateBlock(int value)
        {
            // Ponovno izračunaj hash s novim Solution
            Hash = CalculateHash();
            // Provjeri uslov da hash počinje s "0"
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
                    builder.Append(hashBytes[i].ToString("x2")); // Konvertuj byte u heksadecimalni zapis
                }

                return builder.ToString();
            }
        }
    }
}
