using System;
using System.Numerics;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace DotRsa
{
    public class Rsa
    {
        private readonly int _p;
        private readonly int _q;
        private readonly int _n;
        private readonly int _t;
        private readonly int _d;
        private readonly int _e;

        public Rsa()
        {
            _p = Utils.GetPrime();
            _q = Utils.GetPrime();
            
            Console.WriteLine($"P: {_p}");
            Console.WriteLine($"Q: {_q}");
            
            _n = _p * _q;
            
            Console.WriteLine($"N: {_n}");
            
            _t = (_p - 1) * (_q - 1);
            
            Console.WriteLine($"T: {_t}");
            
            _e = Utils.GetE(_n, _t);
            _d = Utils.GetD(_e, _t);
            
            Console.WriteLine($"E: {_e} | D: {_d}");
        }

        public IEnumerable<string> Encrypt(string message)
        {
            return message
                .ToCharArray()
                .Select((charCode) => BigInteger.ModPow(charCode, _e, _n))
                .Select(encrypted => encrypted.ToString("x"));
        }

        public string Decrypt(IEnumerable<string> message)
        {
            return string.Join("", message
                .Select((hexCode) => BigInteger.Parse(hexCode, NumberStyles.HexNumber))
                .Select((encrypted) => (char) BigInteger.ModPow(encrypted, _d, _n)));
        }
    }
}