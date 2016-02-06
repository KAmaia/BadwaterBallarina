using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadwaterBallarina.Source.IRC.Messages {
	abstract class AIrcMessage {

		protected string message;
		protected string sender;

		protected StreamWriter outStream;
		protected string[] incoming;

		protected string responseString;

		public string[] RawMessageArr { get; private set; }

		public string Sender { get { return sender; } }
		public string Message { get { return message; } }

		public AIrcMessage( StreamWriter outStream, string[ ] incoming ) {
			sender = incoming[0].Substring( 0, incoming[0].IndexOf( '!' ) );
			this.outStream = outStream;
			this.incoming = incoming;
			responseString = "PRIVMSG ";
			message = MakeThisReadable( );
			RawMessageArr = message.Split( ' ' );
			
		}

		public abstract void Respond( string response );

		protected abstract string FormatResponse( string response );

		protected string[] RemoveHeader(string[] incoming ) {
			string[] retVal = new string[incoming.Length - 3];
			for(int i = 3; i < incoming.Length -3; i++ ) {
				retVal[i - 3] = incoming[i];
			}
			return retVal;
		}
		protected string MakeThisReadable( ) {
			
			return String.Join( " ", incoming, 3, incoming.Length - 3 ).Substring( 1 );
		}

	}
}
