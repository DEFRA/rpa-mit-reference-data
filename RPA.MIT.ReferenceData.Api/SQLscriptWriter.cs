using System.Text;

namespace RPA.MIT.ReferenceData.Api
{
    /// <summary>
    /// Managed wrapper for StreamWriter
    /// </summary>
    public class SQLscriptWriter: TextWriter
    {
        private StreamWriter? _streamWriter;
        private readonly string _fileName;

        /// <summary>
        /// Encoding : Not implemented
        /// </summary>
        public override Encoding Encoding => throw new NotImplementedException();

        /// <param name="fileName">Name of file to log SQL scripts to</param>
        public SQLscriptWriter(string fileName)
        {
            _fileName = fileName;
        }

        /// <summary>
        /// Open the file stream
        /// </summary>
        public void Open()
        {
            _streamWriter = new StreamWriter(_fileName, append:true);
            _streamWriter.AutoFlush = true;
        }

        /// <summary>
        /// Close the file stream
        /// </summary>
        public override void Close()
        {
            _streamWriter?.Close();
            _streamWriter?.Dispose();
        }

        /// <summary>
        /// Log a line to the SQL script file
        /// </summary>
        /// <param name="value">Line of text to log</param>
        public override void WriteLine(string? value)
        {
            _streamWriter?.WriteLine(value);
        }

        /// <summary>
        /// Dispose the file stream
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            _streamWriter?.Dispose();
            base.Dispose(disposing);
        }
    }
}
