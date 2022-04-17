using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SharpChain.Model
{
    public class Block
    {
        private long _nonce;
        private readonly DateTime _timeStamp;

        public string PreviousHash { get; set; }
        public IList<Transaction> Transactions { get; set; }
        public string Hash { get; private set; }

        public Block(DateTime timeStamp, List<Transaction> transactions, string previousHash = default)
        {
            _timeStamp = timeStamp;
            _nonce = 0;
            PreviousHash = previousHash;
            Transactions = transactions;
            Hash = CreateHash();
        }

        public void MineBlock(int proofOfWorkDifficulty = 2)
        {
            var hashValidationTemplate = new string('0', proofOfWorkDifficulty);
            while (Hash[..proofOfWorkDifficulty] != hashValidationTemplate)
            {
                _nonce++;
                Hash = CreateHash();
            }
            Console.WriteLine("Block with HASH={0} Successfully mined", Hash);
        }

        public string CreateHash()
        {
            using var sHA256 = new HMACSHA512();
            var rawData = string.Format("PreviousHash:{0}, TimeStamp:{1}, Nonce:{2}, Transations:{3}",
                PreviousHash, _timeStamp.Ticks, _nonce, string.Join(" # ", Transactions));
            var bytes = sHA256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            //return Encoding.Default.GetString(bytes);
            return Convert.ToBase64String(bytes);
        }
    }
}
