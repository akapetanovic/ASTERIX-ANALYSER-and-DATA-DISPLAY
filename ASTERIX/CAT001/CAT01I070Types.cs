using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT01I070Types
    {
        public enum Code_Validation_Type { Code_Validated, Code_Not_Validated, Unknown };
        public enum Code_Garbled_Type { Code_Garbled, Code_Not_Garbled, Unknown };
        public enum Code_Smothed_Or_From_Transporder_Type { Code_Smoothed, Code_From_Transpodner, Unknown };

        public class CAT01070Mode3UserData
        {
            public Code_Validation_Type Code_Validated = Code_Validation_Type.Unknown;
            public Code_Garbled_Type Code_Garbled = Code_Garbled_Type.Unknown;
            public Code_Smothed_Or_From_Transporder_Type Code_Smothed_Or_From_Transponder = Code_Smothed_Or_From_Transporder_Type.Unknown;
            public string Mode3A_Code = "Unknown";
        }
    }
}
