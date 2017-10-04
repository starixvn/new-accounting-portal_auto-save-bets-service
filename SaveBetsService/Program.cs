// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AutoSaveBetsService
{
    using System.ServiceProcess;

    /// <summary>
    /// Main program.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main()
        {
            ServiceBase[] servicesToRun;
            servicesToRun = new ServiceBase[]
            {
                new AutoSaveBetsService()
            };

            ServiceBase.Run(servicesToRun);
        }
    }
}