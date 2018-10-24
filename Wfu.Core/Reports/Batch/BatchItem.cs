using System;

namespace Wfu.Ma.Reports.Lydall.Classes
{
	public class BatchItem
	{
		public string Day { get; set; }
		public DateTime Date { get; set; }
		public string Department { get; set; }
		public DateTime TimeIn { get; set; }
		public DateTime TimeOut { get; set; }
		public double RegularTimeTotal { get; set; } = 0;
		public double OverTimeOneTotal { get; set; } = 0;
		public double OverTimeTwoTotal { get; set; } = 0;
		public double VacationTimeTotal { get; set; } = 0;
		public double HolidayTimeTotal { get; set; } = 0;
		public double SickTimeTotal { get; set; } = 0;
		public double OtherTimeTotal { get; set; } = 0;
		public double TotalTime { get; set; } = 0;
	}
}
