using System;
using System.Collections.Generic;
using System.Data;
using Fanex.SaveBets.Builder.Mapper;

namespace Fanex.SaveBets.Builder.Content
{
    public class ContentBetChoiceBuilderImp : ContentBetChoiceBuilder
    {
        private readonly List<int> bettypesActivator = new List<int> { 221, 222, 223, 224, 225, 226, 227 };

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

                var isBettypeActivator = bettypesActivator.Contains(bettype);
                ContentBetChoice content = isBettypeActivator ? Activator.CreateInstance(type, bettype) as ContentBetChoice :
                                                Activator.CreateInstance(type) as ContentBetChoice;
                result = content.ContentBetChoiceBuilder(reader);
            }

            return result;
        }
    }
}