using Fanex.Common;
using Fanex.Data.Configuration;
using Fanex.Logging;
using Fanex.MailDelivery.Core.Models;
using Fanex.RemotingRef;
using Newtonsoft.Json;
using ONEbook.UM.Services;
using ONEbook.UM.Services.Impl;
using SaveBetsBusiness;
using SaveBetsBusiness.Service;
using System;
using System.Configuration;
using System.IO;

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
                RegisterRemotingRef();

                AppSettings.OnProcessing = false;
                AppSettings.ForceRunningOnProcessing = true;

                string username = AppSettings.UserName;
                string password = AppSettings.Password;

                if (AppSettings.IsEncryptPassword)
                {
                    password = Cryptography.CfsDeCrypt(password);
                }

                var service = new SaveBetsBusinessService(username, password);
                service.ProcessSaveBets();

                JobMessage jobResult = new JobMessage()
                {
                    Mail = null,
                    JobStatus = JobStatus.SuccessWithoutSendmail
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

        private static void RegisterRemotingRef()
        {
            RefDataTypeContainer
                .ConnectTo(new Uri(ConfigurationManager.AppSettings["RemoteRefURL"]))
                .FromApplication("Fanex.AutoSaveBets.Service");
        }
    }
}