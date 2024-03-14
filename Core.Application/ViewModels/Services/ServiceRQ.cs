using Core.Application.ViewModels.Common;
using Core.Application.ViewModels.Common.ValidationAttributes;
using Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Core.Application.ViewModels.Services
{
    public class ServiceRQ : BaseRQ
	{
        [Required(ErrorMessage = "Mã dịch vụ là trường bắt buộc.")]
        [InternalCode<Service, ServiceRQ>(ErrorMessage = "Mã dịch vụ đã tồn tại.")]
        public string? InternalCode { get; set; }

        [Display(Name = "Tên dịch vụ")]
        [Required(ErrorMessage = "Tên dịch vụ là trường bắt buộc.")]
        [StringLength(100, ErrorMessage = "Tên dịch vụ không vượt quá 100 ký tự.")]
        public string? Name { get; set; }

        [Display(Name = "Mô tả dịch vụ")]
        public string? Describe { get; set; }

        [Display(Name = "Giá dịch vụ")]
        [Required(ErrorMessage = "Giá là trường bắt buộc.")]
        [Range(0, int.MaxValue, ErrorMessage = "Giá tối thiểu phải bằng 0.")]
        public double? Price { get; set; }       
    }
}
