using System.IO;
using System;

namespace ProjectBase.Utils.FileImport
{
    /// <summary>
    /// Summary description for CSVParser.
    /// </summary>
    public class CsvReader : IDisposable
    {
        private StreamReader reader;
        private string headerLine;
        private int columnsQuantity;
        private char separator = ',';
        
        /// <summary>
        ///	Creates the CSV Parser object.
        /// </summary>
        /// <param name="filePath">CSV file to read.</param>
        /// <param name="columnsQuantity">Quantity of columns to retrieve</param>
        /// <param name="includesHeaderLine">True if the header should be read.</param>
        /// <param name="separator">Set the current separator to use.</param>
        /// <exception cref="FileEmptyException">Indicates the file is empty.</exception>
        /// <exception cref="UnexpectedColumnsQuantityException">Indicates the number of columns found is not valid given the defined.</exception>
        public CsvReader(string filePath, int columnsQuantity, bool includesHeaderLine, char separator)
        {
            this.separator = separator;
            Initialize(filePath, columnsQuantity, includesHeaderLine);
        }

        /// <summary>
        ///	Creates the CSV Parser object.
        /// </summary>
        /// <param name="filePath">CSV file to read.</param>
        /// <param name="columnsQuantity">Quantity of columns to retrieve</param>
        /// <param name="includesHeaderLine">True if the header should be read.</param>
        /// <exception cref="FileEmptyException">Indicates the file is empty.</exception>
        /// <exception cref="UnexpectedColumnsQuantityException">Indicates the number of columns found is not valid given the defined.</exception>
        public CsvReader(string filePath, int columnsQuantity, bool includesHeaderLine)
        {
            Initialize(filePath, columnsQuantity, includesHeaderLine);
        }

        /// <summary>
        ///	Creates the CSV Parser object.
        /// </summary>
        /// <param name="filePath">CSV file to read.</param>
        /// <remarks>
        ///	Columns are calculated automatically from the header.
        ///	Only possible with csv with headers.
        /// </remarks>
        /// <exception cref="FileEmptyException">Indicates the file is empty.</exception>
        /// <exception cref="UnexpectedColumnsQuantityException">Indicates the number of columns found is not valid given the defined.</exception>
        public CsvReader(string filePath)
        {
            Initialize(filePath, null, true);
        }

        private void Initialize(string filePath, int? columnsQuantity, bool includesHeaderLine)
        {
            // Create a reader for the File
            this.reader = new StreamReader(filePath, System.Text.Encoding.Default);

            if (AtEndOfFile)
                throw new FileEmptyException("Verify the input file is not empty.");

            // Read the header line if any
            if (includesHeaderLine)
                headerLine = reader.ReadLine();

            // Validate the amount of columns match the specified if any
            if (columnsQuantity.HasValue && includesHeaderLine)
            {
                if (this.GetColumnsCount(headerLine) != columnsQuantity.Value)
                    throw new UnexpectedColumnsQuantityException(string.Format("{0} expected columns against {1} found in header line.", columnsQuantity.Value, this.GetColumnsCount(headerLine)));
            }

            // Save the quantity for further use
            if (!columnsQuantity.HasValue)
                this.columnsQuantity = this.GetColumnsCount(headerLine);
            else
                this.columnsQuantity = columnsQuantity.Value;

        }

        public string[] GetHeader()
        {
            return headerLine.Split(separator);
        }

        private int GetColumnsCount(string line)
        {
            int commaCount = 0;
            bool blnOpen = false;

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == separator && !blnOpen)
                    commaCount++;
                else if (line[i] == '"')
                    blnOpen = !blnOpen;
            }

