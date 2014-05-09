// -----------------------------------------------------------------------
// <copyright file="IProcessSaveBets.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace SaveBetsBusiness.Service.Interface
{
    using System.Collections.Generic;
    using System.Data;
    using SaveBetsBusiness.Criteria;
    using SaveBetsBusiness.Model;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IProcessSaveBets
    {
        int MatchId { get; }

        List<DataRow> Jobs  { get; }

        List<DataRow> Turnovers { get; }

        List<DataRow> WinLosses { get; }

        DataRow MatchInfo { get; }

        IDataReader GetTotalBets(GetTotalBetCriteria criteria);

        IDataReader GetBetTickets(GetBetTicketsCriteria criteria);

        List<BettypeInfo> Bettypes_TB { get; }

        List<BettypeInfo> Bettypes_Bets { get; }
    }
}
