using System.Collections.Generic;
using System.Data;

namespace Fanex.SaveBets.Builder.Content
{
    public interface ContentBetChoice
    {
        List<object> ContentBetChoiceBuilder(IDataReader reader);
    }
}