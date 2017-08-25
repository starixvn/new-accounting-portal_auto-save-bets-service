// --------------------------------------------------------------------------
// <copyright file="CacheRef.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="" date="2012.12.17">Created</change>
// </history>

namespace SaveBetsBusiness.Common
{
    using System;
    using System.Collections.Generic;
    using Fanex.Common;
    using RemotingRef;
    using SaveBetsBusiness.Constants;
    using SaveBetsBusiness.Resources;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class CacheRef
    {
        public static string GetBettype(object bettypeId)
        {
            return GetBetType(bettypeId, null);
        }

        /// <summary>
        /// Gets the type of the bet.
        /// </summary>
        /// <param name="betTypeId">The bet type identifier.</param>
        /// <param name="betId">The bet identifier.</param>
        /// <returns>The type of the bet.</returns>
        public static string GetBetType(object betTypeId, object betId)
        {
            // resourceId remove first character
            string resourceId = string.Empty;
            if (betId != null && betId.ToString().Length > 1)
            {
                resourceId = betId.ToString().Substring(1);
            }

            return RefData.GetBetTypeName(
                betTypeId.ToString(),
                GetSimpleLanguage(LanguageTypeConstants.EnUS),
                string.Empty,
                resourceId);
        }

        /// <summary>
        /// Gets the simple language.
        /// </summary>
        /// <param name="lang">The language.</param>
        /// <returns>The simple language.</returns>
        private static string GetSimpleLanguage(string lang)
        {
            switch (lang)
            {
                case LanguageTypeConstants.EnUS:
                    return LanguageTypeConstants.En;
                case LanguageTypeConstants.ZhTW:
                    return LanguageTypeConstants.Ch; // Traditional
                case LanguageTypeConstants.ZhCn:
                    return LanguageTypeConstants.Cs; // Simplified
                case LanguageTypeConstants.JaJp:
                    return LanguageTypeConstants.Jp;
                case LanguageTypeConstants.ThTh:
                    return LanguageTypeConstants.Th; // Thai
                default:
                    return LanguageTypeConstants.En;
            }
        }
    }
}