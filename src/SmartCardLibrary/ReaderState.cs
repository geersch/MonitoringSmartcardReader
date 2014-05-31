using System;
using System.Runtime.InteropServices;

namespace SmartcardLibrary
{
    //Wraps the SCARD_READERSTATE structure of PC/SC.
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    internal struct ReaderState
    {
        #region Member Fields
        //Points to the name of the reader being monitored.
        [MarshalAs(UnmanagedType.LPWStr)]
        private string _reader;
        //Not used by the smart card subsystem but is used by the application.
        private IntPtr _userData;
        //Current state of reader at time of call
        private CardState _currentState;
        //State of reader after state change
        private CardState _eventState;
        //Number of bytes in the returned ATR
        [MarshalAs(UnmanagedType.U4)]
        private int _attribute;
        //ATR of inserted card, with extra alignment bytes.
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 36)]
        private byte[] _rgbAtr;
        #endregion

        #region Methods
        public byte[] RGBAttribute()
        {
            return this._rgbAtr;
        }
        #endregion

        #region "Properties"
        public string Reader
        {
            get { return this._reader; }
            set { this._reader = value; }
        }

        public IntPtr UserData
        {
            get { return this._userData; }
            set { this._userData = value; }
        }

        public CardState CurrentState
        {
            get { return this._currentState; }
            set { this._currentState = value; }
        }

        public CardState EventState
        {
            get { return this._eventState; }
        }
        #endregion
    }
}