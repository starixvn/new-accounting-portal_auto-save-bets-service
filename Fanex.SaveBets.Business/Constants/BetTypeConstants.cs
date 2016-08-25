namespace SaveBetsBusiness.Constants
{
    using System.Collections.Generic;

    /// <summary>
    /// Bet type constant.
    /// </summary>
    public static class BetTypeConstants_
    {
        private const int Handicap = 1;
        private const int _1stHandicap = 7;
        
        private const int OddEven = 2;
        private const int _1stOddEven = 12;
        
        private const int OverUnder = 3;
        private const int _1stOverUnder = 8;
        
        private const int MoneyLine = 20;
        
        private const int _1x2 = 5;
        private const int _1st1x2 = 15;
        
        private const int CorrectScore = 4;
        private const int _1stCorrectScore = 30;
        private const int TotalGoal = 6;
        private const int _1stTotalGoal = 126;
        private const int FirstLastGoal = 14;
        private const int _1stFirstLastGoal = 127;
        private const int HaftFullTime = 16;
        private const int DrawNoBet = 25;
        private const int ToWinToNil = 27;
        private const int _3wayHDP = 28;
        
        private const int CleanSheet = 13;
        private const int DoubleChange = 24;
        private const int BothOneNeitherTeamToScore = 26;
        
        public const string FirstHalfFirstGoalLastGoal = "1FL";
        public const string FirstGoalLastGoal = "FL";
        
        private const int MyanmarHandicap = 301;
        private const int _1stMyanmarHandicap = 303;
        
        private const int MyanmarOverUnder = 302;
        private const int _1stMyanmarOverUnder = 304;
        
        private const int HomeTeamOverUnder = 401;
        private const int AwayTeamOverUnder = 402;
        private const int _1stHomeTeamOverUnder = 403;
        private const int _1stAwayTeamOverUnder = 404;
        private const int _2stCorrectScore = 405;
        
        private const int ChargedCommission = 500;
        private const int MatchWinner = 501;

        private const int _PHAIO_HomeTeamOverUnder = 401;
        private const int _PHAIO_AwayTeamOverUnder = 402;
        private const int _PHAIO_1HHomeTeamOverUnder = 403;
        private const int _PHAIO_1HAwayTeamOverUnder = 404;

        public static List<int> TwoSectionHeaderBettypesGroup()
        {
            List<int> bettypes = new List<int>
            {
                Handicap,
                _1stHandicap,
                OddEven,
                _1stOddEven,
                OverUnder,
                _1stOverUnder,
                MoneyLine,
                MyanmarHandicap,
                MyanmarOverUnder,
                _1stMyanmarHandicap,
                _1stMyanmarOverUnder,
                HomeTeamOverUnder,
                _1stHomeTeamOverUnder,
                AwayTeamOverUnder,
                _1stAwayTeamOverUnder,
                ChargedCommission,
                MatchWinner,

                _PHAIO_HomeTeamOverUnder,
                _PHAIO_AwayTeamOverUnder,
                _PHAIO_1HHomeTeamOverUnder,
                _PHAIO_1HAwayTeamOverUnder
            };

            return bettypes;
        }

        public static List<int> ThreeSectionHeaderBettypesGroup()
        {
            List<int> bettypes = new List<int>
            {
                _1x2,
                _1st1x2,
                430
            };

            return bettypes;
        }

        public static List<int> TwoSectionContentBettypesGroup()
        {
            List<int> bettypes = new List<int>
            {
                Handicap,
                _1stHandicap,
                OddEven,
                _1stOddEven,
                OverUnder,
                _1stOverUnder,
                MoneyLine,
                HomeTeamOverUnder,
                _1stHomeTeamOverUnder,
                AwayTeamOverUnder,
                _1stAwayTeamOverUnder,
                ChargedCommission,
                MatchWinner
            };

            return bettypes;
        }

        public static List<int> MyanmarOddsTwoSectionContentBettypesGroup()
        {
            List<int> bettypes = new List<int>
            {
                MyanmarHandicap,
                MyanmarOverUnder,
                _1stMyanmarHandicap,
                _1stMyanmarOverUnder
            };

            return bettypes;
        }

        public static List<int> ThreeSectionContentBettypesGroup()
        {
            List<int> bettypes = new List<int>
            {
                _1x2,
                _1st1x2
            };

            return bettypes;
        }

        public static List<int> NotShowTeamAndHdpColumnByBettypes()
        {
            List<int> bettypes = new List<int>
            {
                _1x2,
                _1st1x2
            };

            return bettypes;
        }

        public static List<int> ShowHdpColumnByBettypes()
        {
            List<int> bettypes = new List<int>
            {
                _1stHandicap,
                Handicap,
                _1stOddEven,
                OddEven,
                _1stOverUnder,
                OverUnder,
                MoneyLine,
                MyanmarHandicap,
                MyanmarOverUnder,
                _1stMyanmarHandicap,
                _1stMyanmarOverUnder,
                HomeTeamOverUnder,
                _1stHomeTeamOverUnder,
                AwayTeamOverUnder,
                _1stAwayTeamOverUnder,
                ChargedCommission,
                MatchWinner
            };

            return bettypes;
        }

        public static List<int> NotShowScoreColumnByBettypes()
        {
            List<int> bettypes = new List<int>
            {
                _1x2,
                _1st1x2
            };

            return bettypes;
        }

        public static List<int> MyanmarOddBettypes()
        {
            List<int> bettypes = new List<int>
            {
                MyanmarHandicap,
                MyanmarOverUnder,
                _1stMyanmarHandicap,
                _1stMyanmarOverUnder
            };

            return bettypes;
        }

        public static List<int> HandicapBettypes()
        {
            List<int> bettypes = new List<int>
            {
                Handicap,
                _1stHandicap
            };

            return bettypes;
        }
    }
}