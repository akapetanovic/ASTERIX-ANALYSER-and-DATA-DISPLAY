using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsterixDisplayAnalyser
{
    class CAT65DecodeAndStore
    {
        // This method will accept a buffer of data with the assumption that 
        // category has been determined. It will then decode the data and save 
        // it in the shared buffer. Everry time a message is passed in the data 
        // will be appended to the buffer. This means that each line will contain 
        // data for one message. For data items which are not in the message,
        // indicated by the FSPEC field, N/A will be inserted instead. The shared 
        // buffer is loacted in the SharedData and will not be saved. It is responsibility
        // of the user to save the data in a file it desired.
        public static void Do(byte[] Data)
        {

            // I065/000, Message Type
            // I065/010, Data Source Identifier
            // I065/015, Service Identification
            // I065/020, Batch Number
            // I065/030, Time of Message
            // I065/040, SDPS Configuration and Status
            // I065/050, Service Status Report

          
        }

    }
}
