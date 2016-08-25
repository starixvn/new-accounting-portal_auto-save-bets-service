using System;
using System.Collections.Generic;
using System.Data;
using Fanex.SaveBets.Builder.Mapper;

namespace Fanex.SaveBets.Builder.Content
{
    public class ContentBetChoiceBuilderImp : ContentBetChoiceBuilder
    {
        public List<object> GetContentBetchoiceToShowOf(int bettype, IDataReader reader)
        {
            List<object> result = new List<object>();
            ContentBettypeMapper bettypeMapper = new ContentBettypeMapperImp();
            string bettypeContentClass = bettypeMapper.GetContentBetchoiceToShowOf(bettype);
            if (!string.IsNullOrWhiteSpace(bettypeContentClass))
            {
                string typeName = string.Format("Fanex.SaveBets.Builder.Content.{0}", bettypeContentClass);
                Type type = Type.GetType(typeName);
                if (type == null)
                {
                    type = Type.GetType("Fanex.SaveBets.Builder.Content.ContentBetChoice405Imp");
                }

                ContentBetChoice content = Activator.CreateInstance(type) as ContentBetChoice;
                result = content.ContentBetChoiceBuilder(reader);
            }

            return result;
        }
    }
}