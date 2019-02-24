using System;
using System.Collections.Generic;
using System.IO;

namespace EmployeePaySlip.Web.Service
{
    public interface IEmployeeCsvHelper
    {
        IEnumerable<T> ReadFromCsvFile<T>(Stream stream);
    }

}
