using Core.Domain.Common;
using Core.Domain.Common.Interfaces;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Entities
{
	public class Booking : AuditableEntity, IInternalCode
	{
		public string? InternalCode { get; set; }
		public string? CustomerName { get; set; }
		public string? CustomerEmail { get; set; }
		public DateTime? BookingDate { get; set; }
		public int? NumberOfPersons { get; set; }
		public double? TotalPrice { get; set; }

		// Khoá ngoại

		[ForeignKey("TourId")]
		public int TourID { get; set; }

		public Tour? Tour { get; set; }
	}
}
