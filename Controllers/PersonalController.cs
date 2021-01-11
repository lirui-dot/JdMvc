using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JdMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace JdMvc.Controllers
{
    public class PersonalController : Controller
    {
        private readonly UserContext _context;

        public PersonalController(UserContext context)
        {
            _context = context;
        }

        // GET: Personal/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            var personal = _context.Personals.SingleOrDefault(m => m.UserId == id);
            var birthday = DateTime.Now.Year;

            personal.BirthdayYearList = new List<SelectListItem>();
            for (var i = birthday; i >= 1900; i--)
            {
                if (personal.BirthdayYear == i)
                {
                    personal.BirthdayYearList.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString(), Selected = true });
                }
                else
                {
                    personal.BirthdayYearList.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString() });
                }

            }
            personal.BirthdayMonthList = new List<SelectListItem>();
            for (int i = 1; i <= 12; i++)
            {
                if (personal.BirthdayMonth == i)
                {
                    personal.BirthdayMonthList.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString(), Selected = true });
                }
                else
                {
                    personal.BirthdayMonthList.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString() });
                }
            }
            personal.BirthdayDayList = new List<SelectListItem>();
            for (int i = 1; i <= 31; i++)
            {
                if (personal.BirthdayDay == i)
                {
                    personal.BirthdayDayList.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString(), Selected = true });
                }
                else
                {
                    personal.BirthdayDayList.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString() });
                }
            }
            var hobby = _context.Hobbies.ToList();
            ViewData["Hobby"] = hobby;
            string hy = personal.HobbyClassification;
            if (hy != null)
            {
                hy = hy.Substring(0, hy.Length - 1);
                string[] Arrays = hy.Split(",");
                int[] num = Array.ConvertAll(Arrays, int.Parse);
                ViewBag.Hy = num;
            }

            return View(personal);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Personal personal)
        {
            var ig = await _context.Images.FindAsync(id);
            if (ig == null)
            {
                for (int i = 1; i < 17; i++)
                {
                    Image imagesss = new Image();
                    var path = Directory.GetCurrentDirectory();
                    string url = path + @"\wwwroot\Image\" + i + ".jpg";
                    string urlPath = url.Replace(path, "");
                    imagesss.Img = urlPath;
                    _context.Images.Add(imagesss);
                    await _context.SaveChangesAsync();
                }
            }




            var hobby = _context.Hobbies.ToList();
            ViewData["Hobby"] = hobby;
            string hy = personal.HobbyClassification;
            var user = _context.Users.Find(id);
            if (user != null)
            {
                user.UserName = personal.UserName;
                user.Name = personal.Name;
                _context.Update(user);
                await _context.SaveChangesAsync();
            }
            if (hy != null)
            {
                hy = hy.Substring(0, hy.Length - 1);
                string[] Arrays = hy.Split(",");
                int[] num = Array.ConvertAll(Arrays, int.Parse);
                ViewBag.Hy = num;
            }
            var ps = _context.Personals.Single(m => m.UserId == id);
            ps.UserName = personal.UserName;
            ps.LoginName = personal.LoginName;
            ps.Name = personal.Name;
            ps.Gender = personal.Gender;
            ps.BirthdayYear = personal.BirthdayYear;
            ps.BirthdayMonth = personal.BirthdayMonth;
            ps.BirthdayDay = personal.BirthdayDay;
            ps.HobbyClassification = personal.HobbyClassification;
            if (personal != null)
            {
                _context.Update(ps);
                await _context.SaveChangesAsync();
                return RedirectToAction("Edit", "Personal");
            }
            return View();
        }


        public async Task<ActionResult> Pictures(int? id)
        {
            var iii = _context.Images.Find(1);
            if (iii.FileUrl == null)
            {
                var Imgage = _context.Images.ToList();
                foreach (var item in Imgage)
                {
                    string fileUrl = Path.GetFileName(item.Img);
                    string path = Directory.GetCurrentDirectory();
                    if (Imgage != null)
                    {
                        FileStream fileStream = new FileStream(path + @"\wwwroot\Image\" + fileUrl, FileMode.Open, FileAccess.Read);
                        byte[] buffer = new byte[fileStream.Length];
                        fileStream.Read(buffer, 0, (int)fileStream.Length);
                        item.FileUrl = "data:image/png;base64," + Convert.ToBase64String(buffer);
                        _context.Update(item);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            var imgage = _context.Images.ToList();
            ViewData["Image"] = imgage;

            var personal = _context.Personals.SingleOrDefault(m => m.UserId == id);
            string fileUrl1 = Path.GetFileName(personal.Image);
            var path1 = Directory.GetCurrentDirectory();
            if (personal.Image != "" )
            {
                FileStream fs = new FileStream(path1 + @"\wwwroot\Image\" + fileUrl1, FileMode.Open, FileAccess.Read);
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, (int)fs.Length);

                personal.FileUrl = "data:image/png;base64," + Convert.ToBase64String(buffer);
            }

            return View(personal);
        }
        [HttpPost]
        public async Task<ActionResult> Pictures(int id, Personal personal)
        {
            var iii = _context.Images.Find(1);
            if (iii.FileUrl == null)
            {
                var Imgage = _context.Images.ToList();
                foreach (var item in Imgage)
                {
                    string fileUrl = Path.GetFileName(item.Img);
                    string path = Directory.GetCurrentDirectory();
                    if (Imgage != null)
                    {
                        FileStream fileStream = new FileStream(path + @"\wwwroot\Image\" + fileUrl, FileMode.Open, FileAccess.Read);
                        byte[] buffer = new byte[fileStream.Length];
                        fileStream.Read(buffer, 0, (int)fileStream.Length);
                        item.FileUrl = "data:image/png;base64," + Convert.ToBase64String(buffer);
                        _context.Update(item);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            var imgage = _context.Images.ToList();
            ViewData["Image"] = imgage;

            var ps = _context.Personals.Single(m => m.UserId == id);
            if (personal.ImageUrl != null)
            {
                string phones = personal.ImageUrl.Replace("data:image/png;base64,", "");
                byte[] bytes = Convert.FromBase64String(phones);
                var path1 = Directory.GetCurrentDirectory();
                string fileUrl1 = Guid.NewGuid().ToString() + ".png";
                string url = path1 + @"\wwwroot\Image\" + fileUrl1;
                System.IO.File.WriteAllBytes(url, bytes);
                string urlPath = url.Replace(path1, "");
                personal.Image = urlPath;
                ps.Image = personal.Image;
            }
            if (personal.FileUrl != null)
            {
                string phones = personal.FileUrl.Replace("data:image/png;base64,", "");
                byte[] bytes = Convert.FromBase64String(phones);
                var path1 = Directory.GetCurrentDirectory();
                string fileUrl1 = Guid.NewGuid().ToString() + ".png";
                string url = path1 + @"\wwwroot\Image\" + fileUrl1;
                System.IO.File.WriteAllBytes(url, bytes);
                string urlPath = url.Replace(path1, "");
                personal.Image = urlPath;
                ps.Image = personal.Image;
            }


            if (ps != null)
            {
                _context.Update(ps);
                await _context.SaveChangesAsync();
                return RedirectToAction("Pictures", "Personal");
            }

            return View(ps);
        }
        public async Task<ActionResult> Informations(int? id)
        {
            var ps = _context.Personals.Single(m => m.UserId == id);
            return View(ps);
        }
        [HttpPost]
        public async Task<ActionResult> Informations(int id, Personal personal)
        {
            var ps = _context.Personals.Single(m => m.UserId == id);
            ps.Marriage = personal.Marriage;
            ps.Income = personal.Income;
            ps.IdCard = personal.IdCard;
            ps.Education = personal.Education;
            ps.Industry = personal.Industry;
            if (ps != null)
            {
                _context.Update(ps);
                await _context.SaveChangesAsync();
                return RedirectToAction("Informations", "Personal");
            }
            return View();
        }

    }
}
