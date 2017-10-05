// -----------------------------------------------------------------------
// <copyright file="NumberFormatExtension.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Franklin.Nguyen" date="2014.04.21">Created</change>
// </history>

namespace SaveBetsBusiness.Extensions
{
    /// <summary>
    /// Format number extension.
    /// </summary>
    public static class NumberFormatExtension
    {
        public static string DecimalNumberFormat(decimal value)
        {
            return value.ToString("#,##0.00");
        }
    }
}