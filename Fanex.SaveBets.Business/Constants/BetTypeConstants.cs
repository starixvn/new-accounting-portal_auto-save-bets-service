// -----------------------------------------------------------------------
// <copyright file="BetTypeConstants.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Franklin.Nguyen" date="2014.04.21">Created</change>
// </history>

namespace SaveBetsBusiness.Constants
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Bet type constant.
    /// </summary>
    public static class BetTypeConstants
    {
        public const int Handicap = 1;
        public const int _1stHandicap = 7;

        public const int OddEven = 2;
        public const int _1stOddEven = 12;

        public const int OverUnder = 3;
        public const int _1stOverUnder = 8;

        public const int MoneyLine = 20;

        public const int _1x2 = 5;
        public const int _1st1x2 = 15;

        public const int CorrectScore = 4;
        public const int _1stCorrectScore = 30;
        public const int TotalGoal = 6;
        public const int _1stTotalGoal = 126;
        public const int FirstLastGoal = 14;
        public const int _1stFirstLastGoal = 127;
        public const int HaftFullTime = 16;
        public const int DrawNoBet = 25;
        public const int ToWinToNil = 27;
        public const int _3wayHDP = 28;

        public const int CleanSheet = 13;
        public const int DoubleChange = 24;
        public const int BothOneNeitherTeamToScore = 26;

        public const string FirstHalfFirstGoalLastGoal = "1FL";
        public const string FirstGoalLastGoal = "FL";

        public const int MyanmarHandicap = 301;
        public const int _1stMyanmarHandicap = 303;

        public const int MyanmarOverUnder = 302;
        public const int _1stMyanmarOverUnder = 304;
    }
}
