using Core.Application.ViewModels.Common;
using Core.Application.ViewModels.Common.ValidationAttributes;
using Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Core.Application.ViewModels.Tour
{
    public class TourRQ : BaseRQ
	{
        [Required(ErrorMessage = "Mã Tour là trường bắt buộc.")]
        [InternalCode<Service, TourRQ>(ErrorMessage = "Mã Tour đã tồn tại.")]
        public string? InternalCode { get; set; }

        [Display(Name = "Tên Tour")]
        [Required(ErrorMessage = "Tên Tour là trường bắt buộc.")]
        [StringLength(100, ErrorMessage = "Tên Tour không vượt quá 100 ký tự.")]
        public string? Name { get; set; }

        [Display(Name = "Tên Hướng dẫn tour")]
        [Required(ErrorMessage = "Tên Hướng dẫn tour là trường bắt buộc.")]
        [StringLength(100, ErrorMessage = "Tên Hướng dẫn tour không vượt quá 100 ký tự.")]
        public string? TourGuide { get; set; }

        [Display(Name = "Ngày bắt đầu")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateStart { get; set; }

        [Display(Name = "Mô tả Tour")]
        public string? Describe { get; set; }

        [Display(Name = "Giá Tour")]
        [Required(ErrorMessage = "Giá là trường bắt buộc.")]
        [Range(0, int.MaxValue, ErrorMessage = "Giá tối thiểu phải bằng 0.")]
        public double? Price { get; set; }       
    }
}
