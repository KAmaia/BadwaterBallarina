using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadwaterBallarina.Source.Config {
	class IRCConfig {
		//ToDo: Serialize this.  
		#region Autoproperties
		//Config Info
		public String Name { get; set; }
		//server info
		public string Addr { get; set; }

		public int Port { get; set; }

		//User Info
		public string User { get; set; }
		public string Nick { get; set; }
		public string PassW { get; set; }

		//channel info
		public List<string> Channels { get; set; }
		#endregion

	}
}
