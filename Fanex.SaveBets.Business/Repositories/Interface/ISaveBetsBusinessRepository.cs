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
        int Insert(TaskType type);

        void UpdateStatus(int id, Status status);

        DataTable GetLogNotEnoughDataToSendEmail();

        SaveBetsBusinessInfo GetSaveBetsBusinessData(GetSaveBetsBusinessCriteria criteria);

        bool HasTaskAlreadyRunToday();
    }
}