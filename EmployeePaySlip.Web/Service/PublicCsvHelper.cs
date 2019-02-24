using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using System.Globalization;

namespace EmployeePaySlip.Web.Service
{
    public static class PublicCsvHelper
    {
        public static byte[] EnumerableToCsvBytes<T>(IEnumerable<T> records)
        {
            using (var stream = new MemoryStream())
            using (var streamWriter = new StreamWriter(stream))
            using (var csvWriter = new CsvWriter(streamWriter))
            {
                csvWriter.WriteRecords(records);
                streamWriter.Flush();
                return stream.ToArray();
            }
        }
    }
}
