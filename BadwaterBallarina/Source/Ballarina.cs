using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Local Uses
using BadwaterBallarina.Source.Config;
using BadwaterBallarina.Source.IRC;

namespace BadwaterBallarina {
	class Ballarina {
		private IRC ballarinaIRC;

		static void Main( string[ ] args ) {
			Ballarina badwaterBallarina = new Ballarina();
			badwaterBallarina.ballarinaIRC.Connect( );
			Console.WriteLine( "Press Any Key To Continue!" );
			Console.Read( );
		}
		//private CTor, because we don't need it to be public.
		private Ballarina( ) {
			Configurator cf = new Configurator();
			//ToDo: Unhardcode config path.
			ballarinaIRC = new IRC( cf.ReadAndCreateConfig( "..\\Configs\\connections.cfg" ) );
		}
	}
}
