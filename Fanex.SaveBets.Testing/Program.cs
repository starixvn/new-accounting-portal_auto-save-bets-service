using System;
using System.IO;
using Fanex.Common;
using Fanex.Data.Configuration;
using ONEbook.UM.Services;
using ONEbook.UM.Services.Impl;
using SaveBetsBusiness;
using SaveBetsBusiness.Service;

namespace Fanex.SaveBets.Testing
{
    public class Program
    {
        private static void Main(string[] args)
        {
            IMaintenanceService um = new MaintenanceService();
            var downTime = um.GetUnderMaintenanceTime();
            if (downTime.IsUnderMaintenanceTime)
            {
                Console.WriteLine("---------------UM---------------");
                return;
            }

            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

            DbSettingProviderManager.Current.Start();

            AppSettings.OnProcessing = false;
            AppSettings.ForceRunningOnProcessing = true;

            string ip = AppSettings.IP;
            string username = AppSettings.UserName;
            string password = AppSettings.Password;

            if (AppSettings.IsEncryptPassword)
            {
                password = Cryptography.CfsDeCrypt(password);
            }

            var service = new SaveBetsBusinessService(ip, username, password);
            service.ProcessSaveBets();

            Console.WriteLine("---------------DONE---------------");
        }
    }
}