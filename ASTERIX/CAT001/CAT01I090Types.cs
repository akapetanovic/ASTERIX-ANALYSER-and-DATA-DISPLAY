using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT01I090Types
    {

        public enum Code_Validation_Type { Code_Validated, Code_Not_Validated, Unknown };
        public enum Code_Garbled_Type { Code_Garbled, Code_Not_Garbled, Unknown };

        public class CAT01I090FlightLevelUserData
        {
            public Code_Validation_Type Code_Validated = Code_Validation_Type.Unknown;
            public Code_Garbled_Type Code_Garbled = Code_Garbled_Type.Unknown;
            public double FlightLevel = -1.0;
        }
    }
}
