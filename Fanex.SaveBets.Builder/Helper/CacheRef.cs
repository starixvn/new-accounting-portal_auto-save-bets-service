using RemotingRef;

namespace Fanex.SaveBets.Builder.Helper
{
    public class CacheRef
    {
        public static string GetTeamName(string teamId)
        {
            return RefData.GetTeamName(teamId, "en-US");
        }
    }
}