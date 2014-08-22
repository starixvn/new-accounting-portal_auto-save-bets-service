// -----------------------------------------------------------------------
// <copyright file="DirectoryExtensions.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Franklin.Nguyen" date="2014.04.21">Created</change>
// </history>

namespace SaveBetsBusiness.Common
{
    using System;
    using System.IO;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class DirectoryExtensions
    {
        private static readonly int _MaxNumberOfDirectories = 3;

        public static void CheckAndCreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        /// Deletes the directory.
        /// </summary>
        /// <param name="path">The path of directory.</param>
        public static void DeleteDirectory(string path)
        {
            var directories = Directory.GetDirectories(path);
            var directoryLength = directories.Length;
            if (directoryLength > _MaxNumberOfDirectories)
            {
                //// Delete old data. Remain 3 lasted month data.
                for (int i = 0; i < directoryLength - _MaxNumberOfDirectories; i++)
                {
                    var directory = directories[i];
                    Directory.Delete(directory);
                }
            }
        }

        /// <summary>
        /// Creates the save bet report data directory.
        /// </summary>
        /// <param name="eventDate">The event date.</param>
        /// <returns>Create save bet report data directory.</returns>
        public static string CreateSaveBetReportDataDirectory(DateTime eventDate)
        {
            string reportDataDirectory = AppSettings.ExportDataDirectory;

            CheckAndCreateDirectory(reportDataDirectory);

            string directoryDate = string.Format(
                "{0}{1}{2}",
                eventDate.ToString("yyyy"),
                eventDate.ToString("MM"),
                eventDate.ToString("dd"));
            string directoryPath = Path.Combine(reportDataDirectory, directoryDate);
            CheckAndCreateDirectory(directoryPath);

            return directoryPath;
        }
    }
}