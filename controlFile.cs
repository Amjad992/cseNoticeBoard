using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace cseNoticeBoard
{
    class controlFile
    {
        public static string currentText;

        // return number of line without the first line (only actual data line)
        public static void setTextAsCurrent()
        {
            // read the text in the control file
            currentText = System.IO.File.ReadAllText(variables.controlFilePath);
        }
        public static string getLine(int lineNumber)
        {
            try
            {
                string[] token = currentText.Split('\n');
                return token[lineNumber];
            }
            catch (IndexOutOfRangeException)
            {
                return "";
            }
        }
        public static string getItem(int lineNumber, int itemNumber)
        {
            try
            {
                string currentLine = getLine(lineNumber);
                string[] token = currentLine.Split('$');
                return token[itemNumber];
            }
            catch (IndexOutOfRangeException)
            {
                return "";
            }
        }
        public static string getFileName (int lineNumber)
        {
            return getItem(lineNumber, 0);
        }
        public static int getNumberOfLineInControlFile()
        {
            int numberOfLinesInControlFile;

            string[] token = currentText.Split('\n');
            // - 1 because first line is description of elements and not part of data
            numberOfLinesInControlFile = token.Length - 1;

            // - 1 last item with split will be an empty line
            return numberOfLinesInControlFile - 1;
        }
        public static string getDirectoryName(int lineNumber)
        {
            return getItem(lineNumber, 0);
        }


        public static string getFileDescriptionMainWindow (int lineNumber)
        {
            return getItem(lineNumber, 1);
        }
        public static string getSessionNumberTimeTablesWindow (int lineNumber)
        {
            return getItem(lineNumber, 1);
        }
        public static string getTeacherNameTeachersInfoWindow(int lineNumber)
        {
            return getItem(lineNumber, 1);
        }
        public static int getLineNumberTeacherWindow (string teacherName)
        {
            for (int i=1 ; i<= getNumberOfLineInControlFile(); ++i)
            {
                if (teacherName == getTeacherNameTeachersInfoWindow(i))
                {
                    return i;
                }
            }
            return 0;
        }
        public static string getTeacherPositionTeacherWindow(int lineNumber)
        {
            return getItem(lineNumber, 2);
        }
        public static string getTabTitle(int lineNumber)
        {
            return getItem(lineNumber, 1);
        }

        public static bool checkIfChairman (int lineNumber)
        {
            if (getItem(lineNumber, 3) == "yes")
                return true;
            else
                return false;
        }
    }
}
