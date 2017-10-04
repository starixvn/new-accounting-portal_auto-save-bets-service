// -----------------------------------------------------------------------
// <copyright file="GetBetTicketsCriteria.cs" company="Nexcel Solutions Vietnam">
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
    public class GetBetTicketsCriteria : CriteriaBase
    {
        public int MatchId { get; set; }

        public int Bettype { get; set; }

        public bool LiveIndicator { get; set; }

        public override string GetSettingKey()
        {
            return "NAP_SaveBet_GetBets";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}