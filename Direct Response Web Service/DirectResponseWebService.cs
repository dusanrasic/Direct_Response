using Direct_Response_Web_Service.MessagesDb;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace Direct_Response_Web_Service
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    public class DirectResponseWebService : IDirectResponseWebService
    {
        System.Timers.Timer timer = new System.Timers.Timer(2000);
        public ConcurrentDictionary<int, ConnectedClient> _connectedClients = new ConcurrentDictionary<int, ConnectedClient>();
        private ConcurrentDictionary<int, BMessageInfo> messagesToSent = new ConcurrentDictionary<int, BMessageInfo>();
        public int Connected { get; set; }

        private void OnTimerEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            SendMessages();
        }

        public DirectResponseWebService()
        {
            if (!timer.Enabled)
                timer.Enabled = true;
            timer.Elapsed += OnTimerEvent;
        }

        public int Login(string userName, string fullName, int id)
        {
            //Checking if someone is already loged in
            //foreach(var client in _connectedClients)
            //{
            //    if (client.Key == id)
            //        return 1;
            //}
            if (_connectedClients.ContainsKey(id))
                return 1;

            var establishedUserConnection = OperationContext.Current.GetCallbackChannel<IClient>();
            ConnectedClient newClient = new ConnectedClient();
            newClient.connection = establishedUserConnection;
            newClient.UserName = userName;
            newClient.FullName = fullName;
            newClient.Id = id;

            //_connectedClients.TryAdd(userName, newClient);
            //_connectedClients.TryAdd(fullName, newClient);
            //_connectedClients.TryAdd(id.ToString(), newClient);
            _connectedClients.TryAdd(id, newClient);
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Client login: {0} at {1}", newClient.UserName, System.DateTime.Now);
            Console.ResetColor();

            OpMessageSelect oms = new OpMessageSelect();
            OperationResult obj = OperationManager.Singleton.executeOperation(oms);
            OpResCollection orc = obj as OpResCollection;
            if ((orc == null) || (!orc.Status))
            {
                Console.WriteLine("No new messages.{0}", newClient.UserName);
            }
            else
            {
                BMessageInfo[] messages = orc.BaseObjectArray.Cast<BMessageInfo>().ToArray();
                BMessageInfo[] connected = messages.Where(w => w.ToId == id).ToArray();
                foreach (var message in connected)
                {
                    //foreach (var client in _connectedClients)
                    //{
                    //    //if ((client.Key.ToLower() != newClient.FullName.ToLower()) && (client.Key.ToLower() == message.To.ToLower()))
                    //    if ((client.Key != newClient.Id) && (client.Key == message.ToId))
                    //    {
                    //        client.Value.connection.GetMessage(message.Message, message.From, message.FromId, message.FromImage, message.To, message.ToId);
                    //    }
                    //}
                    //bool postoji = _connectedClients.ContainsKey(message.ToId);
                    //if (postoji)
                    //{
                    //ConnectedClient conClient;
                    //bool dohvacen = _connectedClients.TryGetValue(message.ToId, out conClient);
                    //if (dohvacen)
                    //    conClient.connection.GetMessage(message.Message, message.From, message.FromId, message.FromImage, message.To, message.ToId);
                    //}

                    //newClient.connection.GetMessage(message.Message, message.From, message.FromId, message.FromImage, message.To, message.ToId);
                    //OpMessageDelete omd = new OpMessageDelete();
                    //omd.IdMessage = message.IdMessage;
                    //OperationResult result = OperationManager.Singleton.executeOperation(omd);
                    messagesToSent.TryAdd(message.IdMessage, message);

                }
            }
                

            return 0;
        }

        private void SendMessages()
        {
            foreach (var message in messagesToSent)
            {
                BMessageInfo Client = message.Value;
                bool exists = _connectedClients.ContainsKey(Client.ToId);
                if (exists)
                {
                    ConnectedClient conClient;
                    bool retrieved = _connectedClients.TryGetValue(Client.ToId, out conClient);
                    if (retrieved)
                    {
                        foreach (var item in messagesToSent)
                        {
                            BMessageInfo mes = item.Value;
                            conClient.connection.GetMessage(mes.Message, mes.From, mes.FromId, mes.FromImage, mes.To, mes.ToId);
                            OpMessageDelete omd = new OpMessageDelete();
                            omd.IdMessage = mes.IdMessage;
                            OperationResult result = OperationManager.Singleton.executeOperation(omd);
                            messagesToSent.TryRemove(mes.IdMessage, out mes);
                        }
                    }
                }
            }
        }

        public void Logout()
        {
            ConnectedClient client = GetMyClient();
            if(client != null)
            {
                ConnectedClient removedClient;
                _connectedClients.TryRemove(client.Id, out removedClient);

                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Client logof: {0} at {1}", removedClient.UserName, System.DateTime.Now);
                Console.ResetColor();
            }
        }

        public ConnectedClient GetMyClient()
        {
            var establishedUserConnection = OperationContext.Current.GetCallbackChannel<IClient>();
            foreach(var client in _connectedClients)
            {
                if(client.Value.connection == establishedUserConnection)
                {
                    return client.Value;
                }
            }
            return null;
        }

        public void SendMessage(string message, string from, int fromId, string fromImage, string to, int toId)
        {
            bool exists = _connectedClients.ContainsKey(toId);
            if (exists)
            {
                ConnectedClient conClient;
                bool retrieved = _connectedClients.TryGetValue(toId, out conClient);
                if(retrieved)
                    conClient.connection.GetMessage(message, from, fromId, fromImage, to, toId);
            }
            else
            {
                OpMessageInsert omi = new OpMessageInsert();
                omi.Message = new BMessageInfo { Message = message, From = from, FromId = fromId, FromImage = fromImage, To = to, ToId = toId };
                OperationResult obj = OperationManager.Singleton.executeOperation(omi);
            }
        }

        public string UploadImage(byte[] file, string filename)
        {
            try
            {
                string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Assets\\" + filename;
                MemoryStream memoryStream = new MemoryStream(file);
                FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite, FileShare.Delete);
                memoryStream.WriteTo(fs);
                memoryStream.Close();
                fs.Close();
                fs.Dispose();
                
                return path;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public void RemoveOldImage(string filename)
        {
            try
            {
                ConnectedClient client = GetMyClient();
                DirectoryInfo storage = new DirectoryInfo(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Assets\\");
                FileInfo[] filesInStorage = storage.GetFiles("*" + client.UserName + client.Id + "*.*");

                if (filesInStorage.Length != 0)
                {

                    foreach (FileInfo item in filesInStorage)
                    {
                        if(item.FullName != filename)
                        {
                            File.SetAttributes(item.FullName, FileAttributes.Normal);
                            File.Delete(item.FullName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                ex.Message.ToString();
            }
        }
    }
}
