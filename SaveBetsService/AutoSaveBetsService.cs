// -----------------------------------------------------------------------
// <copyright file="AutoSaveBetsService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Franklin.Nguyen" date="2014.04.21">Created</change>
// </history>

namespace AutoSaveBetsService
{
    using System;
    using System.IO;
    using System.ServiceProcess;
    using System.Timers;
    using Fanex.Common;
    using Fanex.Data.Configuration;
    using SaveBetsBusiness;
    using SaveBetsBusiness.Common;
    using SaveBetsBusiness.Service;

    /// <summary>
    /// Auto mail service.
    /// </summary>
    public partial class AutoSaveBetsService : ServiceBase
    {
        /// <summary>
        /// The timer.
        /// </summary>
        private static Timer timer;

        private static SaveBetsBusinessService service;

        public AutoSaveBetsService()
        {
            InitializeComponent(AppSettings.ServiceName);
        }

        /// <summary>
        /// On start service.
        /// </summary>
        /// <param name="args">Data passed by the start command.</param>
        protected override void OnStart(string[] args)
        {
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

            DbSettingProviderManager.Current.Start();
            timer = new Timer();
            timer.Interval = AppSettings.Interval;
            timer.Elapsed += new ElapsedEventHandler(OnInterval);
            timer.AutoReset = true;
            timer.Enabled = true;

            AppSettings.OnProcessing = false;
            AppSettings.ForceRunningOnProcessing = false;

            string ip = AppSettings.IP;
            string username = AppSettings.UserName;
            string password = AppSettings.Password;

            if (AppSettings.IsEncryptPassword)
            {
                password = Cryptography.CfsDeCrypt(password);
            }

            service = new SaveBetsBusinessService(ip, username, password);
        }

        protected override void OnStop()
        {
            timer.Stop();
            timer = null;
            service = null;

            AppSettings.OnProcessing = false;
            AppSettings.ForceRunningOnProcessing = false;

            GC.Collect();
        }

        /// <summary>
        /// Called when [interval].
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="e">The <see cref="ElapsedEventArgs"/> instance containing the event data.</param>
        private static void OnInterval(object source, ElapsedEventArgs e)
        {
            try
            {
                if (!AppSettings.OnProcessing)
                {
                    AppSettings.OnProcessing = true;

                    if (AppSettings.ForceRunning && !AppSettings.ForceRunningOnProcessing)
                    {
                        AppSettings.ForceRunningOnProcessing = true;
                    }

                    service.ProcessSaveBets();

                    AppSettings.OnProcessing = false;                    
                }              
            }
            catch (Exception ex)
            {
                AppSettings.OnProcessing = false;
                LogExtensions.ExceptionHandler(ex);
            }
        }
    }
}