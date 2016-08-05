using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fanex.SaveBets.Builder.Mapper;

namespace Fanex.SaveBets.Builder.Header
{
    public class HeaderBuilderImp : HeaderBuilder
    {
        public List<string> GetHeaderByBettype(int bettype)
        {
            List<string> result = new List<string>();
            BettypeMapper bettypeMapper = new BettypeMapperImp();
            int numOfColumns = bettypeMapper.GetNumOfColumnOf(bettype);
            if (numOfColumns == 0)
            {
                numOfColumns = 1;
            }

            string typeName = string.Format("Fanex.SaveBets.Builder.Header.Header{0}ColumnImp", numOfColumns);
            Type type = Type.GetType(typeName);
            if (type == null)
            {
                type = Type.GetType("Fanex.SaveBets.Builder.Header.Header1ColumnImp");
            }

            Header header = Activator.CreateInstance(type) as Header;

            result = header.BuildHeader();

            return result;
        }
    }
}
