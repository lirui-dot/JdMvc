using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JdMvc.Models;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> Edit()
        {
            var userid = HttpContext.Session.GetInt32("UserId");
            InheritingPage page = new InheritingPage();
            var personal = _context.Personals.SingleOrDefault(m => m.UserId == userid);
            var user = _context.Users.Find(userid);

            if (user != null)
            {
                page.LoginName = user.LoginName;
                page.UserName = user.UserName;
                page.Name = user.Name;
            }

            if (personal != null)
            {
                page.Gender = personal.Gender;
                page.BirthdayYear = personal.BirthdayYear;
                page.BirthdayMonth = personal.BirthdayMonth;
                page.BirthdayDay = personal.BirthdayDay;
                page.HobbyClassification = personal.HobbyClassification;
                string hy = personal.HobbyClassification;
                if (hy != null)
                {
                    hy = hy.Substring(0, hy.Length - 1);
                    string[] Arrays = hy.Split(",");
                    int[] num = Array.ConvertAll(Arrays, int.Parse);
                    ViewBag.Hy = num;
                }
            }

            var birthday = DateTime.Now.Year;
            page.BirthdayYearList = new List<SelectListItem>();
            for (var i = birthday; i >= 1900; i--)
            {
                if (page.BirthdayYear == i)
                {
                    page.BirthdayYearList.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString(), Selected = true });
                }
                else
                {
                    page.BirthdayYearList.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString() });
                }

            }
            page.BirthdayMonthList = new List<SelectListItem>();
            for (int i = 1; i <= 12; i++)
            {
                if (page.BirthdayMonth == i)
                {
                    page.BirthdayMonthList.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString(), Selected = true });
                }
                else
                {
                    page.BirthdayMonthList.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString() });
                }
            }
            page.BirthdayDayList = new List<SelectListItem>();
            for (int i = 1; i <= 31; i++)
            {
                if (page.BirthdayDay == i)
                {
                    page.BirthdayDayList.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString(), Selected = true });
                }
                else
                {
                    page.BirthdayDayList.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString() });
                }
            }

            var hobby = _context.Hobbies.ToList();
            ViewData["Hobby"] = hobby;

            return View(page);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(InheritingPage page)
        {
            var userid = HttpContext.Session.GetInt32("UserId");
            var personal = _context.Personals.SingleOrDefault(m => m.UserId == userid);

            var user = _context.Users.Find(userid);
            var hobby = _context.Hobbies.ToList();
            ViewData["Hobby"] = hobby;

            if (user != null)
            {
                user.UserName = page.UserName;
                user.Name = page.Name;
                _context.Update(user);
                await _context.SaveChangesAsync();
            }
            if (personal == null)
            {
                Personal ps = new Personal();
                ps.UserId = user.Id;
                ps.Gender = page.Gender;
                ps.BirthdayYear = page.BirthdayYear;
                ps.BirthdayMonth = page.BirthdayMonth;
                ps.BirthdayDay = page.BirthdayDay;
                ps.HobbyClassification = page.HobbyClassification;
                _context.Personals.Add(ps);
                await _context.SaveChangesAsync();
            }
            else
            {
                string hy = personal.HobbyClassification;
                if (hy != null)
                {
                    hy = hy.Substring(0, hy.Length - 1);
                    string[] Arrays = hy.Split(",");
                    int[] num = Array.ConvertAll(Arrays, int.Parse);
                    ViewBag.Hy = num;
                }
                personal.Gender = page.Gender;
                personal.BirthdayYear = page.BirthdayYear;
                personal.BirthdayMonth = page.BirthdayMonth;
                personal.BirthdayDay = page.BirthdayDay;
                personal.HobbyClassification = page.HobbyClassification;
                _context.Update(personal);
                await _context.SaveChangesAsync();
            }


            if (personal != null)
            {
                return RedirectToAction("Edit", "Personal");
            }

            return View(page);
        }


        public async Task<ActionResult> Pictures()
        {
            var userid = HttpContext.Session.GetInt32("UserId");
            var ig = _context.Images.Find(1);
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

            var personal = _context.Personals.SingleOrDefault(m => m.UserId == userid);
            string fileUrl1 = Path.GetFileName(personal.Image);
            var path1 = Directory.GetCurrentDirectory();
            if (personal.Image != "" && personal.Image != null)
            {
                FileStream fs = new FileStream(path1 + @"\wwwroot\Image\" + fileUrl1, FileMode.Open, FileAccess.Read);
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, (int)fs.Length);

                personal.FileUrl = "data:image/png;base64," + Convert.ToBase64String(buffer);
            }
            InheritingPage inheritingPage = new InheritingPage();
            inheritingPage.FileUrl = personal.FileUrl;
            return View(inheritingPage);
        }
        [HttpPost]
        public async Task<ActionResult> Pictures(InheritingPage page)
        {

            var imgage = _context.Images.ToList();
            ViewData["Image"] = imgage;

            var userid = HttpContext.Session.GetInt32("UserId");
            var ps = _context.Personals.Single(m => m.UserId == userid);
            if (page.ImageUrl != null)
            {
                string phones = page.ImageUrl.Replace("data:image/png;base64,", "");
                byte[] bytes = Convert.FromBase64String(phones);
                var path1 = Directory.GetCurrentDirectory();
                string fileUrl1 = Guid.NewGuid().ToString() + ".png";
                string url = path1 + @"\wwwroot\Image\" + fileUrl1;
                System.IO.File.WriteAllBytes(url, bytes);
                string urlPath = url.Replace(path1, "");
                page.Image = urlPath;
                ps.Image = page.Image;
            }
            if (page.FileUrl != null)
            {
                string phones = page.FileUrl.Replace("data:image/png;base64,", "");
                byte[] bytes = Convert.FromBase64String(phones);
                var path1 = Directory.GetCurrentDirectory();
                string fileUrl1 = Guid.NewGuid().ToString() + ".png";
                string url = path1 + @"\wwwroot\Image\" + fileUrl1;
                System.IO.File.WriteAllBytes(url, bytes);
                string urlPath = url.Replace(path1, "");
                page.Image = urlPath;
                ps.Image = page.Image;
            }

            if (ps != null)
            {
                _context.Update(ps);
                await _context.SaveChangesAsync();
                return RedirectToAction("Pictures", "Personal");
            }

            return View(page);
        }
        public async Task<ActionResult> Informations()
        {
            var userid = HttpContext.Session.GetInt32("UserId");
            var ps = _context.Personals.SingleOrDefault(m => m.UserId == userid);
            InheritingPage inheriting = new InheritingPage();
            inheriting.Marriage = ps.Marriage;
            inheriting.Income = ps.Income;
            inheriting.IdCard = ps.IdCard;
            inheriting.Education = ps.Education;
            inheriting.Industry = ps.Industry;
            return View(inheriting);
        }
        [HttpPost]
        public async Task<ActionResult> Informations(InheritingPage page)
        {
            var userid = HttpContext.Session.GetInt32("UserId");
            var ps = _context.Personals.Single(m => m.UserId == userid);
            ps.Marriage = page.Marriage;
            ps.Income = page.Income;
            ps.IdCard = page.IdCard;
            ps.Education = page.Education;
            ps.Industry = page.Industry;
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
