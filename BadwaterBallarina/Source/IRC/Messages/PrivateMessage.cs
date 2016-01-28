using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadwaterBallarina.Source.IRC.Messages {
	class PrivateMessage : AIrcMessage {

		public PrivateMessage( StreamWriter outStream, string[ ] incoming ) : base( outStream, incoming ) {
		}

		public override void Respond( string response ) {
			outStream.WriteLine( FormatResponse( response ) );
			outStream.Flush( );
		}

		protected override string FormatResponse( string response ) {
			return responseString + sender + " :" + response;
		}
	}
}