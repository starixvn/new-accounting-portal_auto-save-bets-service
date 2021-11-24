using Fanex.Common;
using Fanex.Logging;
using Fanex.MailDelivery.Core.Models;
using Newtonsoft.Json;
using SaveBetsBusiness;
using SaveBetsBusiness.Service;
using System;

namespace Fanex.SaveBets.Testing
{
    public class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                RegisterService.RegisterServices();

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
                    Mail = new Mail(),
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
    }
}