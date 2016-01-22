using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Local Uses
using BadwaterBallarina.Config;
using BadwaterBallarina.source.IRC;

namespace BadwaterBallarina {
	class Ballarina {
		private IRC ballarinaIRC;

		static void Main( string[ ] args ) {
			Ballarina badwaterBallarina = new Ballarina();
			badwaterBallarina.ballarinaIRC.Connect( );
			Console.WriteLine( "Press Any Key To Continue!" );
			Console.Read( );
		}

		private Ballarina( ) {
			Configurator cf = new Configurator();

			//Commented out so we can test the CONFIGURATOR!
			ballarinaIRC = new IRC( cf.ReadAndCreateConfig( "..\\Configs\\connections.cfg" ) );

		}

	
		private void IRCCommandReceived(string ircCommand ) {

		}

	}
}
