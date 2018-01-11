using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StatTemplate
{
    class PLCDEvMitsubishi
    {
        public PLCDEvMitsubishi()
        { 
            
        }

        public bool read_io_status(ref Pin pPin, ref string err)
        {
            err = "";
            try
            {

                return true;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
            }
        }
    }

    class Pin
    {
        public bool AutoRun = false;
    }
}
