﻿using JdMvc.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JdMvc.Controllers
{
    public class UserController : Controller
    {
        private readonly UserContext _context;

        public UserController(UserContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(User user)
        {

            var p = await _context.Provinces.FindAsync(1);
            if (p == null)
            {
                string url = "https://api.jisuapi.com/area/province?appkey=f48e75474d78a4d6";
                var handler = new HttpClientHandler()
                {
                    AutomaticDecompression = DecompressionMethods.GZip
                };
                using (var http = new HttpClient(handler))
                {
                    var response = await http.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    Console.WriteLine(await response.Content.ReadAsStringAsync());
                    string json = await response.Content.ReadAsStringAsync();
                    // string o=i.Replace("\"","\'");
                    // string p=o.Replace("'name':'','areacode':'010','zipcode':'100000','depth':1},{'id':2,'name':'安徽','parentid':0,'parentname':'','areacode':null,'zipcode':'','depth':1}";status':0,'msg':'ok','result':[{","");
                    // string a=p.Replace("]}","");
                    // string json = @"{'id':1,'name':'北京','parentid':0,'parent

                    // ProvinceDetails pr = JsonConvert.DeserializeObject<ProvinceDetails>(json);//反序列化

                    // CityDetails city = JsonConvert.DeserializeObject<CityDetails>(json);//反序列化
                    ProvinceDetails province = JsonConvert.DeserializeObject<ProvinceDetails>(json);//反序列化
                                                                                                    // Console.WriteLine(string.Format("反序列化： id={0},name={1},parentid={2},parentname={3},areacode={4},zipcode={5},depth={6}"
                                                                                                    // , descJsonStu.id, descJsonStu.name, descJsonStu.parentid, descJsonStu.parentname,descJsonStu.areacode,descJsonStu.zipcode,descJsonStu.depth));
                    string shuju = "";
                    string path = "D:/z.txt";
                    FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Write);
                    for (int i = 0; i < province.result.Count; i++)
                    {
                        string sql = "insert into [Provinces] (name,parentid)values('" + province.result[i].name + "'," + province.result[i].parentid + ");";
                        shuju = shuju + sql;
                    }
                    byte[] bytes = Encoding.UTF8.GetBytes(shuju);
                    fileStream.Write(bytes, 0, bytes.Length);
                    fileStream.Close();
                }
            }

            var c = await _context.Provinces.FindAsync(499);
            if (c == null)
            {
                string ooo = "";
                string path = "D:/qq.txt";
                FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Write);
                for (int w = 1; w < 35; w++)
                {
                    string url = "https://api.jisuapi.com/area/city?parentid=" + w + "&appkey=f48e75474d78a4d6";
                    var handler = new HttpClientHandler()
                    {
                        AutomaticDecompression = DecompressionMethods.GZip
                    };

                    using (var http = new HttpClient(handler))
                    {
                        var response = await http.GetAsync(url);
                        response.EnsureSuccessStatusCode();
                        Console.WriteLine(await response.Content.ReadAsStringAsync());
                        string json = await response.Content.ReadAsStringAsync();
                        ProvinceDetails province = JsonConvert.DeserializeObject<ProvinceDetails>(json);

                        string shuju = "";
                        for (int i = 0; i < province.result.Count; i++)
                        {
                            string sql = "insert into [Provinces] (name,parentid) select '" + province.result[i].name + "' , Id from[Provinces] where name='" + province.result[i].parentname + "';";
                            shuju = shuju + sql;
                        }
                        ooo = ooo + shuju;
                    }

                }
                byte[] bytes = Encoding.UTF8.GetBytes(ooo);
                fileStream.Write(bytes, 0, bytes.Length);
            }
            if (!ModelState.IsValid)
            {
                var dbuser = _context.Users.FirstOrDefault(m => m.LoginName.Equals(user.UserName) && m.PassWord.Equals(user.PassWord));
                HttpContext.Session.SetInt32("UserId", dbuser.Id);

                if (dbuser != null)
                {
                    return RedirectToAction("Edit", "Personal");
                }
                else
                    return View();
            }


            return View(user);
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Register register)
        {


            User user = new User();
            user.UserName = register.LoginName;
            user.LoginName = register.LoginName;
            user.Name = register.Name;
            user.PassWord = register.PassWord;
            user.CpassWord = register.CpassWord;

            if (user != null)
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }


        [AcceptVerbs("GET", "POST")]
        public IActionResult NameIsExists(string LoginName)
        {

            var yhm = _context.Users.SingleOrDefault(m => m.LoginName == LoginName);
            if (yhm != null)
            {
                return Json("用户名重复");
            }

            return Json(true);
        }

    }
}
