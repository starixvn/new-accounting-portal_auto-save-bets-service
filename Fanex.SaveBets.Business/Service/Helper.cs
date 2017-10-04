using System.Collections.Generic;
using Fanex.Common;
using RemotingRef;

namespace SaveBetsBusiness.Service
{
    public class Helper
    {
        private static Dictionary<int, string> ChoiceName()
        {
            Dictionary<int, string> choices = new Dictionary<int, string>();
            choices.Add(1, "Goal");
            choices.Add(2, "No Goal");
            choices.Add(3, "Throw In");
            choices.Add(4, "Free Kick");
            choices.Add(5, "Goal Kick");
            choices.Add(6, "Corner Kick");
            choices.Add(7, "Offside");
            choices.Add(8, "Booking");
            choices.Add(9, "Over");
            choices.Add(10, "Under");
            choices.Add(11, "0-19:59");
            choices.Add(12, "20:00-45+");
            choices.Add(13, "2nd Half");
            choices.Add(14, "Draw");
            choices.Add(15, "None");

            return choices;
        }

        private static List<string> SplitBetteam(string betteam)
        {
            ////Betchoice=2;BettypeTime=36:18-37:18;Time=36:09
            List<string> result = new List<string>();
            var items = betteam.Split(';');
            foreach (var item in items)
            {
                var subItems = item.Split('=');
                foreach (var subItem in subItems)
                {
                    result.Add(subItem);
                }
            }

            return result;
        }

        private static string GetChoice(List<string> splitBetteam)
        {
            string result = string.Empty;
            if (splitBetteam.Count > 1)
            {
                var choiceString = splitBetteam[1];
                if (!string.IsNullOrEmpty(choiceString))
                {
                    int choiceId = Converter.To<int>(choiceString);
                    var data = ChoiceName();
                    if (data.ContainsKey(choiceId))
                    {
                        result = data[choiceId];
                    }
                }
            }

            return result;
        }

        private static string GetChoice805(List<string> splitBetteam)
        {
            string result = string.Empty;
            if (splitBetteam.Count < 3)
            {
                return result;
            }

            string teamId = string.Empty;
            string possession = string.Empty;

            string bettypeTime = splitBetteam[3];
            if (string.IsNullOrEmpty(bettypeTime))
            {
                return result;
            }

            var arrBettype = bettypeTime.Split(',');
            if (arrBettype.Length != 2)
            {
                return result;
            }

            string betchoice = GetChoice(splitBetteam);
            teamId = arrBettype[0];
            possession = arrBettype[1];

            string teamName = RefData.GetBetTypeName(teamId, "en");
            result = string.Format("{0} {1} {2} %", betchoice, teamName, possession);

            return result;
        }

        public static string GetTeam(int bettype, string betteam)
        {
            string result = string.Empty;
            var splitBetteam = SplitBetteam(betteam);
            switch (bettype)
            {
                case 801:
                case 803:
                case 804:
                case 806:
                    result = GetChoice(splitBetteam);
                    break;

                case 805:
                    result = GetChoice805(splitBetteam);
                    break;

                default:
                    result = betteam;
                    break;
            }

            return result;
        }
    }
}