using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadwaterBallarina.Source.IRC.Messages{
	class ServerMessage : AIrcMessage{
		public ServerMessage(StreamWriter outStream, string[] incoming): base(outStream, incoming ) {
			responseString = "";
		}
		public override void Respond( string response ) {
			throw new NotImplementedException( );
		}

		protected override string FormatResponse( string response ) {
			throw new NotImplementedException( );
		}
	}
}
