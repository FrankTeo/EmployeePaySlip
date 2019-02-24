using System;
using System.Collections.Generic;
using System.IO;
using CsvHelper;
using EmployeePaySlip.Web.Models;
using System.Linq;

namespace EmployeePaySlip.Web.Service
{
    public class EmployeeCsvHelper : IEmployeeCsvHelper
    {

        public IEnumerable<T> ReadFromCsvFile<T>(Stream stream)
        {
            using (var streamReader = new StreamReader(stream))
            using (var csvReader = new CsvReader(streamReader))
            {
                csvReader.Configuration.RegisterClassMap<EmployeeCsvMapping>();
                csvReader.Configuration.PrepareHeaderForMatch = (string header, int index) => header.Replace("_", string.Empty).ToLower();

                var records = csvReader.GetRecords<T>().ToArray();

                return records;
            }

        }

    }
}
