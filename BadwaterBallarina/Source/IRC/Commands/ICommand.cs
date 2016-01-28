using BadwaterBallarina.Source.IRC.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadwaterBallarina.Source.IRC.Commands {
	interface ICommand {
		string Alias { get;  set; }
		void Execute(AIrcMessage message );
	}
}
