using System;

namespace NMLib
{
    public static partial class Utility
    {
        public static string GenerateID() {
            Guid guid = Guid.NewGuid();
            return guid.ToString();
        } 
    }
}