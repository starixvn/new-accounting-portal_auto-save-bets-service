// -----------------------------------------------------------------------
// <copyright file="SaveBetsBusinessInfo.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Franklin.Nguyen" date="2014.04.21">Created</change>
// </history>

namespace SaveBetsBusiness.Model
{
    using System;
    using System.Collections.Generic;
    using SaveBetsBusiness.Extensions;
    using SaveBetsBusiness.Resources;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SaveBetsBusinessInfo
    {
        public SaveBetsBusinessInfo()
        {
            Sportbook = new List<SaveBetsBusinessInfo>();
            ThirdPartySport = new List<SaveBetsBusinessInfo>();
        }

        public string Date
        {
            get
            {
                return string.Format(SaveBetsBusinessResource.SaveBetsBusiness_ReportTitle, DateTime.Now.AddDays(-1).ToShortFormatedDate());
            }
        }

        public string GroupName { get; set; }

        public string ProductName { get; set; }

        public decimal WinLoss { get; set; }

        public decimal MonthToDateWinLoss { get; set; }

        public bool IsEnoughData { get; set; }

        public List<SaveBetsBusinessInfo> Sportbook { get; set; }

        public List<SaveBetsBusinessInfo> ThirdPartySport { get; set; }

        public SaveBetsBusinessInfo SportbookTotal { get; set; }

        public SaveBetsBusinessInfo Total { get; set; }

        public void CalculateTotal(SaveBetsBusinessInfo saveBetsBusinessInfo)
        {
            this.WinLoss += saveBetsBusinessInfo.WinLoss;
            this.MonthToDateWinLoss += saveBetsBusinessInfo.MonthToDateWinLoss;
        }
    }
}