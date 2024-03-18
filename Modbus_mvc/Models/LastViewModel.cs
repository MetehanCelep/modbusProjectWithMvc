using Microsoft.AspNetCore.Mvc;

namespace Modbus_mvc.Models
{
    public class LastViewModel : Controller
    {
        public string[] RegistersList { get; set; }
    }
}
