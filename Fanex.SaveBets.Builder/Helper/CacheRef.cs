using Fanex.RemotingRef;

namespace Fanex.SaveBets.Builder.Helper
{
    public class CacheRef
    {
        private static RemotingRefService rrservice = new RemotingRefService();

        public static string GetTeamName(string teamId)
        {
            return rrservice.GetTeamName(teamId, "en-US");
        }

        public static string GetBetTypeName(string bettypeId)
        {
            return rrservice.GetBetTypeName(bettypeId, "en-US");
        }

        public static string GetFastMarketRefid(string betType, int betTeam)
        {
            string str = "FastMarket" + betType;
            if (betTeam > 0)
            {
                str += "Yes";
            }

            if (betTeam == 0)
            {
                str += "No";
            }

            return str;
        }

        public static string GetFastMarketTimeRange(string betType, int hdp1)
        {
            string str1 = hdp1.ToString("00") + ":00";
            string str2;
            switch (betType)
            {
                case "222":
                case "224":
                case "227":
                    string str3 = (hdp1 + 4).ToString("00") + ":59";
                    str2 = string.Format("({0}-{1})", str1, str3);
                    break;

                default:
                    string str4 = hdp1.ToString("00") + ":59";
                    str2 = string.Format("({0}-{1})", str1, str4);
                    break;
            }

            return str2;
        }

        public static string GetResourceDataByRefIdAndResourceId(string refId, string resourceId, string languageCode, string defaultValue)
        {
            return rrservice.GetResourceDataByRefIdAndResourceId(refId, resourceId, languageCode, defaultValue);
        }

        public static string GetBetRadarMatchCodeFormat(string matchCode)
        {
            return rrservice.GetBetRadarMatchCodeFormat(matchCode, "en-US");
        }
    }
}