using System;
using System.Collections.Generic;
using Fanex.SaveBets.Builder.Mapper;

namespace Fanex.SaveBets.Builder.Header
{
    public class HeaderBuilderImp : HeaderBuilder
    {
        public List<string> GetHeaderHandicapAndBetteamOf(int bettype)
        {
            List<string> result = new List<string>();
            HeaderBettypeMapper bettypeMapper = new HeaderBettypeMapperImp();
            string bettypeHeaderClass = bettypeMapper.GetHeaderHandicapAndBetTeamOf(bettype);
            if (!string.IsNullOrWhiteSpace(bettypeHeaderClass))
            {
                string typeName = string.Format("Fanex.SaveBets.Builder.Header.{0}", bettypeHeaderClass);
                Type type = Type.GetType(typeName);
                if (type == null)
                {
                    type = Type.GetType("Fanex.SaveBets.Builder.Header.Header3ColumnImp");
                }

                Header header = Activator.CreateInstance(type) as Header;
                result = header.BuildHeader();
            }

            return result;
        }
    }
}