using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace PlayerLogWarning
{
    public class LogFileChecker
    {
        // we only check once per app launch, to avoid spamming
        private static bool hasChecked = false;

        /// <summary>
        /// Equivalent to 100 MB.
        /// </summary>
        public static readonly long SafeLengthThreshold = 100 * 1048576;

        public static void CheckOnce()
        {
            FileInfo theFile = GetLogFileInfo();
            if (theFile == null)
            {
                // first run; nothing to show
                return;
            }
            // has file; see if we have checked this
            if (hasChecked)
            {
                // we checked this; don't repeat the check.
                return;
            }
            long fileLength = theFile.Length;
            // for now; debug dump the file size
            if (fileLength > SafeLengthThreshold)
            {
                // warn it!
                // if the guy has devmode, they will see this immediately
                PlayerLogWarningMain.LogError("Log file size: " + theFile.Length + "; TOO LARGE!");
            }
            else
            {
                // no issue.
                PlayerLogWarningMain.LogInfo("Log file size: " + theFile.Length + "; within safety limits.");
            }
            hasChecked = true;
            return;
        }

        private static FileInfo GetLogFileInfo()
        {
            /*
             * we look at Player-prev.log
             * the reason is that when RimWorld is running, Player.log is always empty since this is the current log being actively written to.
             * RimWorld will rotate the previous log to Player-prev.log; if that file is extra large, then something must have went wrong.
             */
            string logFilePath = Path.Combine(GenFilePaths.SaveDataFolderPath, "Player-prev.log");
            if (!File.Exists(logFilePath))
            {
                return null;
            }
            return new FileInfo(logFilePath);
        }
    }
}
