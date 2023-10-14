﻿namespace MyWebApi.Hangfire.Services
{
    public class ServiceManagement : IServiceManagement
    {
        public void GenerateMerchandise()
        {
            Console.WriteLine($"Generate Merchandise: long running service at {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
        }

        public void SendEmail()
        {
            Console.WriteLine($"Send Email: delayed execution service at {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
        }

        public void SyncRecords(string jid)
        {
            Console.WriteLine($"Sync Records: at {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} JobID: {jid}");
        }

        public void UpdateDatabase()
        {
            Console.WriteLine($"Update Database: at {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
        }
    }
}
