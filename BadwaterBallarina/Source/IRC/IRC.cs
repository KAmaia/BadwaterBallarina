using System;
using System.IO;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

using BadwaterBallarina.Source.Config;
using BadwaterBallarina.Source.IRC.Messages;
using BadwaterBallarina.Source.IRC.Commands;

namespace BadwaterBallarina.Source.IRC {
	class IRC {
		private string cmdPrefix = ">";
		private List<ICommand> Commands;

		private IRCConfig ircConfig;

		private bool connected;
		private StreamWriter ircWriter;

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
			Commands = new List<ICommand>( );
			Commands.Add( new CmdHello( ) );
		}
		#endregion

		#region Connection
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

		//why do it this way?  Who cares?
		private void InternalConnect( ) {
			IRCConnection = new TcpClient( ircConfig.Addr, ircConfig.Port );
			IRCConnection.ReceiveTimeout = 1000 * 60 * 5;
			IRCStream = IRCConnection.GetStream( );
			IRCReader = new StreamReader( IRCStream );
			IRCWriter = new StreamWriter( IRCStream );
			ircWriter = IRCWriter;

			Console.WriteLine( "Connected to: {0}", ircConfig.Addr );
			Console.WriteLine( "Sending Login Info!" );
			SendServerMessage( String.Format( "USER {0} {1} * : {2}", ircConfig.Nick, 0, ircConfig.Nick ) );
			SendServerMessage( String.Format( "NICK {0}", ircConfig.Nick ) );

			//Time to Authenticate.  
			//ToDo: Replace with SASL
			SendServerMessage( "PRIVMSG NICKSERV :identify " + ircConfig.PassW );

			//sleep for 5 seconds so that we have a chance to auth and get our mask, if applicable.
			Thread.Sleep( 5000 );
			JoinChannels( );
			connected = true;
		}
		//ToDo: Move this crap the hell out of here.
		private void HandleServerCrapBecauseThereIsALotOfIt( string[ ] incoming ) {
			string switcher = incoming[1];
			switch ( switcher ) {
				//Ignore everything except Nick in use;
				//Because really that's all I care about.
				case "433":
					Random rand = new Random();
					SendServerMessage( String.Format( "NICK {0}", ircConfig.Nick + rand.Next( 10000 ) ) );
					SendServerMessage( String.Format( "PRIVMSG NICKSERV :GHOST {0} {1}", ircConfig.Nick, ircConfig.PassW ) );
					Thread.Sleep( 2000 );
					SendServerMessage( String.Format( "NICK {0}", ircConfig.Nick ) );
					SendServerMessage( String.Format( "PRIVMSG NICKSERV :Identify {0}", ircConfig.PassW ) );
					Thread.Sleep( 5000 );
					JoinChannels( );
					break;
			}
		}

		#endregion

		//ToDo: Create Listener Class and move this crap there.
		#region Listening <<--this will move to Listener.cs at some point.
		private void Listen( ) {
			while ( connected ) {
				string incoming;
				while ( ( incoming = IRCReader.ReadLine( ) ) != null ) {
					string[] incomingSplit = CleanUpIncoming(incoming);
					if ( IsPing( incomingSplit ) ) {
						new Ping( ircWriter, incomingSplit ).Respond( );
					}
					else if ( IsServerMessage( incomingSplit ) ) {
						HandleServerCrapBecauseThereIsALotOfIt( incomingSplit );
					}
					else {
						handleChatMessage( incomingSplit );
					}
				}
			}
		}

		private string[ ] CleanUpIncoming( string incoming ) {
			if ( incoming.StartsWith( ":" ) ) {
				incoming = incoming.Substring( 1 );
			}
			return incoming.Split( ' ' );
		}

		private bool IsPing( string[ ] incoming ) {
			//Check if this is a ping message.
			return incoming[0].ToLower( ) == "ping";
		}

		private bool IsServerMessage( string[ ] incoming ) {
			//check to see if the sender is the server
			string sender = incoming[0];
			Regex regex = new Regex(@"\..*\..+$");
			Match possibleSender = regex.Match(sender);
			Match serverMatch = regex.Match(ircConfig.Addr);
			if ( !serverMatch.Success ) {
				throw new Exception( "Hey, something went horribly wrong in IsServerMessage!" );
			}
			if ( !possibleSender.Success ) {
				return false;
			}
			string possVal = possibleSender.Value;
			string servVal = serverMatch.Value;
			return ( servVal == possVal );
		}

		#region delete these
		//OOPS!  I forgot to validate the nick by running 
		//BOTH incoming[2] and config.Nick through ToLower()
		private bool IsPrivateMessage( string[ ] incoming ) {
			return incoming[2].ToLower( ).Equals( ircConfig.Nick.ToLower( ) );
		}

		private bool IsChannelMessage( string[ ] incoming ) {
			return incoming[2].StartsWith( "#" );
		}
		#endregion
		#endregion

		private void handleChatMessage( string[ ] incoming ) {
			string switcher = incoming[1];
			switch ( switcher ) {
				case "JOIN":
					//doStuff(tm);
					break;
				case "PART":
					//doStuff(tm);
					break;
				case "QUIT":
					//doStuff(tm);
					break;
				case "MODE":
					//doStuff(tm);
					break;
				case "NICK":
					//doStuff(tm);
					break;
				case "Kick":
					//doStuff(tm);
					break;
				case "NOTICE":
					//doStuff(tm);
					foreach ( string s in incoming ) {
						Console.Write( s + " " );
					}
					Console.WriteLine( "" );
					break;
				//ToDo: Handle Actions.
				case "PRIVMSG":
					Console.WriteLine( "==+==" );
					foreach(string s in incoming ) {
						Console.WriteLine( s );
					}
					Console.WriteLine( "==+==" );
					if ( !IsCommand( incoming ) ) {
						if ( IsChannelMessage( incoming ) ) {
							ChannelMessage cm = new ChannelMessage(ircWriter, incoming);
							cm.Respond( string.Format( "Got That, {0}", cm.Sender ) );
						}
						else if ( IsPrivateMessage( incoming ) ) {
							PrivateMessage pm = new PrivateMessage(ircWriter, incoming);
							pm.Respond( string.Format( "Got That, {0}", pm.Sender ) );

						}
					}
					break;
			}
		}

		private bool IsCommand( string[ ] incoming ) {
			string match = incoming[3].Substring(1);
			Console.WriteLine( match );
			if ( match.StartsWith( cmdPrefix )){
				match = match.Substring( 1 );
				foreach ( ICommand i in Commands ) {
					if ( match.ToLower( ).Equals( i.Alias.ToLower() )){
						if ( IsChannelMessage( incoming ) ) {
							ChannelMessage cm = new ChannelMessage(ircWriter, incoming);
							i.Execute( cm );
						}
						else if ( IsPrivateMessage( incoming ) ) {
							PrivateMessage pm = new PrivateMessage(ircWriter, incoming);
							i.Execute( pm );
						}
					}
				}
				return true;
			}
			else {
				return false;
			}
		}

		private void JoinChannel( string channel ) {
			SendServerMessage( String.Format( "JOIN {0}", channel ) );
		}

		private void JoinChannels( ) {
			foreach ( string channel in ircConfig.Channels ) {
				Console.WriteLine( "Joining {0}", channel );
				JoinChannel( channel );
			}
		}

		private void SendServerMessage( string message ) {
			IRCWriter.WriteLine( message );
			IRCWriter.Flush( );
		}
	}
}