using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BadwaterBallarina.Source.IRC.Messages;

namespace BadwaterBallarina.Source.IRC.Commands {
	class CmdHello : ICommand {
		public string Alias { get;  }

		public string HelpString {
			get {
				return "Says Hello To <sender>";
			}
		}

		public CmdHello( ) {
			Alias = "Hello";
		}

		public void Execute( AIrcMessage message ) {
			message.Respond( "Hello " + message.Sender );
		}
	}
}
