using System.Collections.Generic;
using System.Data;
using Fanex.Common;
using Fanex.SaveBets.Builder.Helper;

namespace Fanex.SaveBets.Builder.Content
{
    public class ContentBetChoice221_222Imp : ContentBetChoice
    {
        private readonly string bettype;

        public ContentBetChoice221_222Imp(int bettype)
        {
            this.bettype = Converter.ToString2(bettype);
        }

        public List<object> ContentBetChoiceBuilder(DataRow reader)
        {
            var hdp1 = Converter.To<int>(reader["hdp1"]);
            var hdp2 = Converter.To<int>(reader["hdp2"]);
            var oddsspread = Converter.To<decimal>(reader["oddsspread"]);
            var betteam = Converter.To<string>(reader["betteam"]);

            var refId = CacheRef.GetFastMarketRefid(bettype, Converter.ToInt(betteam));
            var betchoice = CacheRef.GetResourceDataByRefIdAndResourceId(refId, Converter.ToString2(hdp2), "en", string.Empty);

            var interval = CacheRef.GetFastMarketTimeRange(bettype, hdp1);

            return new List<object> { betchoice, interval, oddsspread.ToString("G29") };
        }
    }
}