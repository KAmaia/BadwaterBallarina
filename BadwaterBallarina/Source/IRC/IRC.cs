using System;
using System.IO;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


using BadwaterBallarina.Config;
using System.Text.RegularExpressions;

namespace BadwaterBallarina.source.IRC {
	class IRC {

		private bool connected;

		private IRCConfig ircConfig;

		#region NETWORK STUFF
		public TcpClient IRCConnection { get; private set; }
		public NetworkStream IRCStream { get; private set; }
		public StreamReader IRCReader { get; private set; }
		public StreamWriter IRCWriter { get; private set; }
		#endregion

		#region CTOR
		public IRC( IRCConfig config ) {
			ircConfig = config;

			connected = false;
		}
		#endregion

		public void Connect( ) {
			if ( !connected ) {
				InternalConnect( );
				while ( connected ) {
					Listen( );
				}
			}
			else {
				//already connected.  Return.
				return;
			}
		}

		private void Listen( ) {
			while ( connected ) {
				string incoming;
				while ( ( incoming = IRCReader.ReadLine( ) ) != null ) {
					if ( incoming.StartsWith( ":" ) ) {
						incoming = incoming.Remove( 0, 1 );
					}
					Console.Out.WriteLine( incoming );
					string[] incomingSplit = incoming.Split(' ');
					if ( IsPing( incomingSplit ) ) {
						PingRespond( incomingSplit );
					}
					if ( IsServerMessage( incomingSplit ) ) {
						HandleServerCrapBecauseThereIsALotOfIt( incomingSplit );
					}
				}
			}
		}

		private void HandleServerCrapBecauseThereIsALotOfIt(string[] incoming ) {
			string switcher = incoming[1];
				switch(switcher){
					//Ignore everything except Nick in use;
					case "433":
					Random rand = new Random();
					SendServerMessage(String.Format("NICK {0}", ircConfig.IrcNick + rand.Next(10000)));
					SendServerMessage( String.Format( "PRIVMSG NICKSERV :GHOST {0} {1}", ircConfig.IrcNick, ircConfig.IrcPassw ) );
					Thread.Sleep( 2000 );
					SendServerMessage( String.Format( "NICK {0}", ircConfig.IrcNick ) );
					SendServerMessage( String.Format( "PRIVMSG NICKSERV :Identify {0}", ircConfig.IrcPassw ) );
					Thread.Sleep( 5000 );
					JoinChannels( );
					break;
			}
		}

		private bool IsServerMessage( string[] incoming ) {
			//check to see if the sender is the server
			string sender = incoming[0];
			Regex regex = new Regex(@"\..*\..+$");
			Match possibleSender = regex.Match(sender);
			Match serverMatch = regex.Match(ircConfig.IrcAddr);
			if ( !serverMatch.Success ) {
				throw new Exception( "Hey, something went horribly wrong in serverMessage!" );
			}
			if ( !possibleSender.Success ) {
				return false;
			}
			string possVal = possibleSender.Value;
			string servVal = serverMatch.Value;
			return ( servVal == possVal ); 
		}

		private bool IsPing( string[ ] incoming ) {
			return incoming[0].ToLower( ) == "ping";
		}

		private void PingRespond( string[ ] incoming ) {
			Console.Beep( );
			string pingHash = "";
			pingHash = string.Join( " ", incoming, 1, incoming.Length - 1 );
			Console.WriteLine( "Responding with PONG {0}", pingHash );
			SendServerMessage( "PONG " + pingHash );
		}
		private void JoinChannel( string channel ) {
			SendServerMessage( String.Format( "JOIN {0}", channel ) );
		}

		//why do it this way?  Who cares?
		private void InternalConnect( ) {
			IRCConnection = new TcpClient( ircConfig.IrcAddr, ircConfig.IrcPort );
			IRCConnection.ReceiveTimeout = 1000 * 60 * 5;
			IRCStream = IRCConnection.GetStream( );
			IRCReader = new StreamReader( IRCStream );
			IRCWriter = new StreamWriter( IRCStream );


			Console.WriteLine( "Connected to: {0}", ircConfig.IrcAddr );
			Console.WriteLine( "Sending Login Info!" );
			SendServerMessage( String.Format( "USER {0} {1} * : {2}", ircConfig.IrcNick, 0, ircConfig.IrcNick ) );
			SendServerMessage( String.Format( "NICK {0}", ircConfig.IrcNick ) );

			//Time to Authenticate.  
			//ToDo: Replace with SASL
			SendServerMessage( "PRIVMSG NICKSERV :identify " + ircConfig.IrcPassw );

			//sleep for 5 seconds so that we have a chance to auth and get our mask, if applicable.
			Thread.Sleep( 5000 );
			JoinChannels( );
			connected = true;
		}



		private void JoinChannels( ) {
			foreach ( string channel in ircConfig.IrcChannels ) {
				Console.WriteLine( "Joining {0}", channel );
				JoinChannel( channel );
			}
		}


		private void sendChannelMessage( string channel, string message ) {
			Console.WriteLine( channel );
			if ( ircConfig.IrcChannels.Contains( channel ) ) {

				SendServerMessage( String.Format( "PRIVMSG {0} : {1}", channel, message ) );
			}
		}

		private void ListenForMessages( ) {
			while ( connected ) {

			}
		}

		private void SendServerMessage( string message ) {
			IRCWriter.WriteLine( message );
			IRCWriter.Flush( );
		}
	}
}