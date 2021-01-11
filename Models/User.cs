using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JdMvc.Models
{
    public class User
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
        [NotMapped]
        [Required(ErrorMessage = "请再次输入密码")]
        public string CpassWord { get; set; }
    }
}