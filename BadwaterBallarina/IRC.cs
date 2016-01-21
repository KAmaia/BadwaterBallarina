using System;
using System.IO;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadwaterBallarina {
	class IRC {
		private string authPW;
		private string ircNick;
		private int ircPort;
		private string ircServer;
		private string ircChannels;

		private bool connected;

		#region NETWORK STUFF
		public TcpClient IRCConnection { get; private set; }
		public NetworkStream IRCStream { get; private set; }
		public StreamReader IRCReader { get; private set; }
		public StreamWriter IRCWriter { get; private set; }
		#endregion

		#region CTOR
		public IRC(string ircServer, int ircPort, string ircNick, string authPW, string ircChannels ) {
			this.ircServer = ircServer;
			this.ircPort = ircPort;
			this.ircNick = ircNick;
			this.authPW = authPW;
			this.ircChannels = ircChannels;
			connected = false;
		}
		#endregion

		public void Connect( ) {
			if ( !connected ) {
				InternalConnect( );
				connected = true;
				while ( connected ) {
					//doStuff;
				}
			}
			else {
				//already connected.  Return Error.
				return;
			}
		}

		//why do it this way?  Who cares?
		private void InternalConnect( ) {
			this.IRCConnection = new TcpClient( ircServer, ircPort );
			this.IRCConnection.ReceiveTimeout = 1000 * 60 * 5;
			this.IRCStream = IRCConnection.GetStream( );
			this.IRCReader = new StreamReader( IRCStream );
			this.IRCWriter = new StreamWriter( IRCStream );
			Console.WriteLine( IRCReader.Peek() );

			Console.WriteLine( "Connected to: {0}", ircServer );
			Console.WriteLine( "Sending Login Info!" );
			SendServerMessage( String.Format( "USER {0} {1} * : {2}", ircNick, 0, ircNick ) );
			SendServerMessage( String.Format( "NICK {0}", ircNick ) );
			SendServerMessage( String.Format( "JOIN {0}", ircChannels ) );
			



		}

		private void SendServerMessage( string message) {
			IRCWriter.WriteLine( message );
			IRCWriter.Flush( );
		}	
	}
}
