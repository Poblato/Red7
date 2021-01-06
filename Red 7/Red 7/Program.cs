using System;

namespace Red_7._0
{
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client(3, true, true);

            client.Debug();
        }
    }
}