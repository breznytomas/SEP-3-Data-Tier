using System;
using CarSharing_Database.Mediator;

namespace CarSharing_Database
{
    class Program
    {
        static void Main(string[] args)
        {
            new Server().Run();
        }
    }
}