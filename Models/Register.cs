using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace JdMvc.Models
{
    public class Register
    {
        public int Id { get; set; }

        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Remote(action: "NameIsExists", controller: "User")]
        [Display(Name = "登录名")]
        [Required(ErrorMessage = "请输入{0}")]
        public string LoginName { get; set; }
        [Display(Name = "昵称")]
        [Required(ErrorMessage = "请输入{0}")]
        public string Name { get; set; }

        [Display(Name = "密码")]
        [Required(ErrorMessage = "请输入{0}")]
        public string PassWord { get; set; }

        [Required(ErrorMessage = "请再次输入密码")]
        [Display(Name = "确认密码")]
        [Compare("PassWord", ErrorMessage = "两次密码不一样")]
        public string CpassWord { get; set; }

    }
}
