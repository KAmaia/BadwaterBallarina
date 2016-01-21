using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadwaterBallarina.Source {
	class IRCConfig {
		#region fields
		//Server Info
		private string ircServer;
		private int ircPort;

		//User Info
		private string ircUser;
		private string ircNick;
		private string ircPassw;

		//channel info
		private List<string> ircChannels;
		#endregion

		#region properties
		//server info
		public string IrcServer { get { return ircServer; } }
		public int IrcPort { get { return ircPort; } }
		
		//User Info
		public string IrcUser { get { return ircUser; } }
		public string IrcNick { get { return ircNick; } }
		public string IrcPassw { get { return ircPassw; } }

		//channel info
		public List<string> IrcChannels { get { return ircChannels; } }
		#endregion

	}
}
