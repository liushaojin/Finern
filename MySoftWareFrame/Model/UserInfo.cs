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
    }
}
