using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadwaterBallarina.Config {
	class IRCConfig {
		#region fields
		//Config Info
		private String name;
		//Server Info
		private string ircAddr;
		private int ircPort;

		//User Info
		private string ircUser;
		private string ircNick;
		private string ircPassw;

		//channel info
		private List<string> ircChannels;
		#endregion

		#region properties
		//Config Info
		public String Name {
			get { return name; }
			set { name = value; }
		}
		//server info
		public string IrcAddr {
			get { return ircAddr; }
			set { ircAddr = value; }
		}
		public int IrcPort {
			get { return ircPort; }
			set { ircPort = value; }
		}

		//User Info
		public string IrcUser {
			get { return ircUser; }
			set { ircUser = value; }
		}
		public string IrcNick {
			get { return ircNick; }
			set { ircNick = value; }
		}
		public string IrcPassw {
			get { return ircPassw; }
			set { ircPassw = value; }
		}

		//channel info
		public List<string> IrcChannels {
			get { return ircChannels; }
			set { ircChannels = value; }
		}
		#endregion

	}
}
