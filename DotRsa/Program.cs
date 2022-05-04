using System;

namespace DotRsa
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var rsaInstance = new Rsa();

            var encrypted = rsaInstance.Encrypt("Mensagem secreta");

            var encryptedMessage = string.Join(" ", encrypted);
            var decryptedMessage = rsaInstance.Decrypt(encrypted);
            
            Console.WriteLine(encryptedMessage);
            Console.WriteLine(decryptedMessage);
        }
    }
}