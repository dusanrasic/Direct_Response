using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Direct_Response_Web_Service
{
    public class ConnectedClient
    {
        public IClient connection;
        public string UserName { get; set; }
        public string FullName { get; set; }
        public int Id { get; set; }
    }
}
