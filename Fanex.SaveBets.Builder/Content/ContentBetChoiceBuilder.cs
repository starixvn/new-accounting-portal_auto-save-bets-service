using System.Collections.Generic;
using System.Data;

namespace Fanex.SaveBets.Builder.Content
{
    public interface ContentBetChoiceBuilder
    {
        List<object> GetContentBetchoiceToShowOf(int bettype, DataRow reader);
    }
}