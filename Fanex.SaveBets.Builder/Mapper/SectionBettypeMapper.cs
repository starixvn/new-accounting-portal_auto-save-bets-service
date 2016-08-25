using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fanex.SaveBets.Builder.Mapper
{
    public interface SectionBettypeMapper
    {
        int GetNumberSectionOf(int bettype);

        bool IsMyanmarBettypeGroup(int bettype);
    }
}
