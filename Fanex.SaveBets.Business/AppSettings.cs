// -----------------------------------------------------------------------
// <copyright file="AppSettings.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="" date="2014.04.21">Created</change>
// </history>

namespace SaveBetsBusiness
{
    using System;
    using System.Configuration;

    /// <summary>
    /// Application setting.
    /// </summary>
    public static class AppSettings
    {
        /// <summary>
        /// Gets the name of the service.
        /// </summary>
        /// <value>
        /// The name of the service.
        /// </value>
        public static string ServiceName
        {
            get
            {
                var val = ConfigurationManager.AppSettings["ServiceName"];
                return val == null ? "_SaveBetsBusiness.Service" : val;
            }
        }

        /// <summary>
        /// Gets the interval in second.
        /// </summary>
        /// <value>
        /// The interval.
        /// </value>
        public static int Interval
        {
            get
            {
                var val = ConfigurationManager.AppSettings["Interval.Second"];
                int interval = val == null ? 10 : Convert.ToInt32(val) * 1000;
                return interval;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [on processing].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [on processing]; otherwise, <c>false</c>.
        /// </value>
        public static bool OnProcessing { get; set; }

        public static bool ForceRunning
        {
            get
            {
                var val = ConfigurationManager.AppSettings["ForceRunning"];
                return val == null ? false : (Convert.ToInt32(ConfigurationManager.AppSettings["ForceRunning"]) == 1);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [force running on processing].
        /// </summary>
        /// <value>
        /// <c>true</c> if [force running on processing]; otherwise, <c>false</c>.
        /// </value>
        public static bool ForceRunningOnProcessing { get; set; }

        public static DateTime WinlostDateForceRunning
        {
            get
            {
                var val = ConfigurationManager.AppSettings["WinlostDateForceRunning"];
                return val == null ? DateTime.Today : Convert.ToDateTime(ConfigurationManager.AppSettings["WinlostDateForceRunning"]);
            }
        }

        /// <summary>
        /// Gets run at hours.
        /// </summary>
        /// <value>
        /// The Hours.
        /// </value>
        public static int RunAtHours
        {
            get
            {
                var val = ConfigurationManager.AppSettings["RunAtHours"];
                return val == null ? 3 : Convert.ToInt32(ConfigurationManager.AppSettings["RunAtHours"]);
            }
        }

        /// <summary>
        /// Gets run at minutes.
        /// </summary>
        /// <value>
        /// The Minute.
        /// </value>
        public static int RunAtMinutes
        {
            get
            {
                var val = ConfigurationManager.AppSettings["RunAtMinutes"];
                return val == null ? 0 : Convert.ToInt32(ConfigurationManager.AppSettings["RunAtMinutes"]);
            }
        }

        /// <summary>
        /// Gets run at minutes.
        /// </summary>
        /// <value>
        /// The Minute.
        /// </value>
        public static int RunNextTimeInMinutes
        {
            get
            {
                var val = ConfigurationManager.AppSettings["RunNextTimeInMinutes"];
                return val == null ? 10 : Convert.ToInt32(ConfigurationManager.AppSettings["RunNextTimeInMinutes"]);
            }
        }

        public static string ExportDataDirectory
        {
            get
            {
                var val = ConfigurationManager.AppSettings["ExportDataDirectory"];
                return val == null ? string.Empty : Convert.ToString(ConfigurationManager.AppSettings["ExportDataDirectory"]);
            }
        }

        public static string MailTo
        {
            get
            {
                var val = ConfigurationManager.AppSettings["to"];
                return val == null ? string.Empty : Convert.ToString(ConfigurationManager.AppSettings["to"]);
            }
        }

        public static string MailCcTo
        {
            get
            {
                var val = ConfigurationManager.AppSettings["cc"];
                return val == null ? string.Empty : Convert.ToString(ConfigurationManager.AppSettings["cc"]);
            }
        }

        public static string MailBCcTo
        {
            get
            {
                var val = ConfigurationManager.AppSettings["bcc"];
                return val == null ? string.Empty : Convert.ToString(ConfigurationManager.AppSettings["bcc"]);
            }
        }

        public static string MailSubject
        {
            get
            {
                var val = ConfigurationManager.AppSettings["Subject"];
                return val == null ? string.Empty : Convert.ToString(ConfigurationManager.AppSettings["Subject"]);
            }
        }

        public static string MailTitle
        {
            get
            {
                var val = ConfigurationManager.AppSettings["Title"];
                return val == null ? string.Empty : Convert.ToString(ConfigurationManager.AppSettings["Title"]);
            }
        }

        public static string AdminEmail
        {
            get
            {
                var val = ConfigurationManager.AppSettings["AdminEmail"];
                return val == null ? string.Empty : Convert.ToString(ConfigurationManager.AppSettings["AdminEmail"]);
            }
        }

        public static int MatchId
        {
            get;
            set;
        }

        public static int ForceRunningWithMatchId
        {
            get
            {
                var val = ConfigurationManager.AppSettings["ForceRunning.MatchId"];
                return val == null ? 0 : Convert.ToInt32(ConfigurationManager.AppSettings["ForceRunning.MatchId"]);
            }
        }

        public static string IP
        {
            get
            {
                var val = ConfigurationManager.AppSettings["ServerFile.IP"];
                return val == null ? string.Empty : Convert.ToString(ConfigurationManager.AppSettings["ServerFile.IP"]);
            }
        }

        public static string UserName
        {
            get
            {
                var val = ConfigurationManager.AppSettings["ServerFile.UserName"];
                return val == null ? string.Empty : Convert.ToString(ConfigurationManager.AppSettings["ServerFile.UserName"]);
            }
        }

        public static string Password
        {
            get
            {
                var val = ConfigurationManager.AppSettings["ServerFile.Password"];
                return val == null ? string.Empty : Convert.ToString(ConfigurationManager.AppSettings["ServerFile.Password"]);
            }
        }

        public static bool IsEncryptPassword
        {
            get
            {
                var val = ConfigurationManager.AppSettings["ServerFile.IsEncryptPassword"];
                return val == null ? false : Convert.ToString(ConfigurationManager.AppSettings["ServerFile.IsEncryptPassword"]) == "true";
            }
        }
    }
}