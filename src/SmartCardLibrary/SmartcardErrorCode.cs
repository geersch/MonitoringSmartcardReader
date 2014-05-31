using System;
using System.ComponentModel;

namespace SmartcardLibrary
{        
    internal enum SmartcardErrorCode : uint
    {
        [Description("Function succeeded")]
        None = 0,
        [Description("An internal consistency check failed.")]
        InternalError = 2148532225,        
        [Description("The action was canceled by a SCardCancel request.")]
        Canceled = 2148532226,        
        [Description("The supplied handle was invalid.")]
        InvalidHandle = 2148532227,        
        [Description("One or more of the supplied parameters could not be properly interpreted.")]
        InvalidParameter = 2148532228,        
        [Description("Registry startup information is missing or invalid.")]
        InvalidTarget = 2148532229,        
        [Description("Not enough memory available to complete this command.")]
        NoMemory = 2148532230,        
        [Description("An internal consistency timer has expired.")]
        WaitedTooLong = 2148532231,        
        [Description("The data buffer to receive returned data is too small for the returned data.")]
        InsufficientBuffer = 2148532232,        
        [Description("The specified reader name is not recognized.")]
        UnknownReader = 2148532233,        
        [Description("The user-specified timeout value has expired.")]
        Timeout = 2148532234,        
        [Description("The smart card cannot be accessed because of other connections outstanding.")]
        SharingViolation = 2148532235,        
        [Description("The operation requires a smart card, but not smard card is currently in the device.")]
        NoSmartcard = 2148532236,        
        [Description("The specified smart card name is not recognized.")]
        UnknownCard = 2148532237,        
        [Description("The system could not dispose of the media in the requested manner.")]
        CannotDispose = 2148532238,        
        [Description("The requested protocols are incompatible with the protocol currently in use with the smart card.")]
        ProtocolMismatch = 2148532239,        
        [Description("The reader or smart card is not ready to accept commands.")]
        NotReady = 2148532240,        
        [Description("One or more of the supplied parameters values could not be properly interpreted.")]
        InvalidValue = 2148532241,        
        [Description("The action was canceled by the system, presumably to log off or shut down.")]
        SystemCanceled = 2148532242,        
        [Description("An internal communications error has been detected.")]
        CommunicationError = 2148532243,        
        [Description("An internal error has been detected, but the source is unknown.")]
        UnknownError = 2148532244,        
        [Description("An ATR obtained from the registry is not a valid ATR string.")]
        InvalidAttribute = 2148532245,        
        [Description("An attempt was made to end a non-existent transaction.")]
        NotTransacted = 2148532246,        
        [Description("The specified reader is not currently available for use.")]
        ReaderUnavailable = 2148532248,        
        [Description("The operation has been aborted to allow the server application to exit.")]
        Shutdown = 2148532248,        
        [Description("The PCI Receive buffer was too small.")]
        PCITooSmall = 2148532249,        
        [Description("The reader driver does not meet minimal requirements for support.")]
        ReaderUnsupported = 2148532250,        
        [Description("The reader driver did not produce a unique reader name.")]
        DuplicateReader = 2148532251,        
        [Description("The smart card does not meet minimal requirements for support.")]
        CardUnsupported = 2148532252,        
        [Description("The Smart Card Resource Manager is not running.")]
        NoService = 2148532253,        
        [Description("The Smart Card Resource Manager has shut down.")]
        ServiceStopped = 2148532254,        
        [Description("An unexpected card error has occured.")]
        Unexpected = 2148532255,        
        [Description("No primary provider can be found for the smart card.")]
        ICCInstallation = 2148532256,        
        [Description("The requested order of object creation is not supported.")]
        ICCCreationOrder = 2148532257,        
        [Description("This smart card does not support the requested feature.")]
        UnsupportedFeature = 2148532258,        
        [Description("The identified directory does not exist in the smart card.")]
        DirectoryNotFound = 2148532259,        
        [Description("The identified file does not exist in the smart card.")]
        FileNotFound = 2148532260,        
        [Description("The supplied path does not represent a smart card directory.")]
        NoDirectory = 2148532261,        
        [Description("The supplied path does not represent a smart card file.")]
        NoFile = 2148532262,        
        [Description("Access is denied to this file.")]
        NoAccess = 2148532263,        
        [Description("The smart card does not have enough memory to store the information.")]
        WriteTooMany = 2148532264,        
        [Description("There was an error trying to set the smart card file object pointer.")]
        BadSeek = 2148532265,        
        [Description("The supplied PIN is incorrect.")]
        InvalidPin = 2148532266,        
        [Description("An unrecognized error code was returned from a layered component.")]
        UnknownResourceManagement = 2148532267,        
        [Description("The requested certificate does not exist.")]
        NoSuchCertificate = 2148532268,        
        [Description("The requested certificate could not be obtained.")]
        CertificateUnavailable = 2148532269,        
        [Description("Cannot find a smart card reader.")]
        NoReadersAvailable = 2148532270,        
        [Description("A communications error with the smart card has been detected. Retry the operation.")]
        CommunicationDataLast = 2148532271,        
        [Description("The requested key container does not exist on the smart card.")]
        NoKeyContainer = 2148532272,        
        [Description("The Smart Card Resource Manager is too busy to complete this operation.")]
        ServerTooBusy = 2148532273,        
        [Description("The reader cannot communiate with the card, due to ATR string configuration conflicts.")]
        UnsupportedCard = 2148532325,        
        [Description("The smart card is not responding to a reset.")]
        UnresponsiveCard = 2148532326,        
        [Description("Power has been removed from the smart card, so that further communication is not possible.")]
        UnpoweredCard = 2148532327,        
        [Description("The msart card has been reset, so any shared state information is invalid.")]
        ResetCard = 2148532328,        
        [Description("The smart card has been removed, so further communication is not possible.")]
        RemovedCard = 2148532329,        
        [Description("Access was denied because of a security violation.")]
        SecurityViolation = 2148532330,        
        [Description("The card cannot be accessed because th wrong PIN was presented.")]
        WrongPin = 2148532331,        
        [Description("The card cannot be accessed because the maximum number of PIN entry attempts has been reached.")]
        PinBlocked = 2148532332,        
        [Description("The end of the smart card file has been reached.")]
        EndOfFile = 2148532333,        
        [Description("The action was canceled by the user.")]
        CanceledByUser = 2148532334,        
        [Description("No PIN was presented to the smart card.")]
        CardNotAuthenticated = 2148532335        
    }    
}
