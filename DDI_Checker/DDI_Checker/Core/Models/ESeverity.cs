using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.Marshalling;
using System.Text;

namespace DDI_Checker.Core.Models
{
    public enum ESeverity
    {
       Unknown = 0,
       Negligible = 1,
       NonInterruptive = 3,
       HighPriority = 5,
    }
}
