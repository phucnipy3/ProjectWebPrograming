using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class OrderInfomation
    {
        [Required(ErrorMessage = "Tên không được để trống")]
        [Display(Name = "Họ và tên")]
        public string Name { get; set; }

        [Required(ErrorMessage = "SĐT không được để trống")]
        [Phone(ErrorMessage = "Vui lòng nhập đúng SĐT")]
        [MaxLength(10,ErrorMessage ="Số điện thoại phải có 10 chữ số")]
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Vui lòng nhập đúng email")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        public string Transport { get; set; }
    }
}