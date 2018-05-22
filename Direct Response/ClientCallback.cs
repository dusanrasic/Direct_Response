using Direct_Response.Model;
using Direct_Response.ViewModel;
using Direct_Response_Web_Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Direct_Response
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ClientCallback : IClient
    {
        public void GetMessage(string message, string from, int fromId, string fromImage, string to, int toId)
        {
            ((MainWindow)Application.Current.MainWindow).GetMessage(message, from, fromId, fromImage, to, toId);
        }
    }
}
