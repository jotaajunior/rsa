using System;

namespace DotRsa
{
    public static class Utils
    {
        private static readonly Random RandomGenerator = new Random();
        private const int MinNumber = 5000;
        private const int MaxNumber = 7000;
        
        private static bool IsEven(int number)
        {
            return number % 2 == 0;
        }

        private static bool IsPrime(int number)
        {
            if (number <= 2)
            {
                return true;
            }

            if (IsEven(number))
            {
                return false;
            }

            var maxPossibleDivisor = (int) Math.Floor(Math.Sqrt(number));
            
            for (var i = 3; i <= maxPossibleDivisor; i += 2)
            {
                var isDivisible = number % i == 0;

                if (isDivisible)
                {
                    return false;
                }
            }

            return true;
        }

        private static int Gcd(int a, int b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }

            return a | b;
        }

        private static bool IsCoprime(int a, int b)
        {
            return Gcd(a, b) == 1;
        }
        
        private static int GetRandomNumber() => RandomGenerator.Next(MinNumber, MaxNumber);

        public static int GetPrime()
        {
            var selectedNumber = GetRandomNumber();

            while (!IsPrime(selectedNumber))
            {
                selectedNumber = GetRandomNumber();
            }

            return selectedNumber;
        }

        public static int GetE(int n, int t)
        {
            for (var e = 3; e < t; ++e)
            {
                if (IsCoprime(n, e) && IsCoprime(e, t))
                {
                    return e;
                }
            }

            throw new Exception("Error while trying to find E");
        }

        public static int GetD(int e, int t)
        {
            var d = 1;
            
            while (true)
            {
                if (e == d)
                {
                    ++d;
                }
                
                var module = (d * e) % t;
                
                if (module == 1)
                {
                    return d;
                }
                
                ++d;
            }
        }
    }
}