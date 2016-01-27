using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadwaterBallarina.Source.IRC {
	class PrivateMessage : AIrcMessage, IIrcMessage {

		public PrivateMessage( StreamWriter outStream, string[ ] incoming ) : base( outStream, incoming ) {
			responseString = responseString + sender + " :";
		}

		public void Respond( string response ) {

			responseString += response;
			Console.WriteLine( responseString );
			outStream.WriteLine( responseString );
			outStream.Flush( );
		}
	}
}