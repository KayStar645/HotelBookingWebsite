using Core.Application.ViewModels.Common;
using Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Core.Application.ViewModels.Customers
{
    public class CustomerRQ : BaseRQ
    {
        [Display(Name = "Tên khách hàng")]
        [Required(ErrorMessage = "Tên khách hàng là trường bắt buộc.")]
        [StringLength(100, ErrorMessage = "Tên khách hàng không vượt quá 100 ký tự.")]
        public string? Name { get; set; }

        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Số điện thoại là trường bắt buộc.")]
        [Phone(ErrorMessage = "Số điện thoại cần phải đúng định dạng.")]
        public string? Phone { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email là trường bắt buộc.")]
        [EmailAddress(ErrorMessage = "Email cần phải đúng định dạng.")]
        public string? Email { get; set; }

        [Display(Name = "Địa chỉ khách hàng")]
        [StringLength(250, ErrorMessage = "Địa chỉ khách hàng không vượt quá 250 ký tự.")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Phân loại khách hàng là bắt buộc.")]
        [RegularExpression($"^({Customer.TYPE_LOYAL}|{Customer.TYPE_VISITOR})$",
            ErrorMessage = "Giá trị không hợp lệ cho phân loại khách hàng.")]
        public string? Type { get; set; }
    }
}
