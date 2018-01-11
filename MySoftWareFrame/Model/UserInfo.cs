using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class UserInfo
    {
        private string username = null;
        private string password = null;
        private int permision = 0;
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
            }
        }
        
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }
        
        public int Permision
        {
            get
            {
                return permision;
            }
            set
            {
                permision = value;
            }
        }
    }
}
