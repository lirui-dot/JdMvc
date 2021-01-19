using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JdMvc.Models
{
    public class InheritingPage
    {
        public int Id { get; set; }
        [Display(Name = "用户名")]
        [Required(ErrorMessage = "请输入{0}")]
        public string UserName { get; set; }
        [Display(Name = "登录名")]
        public string LoginName { get; set; }
        [Display(Name = "昵称")]
        public string Name { get; set; }
        [Display(Name = "密码")]
        [Required(ErrorMessage = "请输入{0}")]
        public string PassWord { get; set; }

        [Required(ErrorMessage = "请再次输入密码")]
        public string CpassWord { get; set; }
        [Display(Name = "性别")]
        public string Gender { get; set; }

        [Display(Name = "年")]
        public int BirthdayYear { get; set; }

        [Display(Name = "月")]
        public int BirthdayMonth { get; set; }
        [Display(Name = "日")]
        public int BirthdayDay { get; set; }
        [NotMapped]
        public List<SelectListItem> BirthdayYearList { get; set; }
        [NotMapped]
        public List<SelectListItem> BirthdayMonthList { get; set; }
        [NotMapped]
        public List<SelectListItem> BirthdayDayList { get; set; }

        [Display(Name = "兴趣爱好")]
        public string HobbyClassification { get; set; }

        [Display(Name = "图片")]
        public string Image { get; set; }
        [NotMapped]
        public string FileUrl { get; set; }
        [NotMapped]
        public string ImageUrl { get; set; }

        [Display(Name = "婚姻状况")]
        public string Marriage { get; set; }

        [Display(Name = "月收入")]
        public string Income { get; set; }
        [RegularExpression(@"^[1-9]\d{5}(18|19|([23]\d))\d{2}((0[1-9])|(10|11|12))(([0-2][1-9])|10|20|30|31)\d{3}[0-9Xx]$", ErrorMessage = "您输入的身份证格式有误，请重新输入！")]
        [StringLength(18, ErrorMessage = "您输入的身份证格式有误，请重新输入！")]
        [Display(Name = "身份证号码")]
        public string IdCard { get; set; }

        [Display(Name = "教育程度")]
        public string Education { get; set; }

        [Display(Name = "所在行业")]
        public string Industry { get; set; }
        [Required(ErrorMessage = "请输入收货人名称")]
        [Display(Name = "*收货人")]
        public string Consignee { get; set; }
        [Required(ErrorMessage = "所在地区不能为空")]
        [Display(Name = "*所在地区")]
        public string Area { get; set; }
        [Required(ErrorMessage = "地址不能为空")]
        [Display(Name = "*详细地址")]
        public string DetailedAddress { get; set; }
        [Required(ErrorMessage = "手机号码不能为空")]
        [Display(Name = "*手机")]
        [RegularExpression(@"^1[3458][0-9]{9}$", ErrorMessage = "手机号格式不正确")]
        public string Phone { get; set; }
        [RegularExpression(@"^1[3458][0-9]{9}$", ErrorMessage = "固定电话格式不正确")]
        [Display(Name = "固定电话")]
        public string FixedPhone { get; set; }
        [EmailAddress(ErrorMessage="电子邮箱格式不正确")]
        [Display(Name = "电子邮箱")]
        public string EmailAddress { get; set; }
        [Display(Name = "地址别名")]
        public string AddressAlias { get; set; }
    }
}