using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections;
using TCPClient.Classes;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace TCPClient
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class TCPClient
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args){
			TCPClient client = null;
            DateTime tick = DateTime.Now;
            var fileName = tick.ToString("yyyy-MM-dd---HH-mm-ss");
			client = new TCPClient(fileName + ".txt\r\n");
		}

		private String m_fileName=null;
		public TCPClient(String fileName){
			m_fileName=fileName;
			Thread t = new Thread(new ThreadStart(ClientThreadStart));
			t.Start();
		}

        private void ClientThreadStart(){
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 31001));

            var someMacroObj = new MacroObj();
            someMacroObj.cmd = "start";
            someMacroObj.pathExec = "C:\\Program Files(x86)\\Google\\Chrome\\Application\\chrome.exe\\";
            someMacroObj.paramObj = "http://www.kree8tive.dk";
            var json = new JavaScriptSerializer().Serialize(someMacroObj);
            
            // Send the file name.
            clientSocket.Send(Encoding.ASCII.GetBytes(m_fileName));

            // Receive the length of the filename.
            byte[] data = new byte[128];
            clientSocket.Receive(data);
            int length = BitConverter.ToInt32(data, 0);

            clientSocket.Send(Encoding.ASCII.GetBytes(json + "\r\n"));
            clientSocket.Send(Encoding.ASCII.GetBytes("[EOF]\r\n"));

            /* 
                What does this bit do ???
                Necessary ?
            */
			// Get the total length
			clientSocket.Receive(data);
			length=BitConverter.ToInt32(data,0);
            /* ? */

			clientSocket.Close();
		}

	}
}
