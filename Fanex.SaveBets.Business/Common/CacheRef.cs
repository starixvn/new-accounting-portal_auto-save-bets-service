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
    using RemotingRef;
    using SaveBetsBusiness.Constants;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class CacheRef
    {
        public static string GetBettype(object betTypeId)
        {
            return RefData.GetBetTypeName(betTypeId.ToString(), LanguageTypeConstants.En);
        }
    }
}