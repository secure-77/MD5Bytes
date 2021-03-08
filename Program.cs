using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace md5bytes
{
    class Program
    {
        static void Main(string[] args)
        {
            
            if (args.Length == 0)
            {
                //ManualCheck();
                PrintHelp();

            } else if (args[0].Equals("-h"))
            {
                PrintHelp();
            }

            else if (args[0].Equals("-c"))
            {
                ManualCheck(args[1]);
            }
            else {
                Decrypt(args[0], args[1], args[2]);
            }

            Environment.Exit(0);
        }


        private static void PrintHelp()
        {
            Console.WriteLine("Help");
            Console.WriteLine("md5bytes <wordlist> <inputfile> <outputfile>");
            Console.WriteLine("-c       check mode");
            Console.WriteLine("-h       show this help");
        }


        private static void ManualCheck(string inputClearPassword)
        {
            Console.WriteLine("Password: " + inputClearPassword);
            
            // get ASCII Bytes from String
            byte[] bytes1 = Encoding.ASCII.GetBytes(inputClearPassword);
            string input1 = string.Join(" ", bytes1.Select(b => b.ToString()));
            Console.WriteLine("String in ASCII Bytes: " + input1);

            // hash Bytes 
            byte[] bytes2 = new MD5CryptoServiceProvider().ComputeHash(bytes1);
            string input2 = string.Join(" ", bytes2.Select(b => b.ToString()));
            Console.WriteLine("Bytes after MD5 Hash: " + input2);
            Console.WriteLine("MD5 Sum: " + GetMD5Hash(bytes2));


            // get hashed ASCII Bytes 
            string input3 = Encoding.ASCII.GetString(bytes2);
            Console.WriteLine("String from ASCII Bytes after MD5 Hash: " + input3);

            // backward string in bytes
            byte[] bytes3 = Encoding.ASCII.GetBytes(input3);
            string input4 = string.Join(" ", bytes3.Select(b => b.ToString()));
            Console.WriteLine("Reverse:");
            Console.WriteLine("Bytes from ASCII String after MD5 Hash: " + input4);
            Console.WriteLine("MD5 Sum: " + GetMD5Hash(bytes3));

            Console.ReadKey();
            Environment.Exit(0);
        }
    

        private static void Decrypt(string wordlist, string input, string output)
        {

            try
            {

            int counter = 0;
            string line;
            string word;

            StreamReader file = new StreamReader(input);
            while ((line = file.ReadLine()) != null)
            {
                Console.WriteLine("Read binary data: " + line);
                byte[] inputPasswordBytes = Encoding.ASCII.GetBytes(line);

                // hash wordlist password
                bool found = false;

                StreamReader wlist = new StreamReader(wordlist);
                while ((word = wlist.ReadLine()) != null && found == false)
                {
                    string convertedWord = HashPassword(word);
                    if (convertedWord.Equals(line))
                    {
                        WriteHasheToFile(output, line + ":" + word);
                        Console.WriteLine("found password!!!!");
                        Console.WriteLine("hash input    : " + line);
                        Console.WriteLine("wordlist input: " + convertedWord);
                        Console.WriteLine("Password: " + word);
                        found = true;
                    }
                    counter++;
                }

                wlist.Close();
                Console.WriteLine("finished");
                Console.WriteLine("{0} wordlist entries calculated", counter);

            }

            file.Close();         

            }
            catch (global::System.Exception e)
            {

                Console.WriteLine(e);
            }

        }

        public static void WriteHasheToFile(string output, string hash)
        {
             // This text is added only once to the file.
            if (!File.Exists(output))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(output))
                {
                    sw.WriteLine(hash);
                }
            } else
            {
                using (StreamWriter sw = File.AppendText(output))
                {
                    sw.WriteLine(hash);
                }

            }
        }


        private static string HashPassword(string password)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(password);
            byte[] bytes2 = new MD5CryptoServiceProvider().ComputeHash(bytes);
            string strangeChars = Encoding.ASCII.GetString(bytes2);
            return strangeChars;
        }

        private static string GetMD5Hash(byte[] password)
        {
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < password.Length; i++)
            {
                //string test = password[i].ToString("x2");
                sBuilder.Append(password[i].ToString("x2"));//get hash value
            }

            return sBuilder.ToString();

        }


    }
}
