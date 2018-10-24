
using System;
using System.IO;
using System.Collections.Generic;

namespace Wfu.Core.Reports.Employee
{
    public class EmployeeReport
    {

        private StreamReader _reader;
        private List<Employee> _fileContents;

        public EmployeeReport(string path)
        {
            if (File.Exists(path))
            {
                _reader = new StreamReader(path);
                _fileContents = new List<Employee>();
            }
        }

        public List<Employee> Scan()
        {
            string line = NewLine();
            string[] array = line.Split(Convert.ToChar(","));
            string value = "";
            if (_reader != null)
            {
                do
                {
                    var employee = new Employee();
                    line = NewLine();
                    array = line.Split(Convert.ToChar(","));
                    employee.FirstName = array[0].Replace("\"", "");
                    employee.LastName = array[1].Replace("\"", "");
                    employee.DepartmentName = array[2].Replace("\"", "");
                    employee.DepartmentCode = array[3].Replace("\"", "");
                    employee.PayrollNumber = array[4].Replace("\"", "");
                    value = array[5].Replace("\"", "").Replace("\"", "");
                    if (IsNumeric(value))
                    {
                        employee.PayRate = Convert.ToDouble(value);
                    }                    
                    employee.Status = array[6].Replace("\"", ""); ;
                    _fileContents.Add(employee);
                } while (!_reader.EndOfStream);
            }
            return _fileContents;
        }

        private string NewLine(int count = 1)
        {
            var currentLine = "";
            for (int i = 0; i < count; i++)
            {
                currentLine = _reader.ReadLine();
            }
            return currentLine;
        }

        private bool IsNumeric(string value)
        {
            double number = 0;
            if (Double.TryParse(value, out number))
            {
                return true;
            }
            return false;
        }

    }
}
