
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BadwaterBallarina.Source.IRC;
using System.IO;

namespace BadwaterBallarina.Source.IRC.Messages {
	class ChannelMessage : AIrcMessage, IIrcMessage {

		private string channel;

		public string Channel { get { return channel; } }

		public ChannelMessage( StreamWriter outStream, string[ ] incomingMessage ) : base( outStream, incomingMessage ) {
			channel = incomingMessage[2];
			responseString = responseString + channel + " :";
		}

		public void Respond( string response ) {
			responseString = responseString + response;
			outStream.WriteLine( responseString );
			outStream.Flush( );
		}
	}
}
