using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommandPreprocessor
{
    enum MachineState
    {
        SERIALPORTCONNECTED,	// 0
        HANDSHAKEACKRECEIVED,	// 1
        CNCMATICCONNECTED,		// 2
        CONFIGURED,				// 3
        WAITINGCOMMAND,			// 4
        PROCESSINGCOMMAND,		// 5
        LIMITSENSOR,			// 6
        EMERGENCYSTOP,			// 7
        FREEMOVES				// 8
    }
}
