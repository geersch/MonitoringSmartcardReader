using System;
using System.Runtime.InteropServices;

namespace SmartcardLibrary
{
    internal enum ScopeOption
    {
        //User
        None = 0,
        Terminal = 1,
        System = 2
    }     

    internal sealed partial class UnsafeNativeMethods
    {
         #region WinScard.DLL Imports

         [DllImport("WINSCARD.DLL", EntryPoint = "SCardEstablishContext", CharSet = CharSet.Unicode, 
             SetLastError = true)]
         static internal extern uint EstablishContext(ScopeOption scope, IntPtr reserved1, 
             IntPtr reserved2, ref SmartcardContextSafeHandle context);
        
         [DllImport("WINSCARD.DLL", EntryPoint = "SCardReleaseContext", CharSet = CharSet.Unicode, 
             SetLastError = true)]
         static internal extern uint ReleaseContext(IntPtr context);
        
         [DllImport("WINSCARD.DLL", EntryPoint = "SCardListReaders", CharSet = CharSet.Unicode, 
             SetLastError = true)]
         static internal extern uint ListReaders(SmartcardContextSafeHandle context, string groups, 
             string readers, ref int size);
        
         [DllImport("WINSCARD.DLL", EntryPoint = "SCardGetStatusChange", CharSet = CharSet.Unicode, 
             SetLastError = true)]
         static internal extern uint GetStatusChange([In(), Out()] SmartcardContextSafeHandle context,
             [In(), Out()] int timeout, [In(), Out()] ReaderState[] states, [In(), Out()] int count);
        
         #endregion
    }
}