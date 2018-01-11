using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FNPlatform.Helper
{
    class DataManagementHelper
    {
        private SQLiteHelper sqlHelper = null;
        public DataManagementHelper(SQLiteHelper sqlhelper)
        {
            sqlHelper = sqlhelper;
        }


    }
}
