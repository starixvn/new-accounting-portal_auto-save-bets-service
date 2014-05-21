// -----------------------------------------------------------------------
// <copyright file="Mailing.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace SaveBetsBusiness.Service.Implement
{
    using System;
    using System.IO;
    using RazorEngine;
    using SaveBetsBusiness.Common;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class Mailing
    {
        public const string MatchEmptyDataNotificationTemplate = @"/Templates/NotificationEmailTemplate.cshtml";

        /// <summary>
        /// The empty match data email notification.
        /// </summary>
        /// <param name="matchId">The match identifier.</param>
        public static void MatchEmptyDataEmailNotification(int matchId)
        {
            try
            {
                var root = AppDomain.CurrentDomain.BaseDirectory;
                string filePath = root + MatchEmptyDataNotificationTemplate;
                string template = File.ReadAllText(filePath);
                var emailContent = Razor.Parse(template, matchId);
                string emailTo = AppSettings.AdminEmail;
                string emailSubject = string.Format(AppSettings.MailSubject, matchId);
                string emailTitle = AppSettings.MailTitle;

                var mailItem = new Fanex.MailDeliver.Model.MailRecord
                {
                    MailTo = emailTo,
                    Subject = emailSubject,
                    Title = emailTitle,
                    Body = emailContent
                };

                if (!string.IsNullOrEmpty(mailItem.MailTo.Trim()))
                {
                    using (var client = new Fanex.MailDeliver.MailClient.API.MailDeliver())
                    {
                        client.SendMail(mailItem);
                    }
                }
            }
            catch (Exception ex)
            {
                LogExtensions.ExceptionHandler(ex);
            }
        }
    }
}