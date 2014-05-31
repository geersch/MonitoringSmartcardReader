using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SmartcardLibrary
{    
    internal enum SmartcardState
    {
        None = 0,
        Inserted = 1,
        Ejected = 2
    }

    public class SmartcardManager : IDisposable 
    {
        #region Member Fields

        //Shared members are lazily initialized.
        //.NET guarantees thread safety for shared initialization.
        private static readonly SmartcardManager _instance = new SmartcardManager();

        private SmartcardContextSafeHandle _context;
        private SmartcardErrorCode _lastErrorCode;
        private bool _disposed = false;
        private ReaderState[] _states;                
        //A thread that watches for new smart cards.
        private BackgroundWorker _worker;                

        #endregion

        #region Methods

        //Make the constructor private to hide it. This class adheres to the singleton pattern.
        private SmartcardManager()
        {
            //Create a new SafeHandle to store the smartcard context.
            this._context = new SmartcardContextSafeHandle();
            //Establish a context with the PC/SC resource manager.
            this.EstablishContext();
            
            //Compose a list of the card readers which are connected to the
            //system and which will be monitored.
            ArrayList availableReaders = this.ListReaders();            
            this._states = new ReaderState[availableReaders.Count];
            for (int i = 0; i <= availableReaders.Count - 1; i++)
            {
                this._states[i].Reader = availableReaders[i].ToString();
            }

            //Start a background worker thread which monitors the specified
            //card readers.
            if ((availableReaders.Count > 0))
            {
                this._worker = new BackgroundWorker();
                this._worker.WorkerSupportsCancellation = true;
                this._worker.DoWork += WaitChangeStatus;
                this._worker.RunWorkerAsync();
            }
        }

        public static SmartcardManager GetManager() 
        {            
            return _instance;
        }

        private bool EstablishContext()
        {
            if ((this.HasContext))
            {
                return true;
            }
            this._lastErrorCode =
                (SmartcardErrorCode)UnsafeNativeMethods.EstablishContext(ScopeOption.System,
                IntPtr.Zero, IntPtr.Zero, ref this._context);
            return (this._lastErrorCode == SmartcardErrorCode.None);
        }        

        private void WaitChangeStatus(object sender, DoWorkEventArgs e)
        {
            while (!e.Cancel) 
            {
                SmartcardErrorCode result;
                
                //Obtain a lock when we use the context pointer, 
                //which may be modified in the Dispose() method.
                lock (this) 
                {
                    if (!this.HasContext) 
                    {
                        return;
                    }
                    
                    //This thread will be executed every 1000ms. 
                    //The thread also blocks for 1000ms, meaning 
                    //that the application may keep on running for 
                    //one extra second after the user has closed 
                    //the Main Form.
                    result = 
                        (SmartcardErrorCode)UnsafeNativeMethods.GetStatusChange(
                        this._context, 1000, this._states, this._states.Length);
                }
                
                if ((result == SmartcardErrorCode.Timeout)) 
                {
                    // Time out has passed, but there is no new info. Just go on with the loop
                    continue;
                }
                
                for (int i = 0; i <= this._states.Length - 1; i++) 
                {
                    //Check if the state changed from the last time.
                    if ((this._states[i].EventState & CardState.Changed) == CardState.Changed)
                    {
                        //Check what changed.
                        SmartcardState state = SmartcardState.None;
                        if ((this._states[i].EventState & CardState.Present) == CardState.Present 
                            && (this._states[i].CurrentState & CardState.Present) != CardState.Present)
                        {
                            //The card was inserted.                            
                            state = SmartcardState.Inserted;
                        }
                        else if ((this._states[i].EventState & CardState.Empty) == CardState.Empty 
                            && (this._states[i].CurrentState & CardState.Empty) != CardState.Empty)
                        {
                            //The card was ejected.
                            state = SmartcardState.Ejected;
                        }
                        if (state != SmartcardState.None && this._states[i].CurrentState != CardState.None)
                        {
                            switch(state)
                            {
                                case SmartcardState.Inserted:
                                {
                                    MessageBox.Show("Card inserted");
                                    break;
                                }
                                case SmartcardState.Ejected:
                                {
                                    MessageBox.Show("Card ejected");
                                    break;
                                }
                                default:
                                {
                                    MessageBox.Show("Some other state...");
                                    break;
                                }
                            }
                        }
                        //Update the current state for the next time they are checked.
                        this._states[i].CurrentState = this._states[i].EventState;
                    }
                }
            }
        }

        private int GetReaderListBufferSize()
        {
            if ((this._context.IsInvalid)) 
            {
                return 0;
            }
            int result = 0;
            this._lastErrorCode = 
                (SmartcardErrorCode)UnsafeNativeMethods.ListReaders(
                this._context, null, null, ref result);
            return result;
        }

        public ArrayList ListReaders()
        {
            ArrayList result = new ArrayList();
            
            //Make sure a context has been established before 
            //retrieving the list of smartcard readers.
            if (this.EstablishContext())
            {
                //Ask for the size of the buffer first.
                int size = this.GetReaderListBufferSize();

                //Allocate a string of the proper size in which 
                //to store the list of smartcard readers.
                string readerList = new string('\0', size);
                //Retrieve the list of smartcard readers.
                this._lastErrorCode =
                    (SmartcardErrorCode)UnsafeNativeMethods.ListReaders(this._context, 
                    null, readerList, ref size);
                if ((this._lastErrorCode == SmartcardErrorCode.None))
                {
                    //Extract each reader from the returned list.
                    //The readerList string will contain a multi-string of 
                    //the reader names, i.e. they are seperated by 0x00 
                    //characters.
                    string readerName = string.Empty;
                    for (int i = 0; i <= readerList.Length - 1; i++)
                    {
                        if ((readerList[i] == '\0'))
                        {
                            if ((readerName.Length > 0))
                            {
                                //We have a smartcard reader's name.
                                result.Add(readerName);
                                readerName = string.Empty;
                            }
                        }
                        else
                        {
                            //Append the found character.
                            readerName += new string(readerList[i], 1);
                        }
                    }
                }
            }
            return result;
        }        

        #endregion

        #region IDisposable Support

        //IDisposable
        private void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    // Free other state (managed objects).            
                }

                //Free your own state (unmanaged objects).
                //Set large fields to null.
                this._states = null;
                this._worker.CancelAsync();
                this._worker.Dispose();
                this._context.Dispose();
            }
            this._disposed = true;
        }        

        // Implement IDisposable.
        // Do not make this method virtual.
        // A derived class should not be able to override this method.
        public void Dispose()
        {
            Dispose(true);
            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Properties

        private bool HasContext
        {            
            get { return (!this._context.IsInvalid); }
        }        

        #endregion
    }    
}
