using System;
using System.IO;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BadwaterBallarina.Config;

namespace BadwaterBallarina {
	class IRC {

		//ToDo: Make all of this a config, and just get the values from there.
		//That way we can 
		//forEach(ircConfig ic in IRC Configs){
		//	InternalConnect(ic);
		// }
		private string ircPW;
		private string ircNick;
		private int ircPort;
		private string ircAddr;
		private List<string> ircChannels;

		private bool connected;

		#region NETWORK STUFF
		public TcpClient IRCConnection { get; private set; }
		public NetworkStream IRCStream { get; private set; }
		public StreamReader IRCReader { get; private set; }
		public StreamWriter IRCWriter { get; private set; }
		#endregion

		#region CTOR
		public IRC(IRCConfig config ) {
			this.ircAddr = config.IrcAddr;
			this.ircPort = config.IrcPort;
			this.ircNick = config.IrcNick;
			this.ircPW = config.IrcPassw;
			this.ircChannels = config.IrcChannels;
			connected = false;
		}
		#endregion

		public void Connect( ) {
			if ( !connected ) {
				InternalConnect( );
				while ( connected ) {
					
				}
			}
			else {
				//already connected.  Return Error.
				return;
			}
		}

		//why do it this way?  Who cares?
		private void InternalConnect( ) {
			this.IRCConnection = new TcpClient( ircAddr, ircPort );
			this.IRCConnection.ReceiveTimeout = 1000 * 60 * 5;
			this.IRCStream = IRCConnection.GetStream( );
			this.IRCReader = new StreamReader( IRCStream );
			this.IRCWriter = new StreamWriter( IRCStream );
			Console.WriteLine( IRCReader.Peek( ) );

			Console.WriteLine( "Connected to: {0}", ircAddr );
			Console.WriteLine( "Sending Login Info!" );
			SendServerMessage( String.Format( "USER {0} {1} * : {2}", ircNick, 0, ircNick ) );
			SendServerMessage( String.Format( "NICK {0}", ircNick ) );
			JoinChannels( );
			connected = true;
		}
		private void JoinChannels( ) {
			foreach ( string channel in ircChannels ) {
				Console.WriteLine( "Joining {0}", channel );
				SendServerMessage( String.Format( "JOIN {0}", channel ) );
			}
		}





		private void SendServerMessage( string message ) {
			IRCWriter.WriteLine( message );
			IRCWriter.Flush( );
		}
	}
}