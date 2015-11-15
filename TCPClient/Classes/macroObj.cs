using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPClient.Classes
{
    class MacroObj
    {
        private string _cmd;
        private string _pathExec;
        private string _paramObj;


        public string cmd {
            get{ return _cmd; }
            set{ _cmd = value; }
        }

        public string pathExec{
            get{ return _pathExec; }
            set{ _pathExec = value; }
        }

        public string paramObj{
            get { return _paramObj; }
            set { _paramObj = value; }
        }

    }
}