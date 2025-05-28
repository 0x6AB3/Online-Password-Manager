using System;
using PasswordManagerUtilities;

namespace PBKDF2_Speed_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string password = "E$%GYgtyGj4";
            string salt = "fwef45yfDHE$%76gFJGR^";
            int iterations = 50000;
            int tests = 10;
            
            for (int i = 1; i <= 5; i++)
            {
                double total = 0;
                for (int j = 1; j <= tests; j++)
                {
                    DateTime start = DateTime.Now;
                    byte[] derivedKey = PBKDF2.DeriveKey(password, salt, iterations * i);
                    DateTime finish = DateTime.Now;
                    double deltaT = (finish - start).TotalMilliseconds;
                    Console.WriteLine($"Test: {i}:{j}\tIterations: {iterations * i}\tDelta Time: {deltaT}\tDerived Key: {Convert.ToBase64String(derivedKey)}");
                    total += deltaT;
                }
                double avgDeltaT = total / tests;
                Console.WriteLine($"Test {i}\tAverage Delta Time: {avgDeltaT}");
            }
            
            /*
            string password = "E$%GYggt456h74h567$H%^&H$%^&H$%^&H$%^H&YTUIIKYULyGj4";
            string salt = "fwef45yjfDHE$%fjghjf6huf6thur67uhr56uh76gFJGR^";
            int iterations = 100000;
            byte[] derivedKey = PBKDF2.DeriveKey(password, salt, iterations);
            byte[] auth = PBKDF2.DeriveKey(derivedKey, salt, iterations);
            Console.Write(System.Convert.ToBase64String(auth).Length);
            */
        }
    }
}
