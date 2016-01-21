using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadwaterBallarina.Config {
	class IRCConfig {
		//ToDo: Serialize this.  
		#region Autoproperties
		//Config Info
		public String Name { get; set; }
		//server info
		public string IrcAddr { get; set; }

		public int IrcPort { get; set; }

		//User Info
		public string IrcUser { get; set; }
		public string IrcNick { get; set; }
		public string IrcPassw { get; set; }

		//channel info
		public List<string> IrcChannels { get; set; }
		#endregion

	}
}
