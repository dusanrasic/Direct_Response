using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Direct_Response.UsersDb
{
    public abstract class Operation
    {
        public abstract OperationResult execute(Direct_Response_UsersDbEntities entities);
    }
    public class OperationResult 
    {
        private string message;
        private DbItem[] dbItems;
        public bool Status { get; set; }

        public string Message
        {
            get { return message; }
            set { message = value; }
        }
        
        public DbItem[] DbItems
        {
            get { return dbItems; }
            set { dbItems = value; }
        }
    }
    public abstract class DbItem { }
    public abstract class CriteriaForSelection { }
}
