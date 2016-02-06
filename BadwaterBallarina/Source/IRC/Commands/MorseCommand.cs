using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BadwaterBallarina.Source.IRC.Messages;
using BadwaterBallarina.Source.Morse;
using System.Threading;

namespace BadwaterBallarina.Source.IRC.Commands {
	class CmdMorse : ICommand {
		public string Alias { get; set; }

		public string HelpString {
			get {
				return "Annoys the living hell out of Irinix";
			}
		}

		private object myLock = new object();

		const int DIT_LENGTH = 100;
		const int DAH_LENGTH = DIT_LENGTH * 3;
		const int FREQ = 700;

		public CmdMorse( ) {
			Alias = "Morse";
		}

		public void Execute( AIrcMessage message ) {
			//strip the Alias
			string convertMe = message.Message.Substring(Alias.Length + 1).ToLower();

			string response = "";
			foreach ( char c in convertMe ) {
				response += MorseLookupTable.LookupChar( c ) + " ";
			}
			message.Respond( response );
			//Uncomment below to simulate a radio room circa 1900

			//Thread morsePlayThread = new Thread(() => playMorse( response ));
			//morsePlayThread.Start( );
		}
		private void playMorse( string s ) {
			lock ( myLock ) {
				foreach ( char c in s ) {
					switch ( c ) {
						case '.':
							Console.Beep( FREQ, DIT_LENGTH );
							break;
						case '-':
							Console.Beep( FREQ, DAH_LENGTH );
							break;
						case ' ':
							Thread.Sleep( DAH_LENGTH );
							break;
						default:
							Thread.Sleep( DAH_LENGTH );
							break;
					}
				}
			}
		}
	}
}
