
using System;
using System.IO;
using System.Collections.Generic;
using Wfu.Core.Enums.BatchReport;

namespace Wfu.Ma.Reports.Lydall.Classes
{
	public class BatchReport
	{
        private const string END_OF_RECORD = "DEPT";

        private StreamReader _reader;
        private List<Batch> _fileContents;

        public BatchReport(string path)
        {
            if (File.Exists(path))
            {
                _reader = new StreamReader(path);
                _fileContents = new List<Batch>();
            }
        }

        public List<Batch> Scan()
        {
            string line = NewLine();
            string[] array = line.Split(Convert.ToChar(","));
            if (_reader != null)
            {
                do
                {
                    var batch = ScanHeader();
                    line = NewLine(2);
                    do
                    {
                        line = NewLine();
                        if (array.Length >= 19)
                        {
                            var batchLine = new BatchItem();
                            array = line.Split(Convert.ToChar(","));
                            batchLine.Day = array[(int)Columns.DAY_COL];
                            if ((batchLine.Day.Trim() != "") && (batchLine.Day.Trim().ToUpper() != "DEPT"))
                            {
                                if (array[(int)Columns.DATE_COL].ToString() != ""
                                    && array[(int)Columns.DAY_COL].ToString().ToUpper() != END_OF_RECORD)
                                {
                                    batchLine.Date = Convert.ToDateTime(array[(int)Columns.DATE_COL]);
                                }
                                batchLine.Department = array[(int)Columns.DEPT_COL];
                                if ((array[(int)Columns.TIME_IN_COL] != "")
                                    && (array[(int)Columns.TIME_IN_COL] != "------"))
                                {
                                    batchLine.TimeIn = Convert.ToDateTime(array[(int)Columns.TIME_IN_COL]);
                                }
                                if ((array[(int)Columns.TIME_OUT_COL] != "")
                                    && (array[(int)Columns.TIME_OUT_COL] != "------"))
                                {
                                    batchLine.TimeOut = Convert.ToDateTime(array[(int)Columns.TIME_OUT_COL]);
                                }
                                batchLine.RegularTimeTotal = ConvertToDbl(array[(int)Columns.REG_PAY_COL]);
                                batchLine.OverTimeOneTotal = ConvertToDbl(array[(int)Columns.OT1_TIME_COL]);
                                batchLine.OverTimeTwoTotal = ConvertToDbl(array[(int)Columns.OT2_TIME_COL]);
                                batchLine.VacationTimeTotal = ConvertToDbl(array[(int)Columns.VAC_TIME_COL]);
                                batchLine.HolidayTimeTotal = ConvertToDbl(array[(int)Columns.HOL_TIME_COL]);
                                batchLine.SickTimeTotal = ConvertToDbl(array[(int)Columns.SICK_TIME_COL]);
                                batchLine.OtherTimeTotal = ConvertToDbl(array[(int)Columns.OTH_TIME_COL]);
                                batchLine.TotalTime = ConvertToDbl(array[(int)Columns.TOTAL_COL]);
                                batch.TimeDetail.Add(batchLine);
                            }
                            if (array[(int)Columns.TIME_OUT_COL].ToLower() == "total hours")
                            {
                                batch.TotalHours = Convert.ToDouble(array[(int)Columns.TOTAL_COL] + 0);
                            }
                            else if (array[(int)Columns.TIME_OUT_COL].ToLower() == "gross pay")
                            {
                                batch.GrossPay = Convert.ToDouble(array[(int)Columns.TOTAL_COL] + 0);
                            }
                        }
                    } while (array[(int)Columns.DAY_COL].ToString().ToUpper() != END_OF_RECORD);
                    _fileContents.Add(batch);
                    NewLine(2);
                } while (!_reader.EndOfStream);
            }
            return _fileContents;
        }

        private Batch ScanHeader()
        {
            var returnHeader = new Batch();
            var parseLine = NewLine();
            var array = parseLine.Split(Convert.ToChar(","));
            if (array.Length >= 2)
            {
                array = array[0].Split(Convert.ToChar("-"));
                if (array.Length >= 2)
                {
                    returnHeader.EmployeeName = array[0];
                    returnHeader.EmployeeId = array[1];
                }
            }
            parseLine = NewLine(2);
            array = parseLine.Split(Convert.ToChar(":"));
            if (array.Length >= 2)
            {
                array = array[1].Split(Convert.ToChar("-"));
                if (array.Length >= 2)
                {
                    returnHeader.PayPeriodStart = Convert.ToDateTime(array[0]);
                    returnHeader.PayPeriodEnd = Convert.ToDateTime(array[1]);
                }
            }
            return returnHeader;
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

        private double ConvertToDbl(string s)
        {
            double j;
            if (double.TryParse(s, out j))
                return j;
            else
                return 0;
        }
    }
}
