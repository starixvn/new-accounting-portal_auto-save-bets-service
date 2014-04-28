// -----------------------------------------------------------------------
// <copyright file="ISaveBetsBusinessRepository.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace SaveBetsBusiness.Repositories.Interface
{
    using System.Data;
    using SaveBetsBusiness.Criteria;
    using SaveBetsBusiness.Enums;
    using SaveBetsBusiness.Model;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface ISaveBetsBusinessRepository
    {
        DataTable GetSaveGetJobs();

        DataTable GetTurnover(int MatchId);

        DataTable GetWinLoss(int MatchId);

        DataTable GetMatchInfo(int MatchId);

        DataTable GetTotalBet(GetTotalBetCriteria criteria);

        DataTable GetBetTikets(GetBetTicketsCriteria criteria);
    }
}