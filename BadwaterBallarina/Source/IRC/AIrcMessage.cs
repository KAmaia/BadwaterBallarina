﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadwaterBallarina.source.IRC {
	abstract class AIrcMessage {

		protected string message;
		protected string sender;

		protected StreamWriter outStream;
		protected string[] incoming;

		protected string responseString = "PRIVMSG ";

		public string Sender { get { return sender; } }
		public string Message { get { return message; } }

		public AIrcMessage(StreamWriter outStream, string[ ] incoming ) {
			sender = incoming[0].Substring( 0, incoming[0].IndexOf( '!' ) );
			this.outStream = outStream;
			this.incoming = incoming;
			message = MakeThisReadable( );
		}

		protected string MakeThisReadable(  ) {
			
			return String.Join( " ", incoming, 3, incoming.Length - 3 ).Substring( 1 );
		}

	}
}