using System;

namespace SmartcardLibrary
{
    //CardState enumeration, used by the PC/SC function SCardGetStatusChanged.    
    internal enum CardState
    {
        //Unaware
        None = 0,
        Ignore = 1,
        Changed = 2,
        Unknown = 4,
        Unavailable = 8,
        Empty = 16,
        Present = 32,
        AttributeMatch = 64,
        Exclusive = 128,
        InUse = 256,
        Mute = 512,
        Unpowered = 1024
    }
}

