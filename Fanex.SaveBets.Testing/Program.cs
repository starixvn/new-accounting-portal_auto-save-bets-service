using System;
using System.IO;
using Fanex.Common;
using Fanex.Data.Configuration;
using Fanex.Logging;
using Fanex.MailDelivery.Client.Messages;
using Newtonsoft.Json;
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
            try
            {
                IMaintenanceService um = new MaintenanceService();
                var downTime = um.GetUnderMaintenanceTime();
                if (downTime.IsUnderMaintenanceTime)
                {
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

                JobResultMessage jobResult = new JobResultMessage()
                {
                    Mail = null,
                    JobStatus = MailDelivery.Client.Enumerations.JobStatus.SuccessWithoutSendmail
                };

                var json = JsonConvert.SerializeObject(jobResult);
                Console.Write(json);
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message);
            }
        }
    }
}