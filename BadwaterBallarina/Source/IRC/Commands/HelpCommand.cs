using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BadwaterBallarina.Source.IRC.Messages;

namespace BadwaterBallarina.Source.IRC.Commands {
	class HelpCommand : ICommand {
		List<ICommand> commands;
		public HelpCommand( List<ICommand> commands ) {
			this.commands = commands;
		}
		public string Alias {
			get { return "Help"; }
		}

		public string HelpString {
			get { return "This Screen"; }
		}

		public void Execute( AIrcMessage message ) {
			string seperator = " || ";
			if ( message.RawMessageArr.Length <= 1 ) {
				foreach ( ICommand ic in commands ) {
					string response = ic.Alias + seperator + ic.HelpString;
					message.Respond( response );
				}
			}
			else {
				//parse the command out of RawMessagArr
			}
		}
	}
}

