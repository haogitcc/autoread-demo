namespace ReceiveAutonomousReading
{
    public class ResponseReadDataEventArgs
    {
        #region Fields

        private byte[] _responseReadData = null;

        #endregion

        #region Properties

        /// <summary>
        /// Tag Read Data
        /// </summary>
        public byte[] ResponseReadData
        {
            get { return _responseReadData; }
        }

        #endregion

        #region Construction

        /// <summary>
        /// TagReadData EventArgs Constructor
        /// </summary>
        /// <param name="tagReadData">the tag read data</param>
        public ResponseReadDataEventArgs(byte[] responseReadData)
        {
            _responseReadData = responseReadData;
        }

        #endregion
    }
}