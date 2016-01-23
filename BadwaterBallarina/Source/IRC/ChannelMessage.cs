using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadwaterBallarina.source.IRC {
	class ChannelMessage {

		public string Sender { get; set; }
		public string Channel { get; set; }
		public string Message { get; set; }

		public ChannelMessage( string[ ] incomingMessage ) {
			makeThisReadable( incomingMessage );
		}

		private void makeThisReadable(string[] incoming ) {
			Sender = incoming[0].Substring( 0, incoming[0].IndexOf( '!' ) );
			Channel = incoming[2];

			Message = String.Join( " ", incoming, 3, incoming.Length - 3);
			Message = Message.Substring( 1 );
		}

	}
}
