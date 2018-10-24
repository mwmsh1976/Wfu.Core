
using System;
using System.Collections.Generic;

namespace Wfu.Ma.Reports.Lydall.Classes
{
	public class Batch
	{
		public string EmployeeName { get; set; }
		public string EmployeeId { get; set; }
        public double EmployeePayRate { get; set; }
        public double GrossPay { get; set; }
		public string Title { get; set; }
		public DateTime PayPeriodStart { get; set; }
		public DateTime PayPeriodEnd { get; set; }
		public List<BatchItem> TimeDetail = new List<BatchItem>();
        public double TotalHours { get; set; }
	}
}
