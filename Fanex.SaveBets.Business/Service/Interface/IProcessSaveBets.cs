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

        List<DataRow> Jobs { get; }

        DataRow Turnovers { get; }

        List<DataRow> WinLosses { get; }

        DataRow MatchInfo { get; }

        bool IsEndMatch { get; }

        List<BettypeInfo> Bettypes_TB { get; }

        List<BettypeInfo> Bettypes_Bets { get; }

        DataTable GetTotalBets(GetTotalBetCriteria criteria);

        DataTable GetBetTickets(GetBetTicketsCriteria criteria);

        void ExecuteEndMatchJob(ExecuteEndMatchJobCriteria criteria);
    }
}