using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadwaterBallarina.Source.IRC.Messages {
	class PrivateMessage : AIrcMessage {

		public PrivateMessage( StreamWriter outStream, string[ ] incoming ) : base( outStream, incoming ) {
			responseString = responseString + sender + " :";
		}

		public override void Respond( string response ) {

			responseString += response;
			outStream.WriteLine( responseString );
			outStream.Flush( );
		}
	}
}