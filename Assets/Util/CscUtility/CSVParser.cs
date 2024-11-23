using System;
using System.IO;
using System.Linq;

namespace Util.CscUtility
{
    public class CsvParser
    {
        public ContentIgnorance IgnoredContent = ContentIgnorance.None;
        public ReadSequence ReadMethod = ReadSequence.RowFirst;
        private string fileContent;

        public int skipRows, skipCols;

        public enum ContentIgnorance
        {
            None = 0,
            IgnoreFirstColumn = 1,
            IgnoreFirstRow = 2,
            IgnoreFirstColumnAndRow = 3
        }

        public enum ReadSequence
        {
            RowFirst,
            ColumnFirst,
        }

        private string[][] ParseStringToArray()
        {
            var lineDelimiter = Environment.NewLine;
            var lines = fileContent.Split(lineDelimiter).Skip(skipRows);
            return lines.Select(line => line.Split(',')).Skip(skipCols).ToArray();
        }
    }
}
