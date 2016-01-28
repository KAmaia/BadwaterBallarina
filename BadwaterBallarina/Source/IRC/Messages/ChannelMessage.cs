
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BadwaterBallarina.Source.IRC;
using System.IO;

namespace BadwaterBallarina.Source.IRC.Messages {
	class ChannelMessage : AIrcMessage {

		private string channel;

		public string Channel { get { return channel; } }

		public ChannelMessage( StreamWriter outStream, string[ ] incomingMessage ) : base( outStream, incomingMessage ) {
			channel = incomingMessage[2];
		}

		public override void Respond( string response ) {
			outStream.WriteLine( FormatResponse(response) );
			outStream.Flush( );
		}

		protected override string FormatResponse( string response ) {
			return responseString + channel + " :" + response;

		}
	}
}
