using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadwaterBallarina.Source.IRC.Messages {
	class Ping {
		private StreamWriter outStream;
		private string pingHash = "PONG ";

		public Ping( StreamWriter outStream, string[ ] incoming ) {
			this.outStream = outStream;
			string tmp = string.Join( " ", incoming, 1, incoming.Length - 1 );
			pingHash += tmp;
		}

		public void Respond( ) {
			Console.Beep( );
			outStream.WriteLine( pingHash );
			outStream.Flush( );
		}
	}
}
