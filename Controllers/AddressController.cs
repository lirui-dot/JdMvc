using JdMvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace JdMvc.Controllers
{
    public class AddressController : Controller
    {
        private readonly UserContext _context;

        public AddressController(UserContext context)
        {
            _context = context;
        }

        



    }
}