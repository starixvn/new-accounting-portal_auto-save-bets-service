// -----------------------------------------------------------------------
// <copyright file="GetWinLossCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Franklin.Nguyen" date="2014.04.21">Created</change>
// </history>

namespace SaveBetsBusiness.Criteria
{
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Get win loss by products criteria.
    /// </summary>
    public class GetWinLossCriteria : CriteriaBase
    {
        public int MatchId { get; set; }

        public override string GetSettingKey()
        {
            return "NAP_SaveBet_GetWinLost";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}