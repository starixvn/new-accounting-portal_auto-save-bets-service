using System;
using System.Configuration;
using Fanex.RemotingRef;

namespace AutoSaveBetsService
{
    public static class RemotingRefRegister
    {
        public static void Register()
        {
            RefDataTypeContainer
                .ConnectTo(new Uri(ConfigurationManager.AppSettings["RemoteRefURL"]))
                .FromApplication("Fanex.AutoSaveBets.Service");
        }
    }
}