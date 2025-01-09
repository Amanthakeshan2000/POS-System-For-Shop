using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS
{
    public class DBConfig
    {
        public string MyConnection()
        {
            string con = "Server=MSI\\SQLEXPRESS;Database=pos;Integrated Security=True;";
            return con;
        }
    }
}
