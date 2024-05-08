using System;
using System.Collections.Generic;
using System.Drawing;
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
            string fileSizeText = GetFileSizeText(fileLength);
            // for now; debug dump the file size
            if (fileLength > SafeLengthThreshold)
            {
                // warn it!
                // if the guy has devmode, they will see this immediately
                PlayerLogWarningMain.LogError($"Log file size: {fileSizeText}; TOO LARGE!");
            }
            else
            {
                // no issue.
                PlayerLogWarningMain.LogInfo($"Log file size: {fileSizeText}; within safety limits.");
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

        public static string GetFileSizeText(long length)
        {
            // am lazy; ref https://stackoverflow.com/questions/281640/how-do-i-get-a-human-readable-file-size-in-bytes-abbreviation-using-net
            if (length < 0)
            {
                return "(invalid)";
            }
            string[] sizeUnits = { "B", "KB", "MB", "GB", "TB" };
            int order = 0;
            double loopingLength = length;
            while (loopingLength >= 1024 && order < sizeUnits.Length - 1)
            {
                order++;
                loopingLength /= 1024;
            }
            return string.Format("{0:0.##} {1}", loopingLength, sizeUnits[order]);
        }
    }
}
