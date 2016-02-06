using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BadwaterBallarina.Source.IRC.Messages;

namespace BadwaterBallarina.Source.IRC.Commands {
	class CmdNames : ICommand {
		public string Alias { get;  set; }

		public string HelpString {
			get {
				return "NOT YET IMPLEMENTED";
			}
		}

		public CmdNames( ) {
			Alias = "Names";
		}

		public void Execute( AIrcMessage message ) {
			message.Respond( "/Names" );
		}
	}
}
