// -----------------------------------------------------------------------
// <copyright file="ISaveBetsBusinessRepository.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace SaveBetsBusiness.Repositories.Interface
{
    using System.Data;
    using SaveBetsBusiness.Criteria;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface ISaveBetsBusinessRepository
    {
        DataTable GetSaveGetJobs();

        DataTable GetTurnover(int matchId);

        DataTable GetWinLoss(int matchId);

        DataTable GetMatchInfo(int matchId);

        DataTable GetTotalBet(GetTotalBetCriteria criteria);

        DataTable GetBetTickets(GetBetTicketsCriteria criteria);

        void ExecuteEndMatchJob(ExecuteEndMatchJobCriteria criteria);
    }
}