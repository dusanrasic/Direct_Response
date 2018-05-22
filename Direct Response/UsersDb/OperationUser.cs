using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Direct_Response.UsersDb
{
    public class UserDb  : DbItem
    {
        #region Property's
        private int user_Id;
        private string username;
        private string password;
        private string full_Name;
        private string email;
        private string image;

        public int User_Id
        {
            get { return this.user_Id; }
            set { this.user_Id = value; }
        }
        public string Username
        {
            get { return this.username; }
            set { this.username = value; }
        }
        public string Password
        {
            get { return this.password; }
            set { this.password = value; }
        }
        public string Full_Name
        {
            get { return this.full_Name; }
            set { this.full_Name = value; }
        }
        public string Email
        {
            get { return this.email; }
            set { this.email = value; }
        }
        public string Image
        {
            get { return this.image; }
            set { this.image = value; }
        }
        #endregion
    }
    public class CriteriaUser : CriteriaForSelection
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public abstract class OpUserBase : Operation
    {
        public CriteriaUser Criteria { get; set; }
        public override OperationResult execute(Direct_Response_UsersDbEntities entities)
        {
            IEnumerable<UserDb> ieUsers;
            if ((this.Criteria == null) || (this.Criteria.Username == null) &&
                (this.Criteria.Password == null))
                ieUsers = from user in entities.Users
                          select new UserDb
                          {
                              User_Id = user.User_Id,
                              Username = user.Username,
                              Password = user.Password,
                              Email = user.Email,
                              Full_Name = user.Full_name,
                              Image = user.Image
                          };
            else
            {
                ieUsers = from user in entities.Users
                          where ((this.Criteria.Username == null) ? true : 
                                    this.Criteria.Username == user.Username) &&
                          ((this.Criteria.Password == null) ? true : 
                                    this.Criteria.Password == user.Password)
                          select new UserDb
                          {
                              User_Id = user.User_Id,
                              Username = user.Username,
                              Password = user.Password,
                              Email = user.Email,
                              Full_Name = user.Full_name,
                              Image = user.Image
                          };
            }
            UserDb[] array = ieUsers.ToArray();
            OperationResult obj = new OperationResult();
            obj.DbItems = array;
            obj.Status = true;
            return obj;
        }
    }
    public class OpUserSelect : OpUserBase { }
    public class OpUserInsert : OpUserBase
    {
        private UserDb user;
        public UserDb User
        {
            get { return user; }
            set { user = value; }
        }
        public override OperationResult execute(Direct_Response_UsersDbEntities entities)
        {
            if ((this.user != null) && !string.IsNullOrEmpty(this.user.Username) &&
                !string.IsNullOrEmpty(this.user.Password) &&
                !string.IsNullOrEmpty(this.user.Full_Name) &&
                !string.IsNullOrEmpty(this.user.Email) &&
                !string.IsNullOrEmpty(this.user.Image))
                entities.UserInsert(this.user.Username, this.user.Password, this.user.Email, this.user.Full_Name, this.user.Image);
            return base.execute(entities);
        }
    }
    public class OpUserUpdate : OpUserBase
    {
        private UserDb user;
        public UserDb User
        {
            get { return user; }
            set { user = value; }
        }
        public override OperationResult execute(Direct_Response_UsersDbEntities entities)
        {
            if ((this.user != null) && (this.user.User_Id) > 0 &&
                !string.IsNullOrEmpty(this.user.Username) &&
                !string.IsNullOrEmpty(this.user.Password) &&
                !string.IsNullOrEmpty(this.user.Full_Name) &&
                !string.IsNullOrEmpty(this.user.Email) &&
                !string.IsNullOrEmpty(this.user.Image))
                entities.UserUpdate(this.user.User_Id, this.user.Username, this.user.Password, this.user.Email, this.user.Full_Name, this.user.Image);
            return base.execute(entities);
        }

    }
}
