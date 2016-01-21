using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadwaterBallarina {
	class Ballarina {
		private IRC ballarinaIRC;

		static void Main( string[ ] args ) {
			Ballarina badwaterBallarina = new Ballarina();
			badwaterBallarina.ballarinaIRC.Connect( );
		}

		private Ballarina( ) {
			ballarinaIRC = new IRC( "irc.freenode.net", 6667, "BW_Ballarina", "**********", "#Badwater");
			
		}

	}
}
