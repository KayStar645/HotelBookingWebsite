using Core.Application.ViewModels.Common;
using System.ComponentModel.DataAnnotations;

namespace Core.Application.ViewModels.Services
{
    public class ServiceRQ : BaseRQ
	{
        [Required(ErrorMessage = "Mã dịch vụ là trường bắt buộc.")]
        public string? InternalCode { get; set; }

        [Display(Name = "Tên dịch vụ")]
        [Required(ErrorMessage = "Tên dịch vụ là trường bắt buộc.")]
        [StringLength(100, ErrorMessage = "Tên dịch vụ không vượt quá 100 ký tự.")]
        public string? Name { get; set; }

        [Display(Name = "Mô tả dịch vụ")]
        [StringLength(300, ErrorMessage = "Mô tả không vượt quá 300 ký tự.")]
        public string? Describe { get; set; }

        [Display(Name = "Giá dịch vụ")]
        [Required(ErrorMessage = "Giá là trường bắt buộc.")]
        [Range(0, int.MaxValue, ErrorMessage = "Giá tối thiểu phải bằng 0.")]
        public double? Price { get; set; }       
    }
}
