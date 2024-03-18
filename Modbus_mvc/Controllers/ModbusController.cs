using Microsoft.AspNetCore.Mvc;
using Modbus_mvc.Models;
using Modbus.Device;
using System.Net.Sockets;
using System;
using System.Collections.Generic;

namespace Modbus_mvc.Controllers
{
    public class ModbusController : Controller
    {
        TcpClient client;
        ModbusIpMaster master;

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Last(ModbusDatas data)
        {
            string ipAddress = Convert.ToString(data.IpAdress);
            int port = data.Port;
            byte slaveId = 1;
            int second = data.Second; 

            try
            {
                client = new TcpClient();
                client.Connect(ipAddress, port);
                master = ModbusIpMaster.CreateIp(client);

                ushort startAddress = 0;
                ushort numRegisters = 10;
                ushort[] registers = master.ReadHoldingRegisters(slaveId, startAddress, numRegisters);

                List<string> list = new List<string>();

                for (int i = 0; i < numRegisters; i++)
                {
                    list.Add(registers[i].ToString());
                }

                
                ViewBag.RefreshInterval = second;

                return View(list);
            }
            catch (SocketException ex)
            {
                return Content($"Bağlantı hatası: {ex.Message}");
            }
        }

    }
}