            return commaCount + 1;
        }

        /// <summary>
        ///		CSVParser destructor
        /// </summary>
        ~CsvReader()
        {
            reader.Close();
        }

        /// <summary>
        ///		Check if the file is at the end.
        ///		Otherwise we can keep reading the file.
        /// </summary>
        public bool AtEndOfFile
        {
            get
            {
                if (reader.Peek() > -1)
                    return false;
                else
                    return true;
            }
        }

        public string[] ReadLine()
        {
            string originalLine = string.Empty;
            return ReadLine(out originalLine);
        }

        /// <summary>
        ///	Read the next line in the CSV file and return it contents into an ArrayList.
        /// </summary>
        /// <returns>An ArrayList object containing each value per column retrieved.</returns>
        public string[] ReadLine(out string originalLine)
        {
            int lngLenght;
            int lngNextPos;
            int i;
            bool blnNullArray;
            string line = string.Empty;

            int lngPos = 0;
            int lngItemIndex = 0;
            string[] avarItems = new string[this.columnsQuantity];

            if (!this.AtEndOfFile)
            {
                line = GetLine();
                if (line.Trim() == string.Empty)
                    line = string.Empty;

                lngLenght = line.Length;

                bool firstEmpty = false;

                while (lngPos < lngLenght)
                {
                    if (lngPos == 0 && line[0].ToString() == separator.ToString())
                    {
                        avarItems[lngItemIndex] = "";
                        firstEmpty = true;
                        lngItemIndex = lngItemIndex + 1;
                        if (lngItemIndex > this.columnsQuantity - 1) break;
                    }

                    lngNextPos = FindSeparator(lngPos, line, separator.ToString());

                    // Find next separator...
                    if (lngNextPos == -1)
                        lngNextPos = lngLenght + 1;

                    // Add item to array, strip quotes if any
                    if (lngPos + 1 == lngNextPos)
                    {
                        avarItems[lngItemIndex] = "";
                    }
                    else if (lngPos == 0 && !firstEmpty)
                    {
                        avarItems[lngItemIndex] = StripQuotes(line.Substring(lngPos, lngNextPos));
                    }
                    else if (firstEmpty && lngPos <= 1)
                    {
                        avarItems[lngItemIndex] = StripQuotes(line.Substring(lngPos, lngNextPos - 1));
                        firstEmpty = false;
                    }
                    else if (lngNextPos > lngLenght)
                        avarItems[lngItemIndex] = StripQuotes(line.Substring(lngPos + 1));
                    else
                        avarItems[lngItemIndex] = StripQuotes(line.Substring(lngPos + 1, lngNextPos - lngPos - 1));

                    if (avarItems[lngItemIndex] == null)
                        avarItems[lngItemIndex] = string.Empty;

                    // Set Position Of Next Separator
                    lngItemIndex = lngItemIndex + 1;
                    lngPos = lngNextPos;
                    if (lngItemIndex > this.columnsQuantity - 1) break;
                }

                // If all the items of the array are null or nullstring, then return NULL to be handled correctly
                blnNullArray = true;
                for (lngItemIndex = 0; lngItemIndex <= this.columnsQuantity - 1; lngItemIndex++)
                {
                    if (avarItems[lngItemIndex] != null)
                    {
                        blnNullArray = false;
                        break;
                    }
                }

                if (blnNullArray) avarItems = null;
            }
            else
                avarItems = null;

            originalLine = line;

            return avarItems;
        }


        /// <summary>
        ///	Finds the position of the next separator.
        /// </summary>
        /// <param name="Start">Position to start searching</param>
        /// <param name="Value">String to search in</param>
        /// <param name="Separator">Separator to search for</param>
        /// <returns>Position of the next separator</returns>
        private int FindSeparator(int Start, string Value, string Separator)
        {
            int i;

            bool blnOpenQuote = false;
            int lngBreakPosition = -1;
            int lngLenght = Value.Length;

            if (Start == 0 && Value.StartsWith(Separator))
                lngBreakPosition = 1;
            else
            {
                for (i = Start + 1; i <= lngLenght - 1; i++)
                {
                    if (Value.Substring(i, 1) == "\"")
                        blnOpenQuote = !blnOpenQuote;

                    if (Value.Substring(i, 1) == Separator && !blnOpenQuote)
                    {
                        lngBreakPosition = i;
                        break;
                    }
                }
            }

            return lngBreakPosition;
        }


        /// <summary>
        ///	Strip Quotes from the string
        /// </summary>
        /// <param name="Value">Value to strip quotes from</param>
        /// <returns>Clean string without quotes</returns>
        private string StripQuotes(string Value)
        {
            string varStripped;

            if (Value == "")
                varStripped = null;
            else if (Value.StartsWith("\""))
                varStripped = Value.Substring(1, Value.Length - 2);
            else
                varStripped = Value;

            return varStripped;
        }


        /// <summary>
        ///	Read a complete line from a CSV files.
        /// </summary>
        /// <returns>Complete Line</returns>
        private string GetLine()
        {
            bool blnFirstTime = true;
            bool blnOpenQuote = true;
            int i;
            string strLine = "";

            do
            {
                if (blnFirstTime)
                    strLine = reader.ReadLine();
                else
                    strLine += " " + reader.ReadLine();

                blnOpenQuote = false;

                for (i = 0; i <= strLine.Length - 1; i++)
                {
                    if (strLine.Substring(i, 1) == "\"")
                        blnOpenQuote = !blnOpenQuote;
                }

                blnFirstTime = false;
            }
            while (blnOpenQuote);

            return strLine;
        }

        #region IDisposable Members

        public void Dispose()
        {
            reader.Close();
        }

        #endregion
    }

    public class FileEmptyException : Exception
    {
        public FileEmptyException(string message) : base(message) { }
    }

    public class UnexpectedColumnsQuantityException : Exception
    {
        public UnexpectedColumnsQuantityException(string message) : base(message) { }
    }
}
