using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Direct_Response.UsersDb
{
    public class OperationManager
    {
        #region Singleton
        private OperationManager() { }
        private static volatile OperationManager singleton;
        private static object syncRoot = new object();

        public static OperationManager Singleton
        {
            get
            {
                if (OperationManager.singleton == null)
                {
                    lock (OperationManager.syncRoot)
                    {
                        if (OperationManager.singleton == null)
                            OperationManager.singleton = new OperationManager();
                    }
                }
                return OperationManager.singleton;
            }
        }
        #endregion

        private Direct_Response_UsersDbEntities entities = new Direct_Response_UsersDbEntities();

        public OperationResult executeOperation(Operation op)
        {
            try
            {
                return op.execute(this.entities);
            }
            catch (Exception e)
            {
                OperationResult obj = new OperationResult();
                obj.Status = false;
                return obj;
            }
        }
    }
}
