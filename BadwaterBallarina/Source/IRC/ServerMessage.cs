﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadwaterBallarina.Source.IRC {
	class ServerMessage : AIrcMessage, IIrcMessage {
		public ServerMessage(StreamWriter outStream, string[] incoming): base(outStream, incoming ) {
			responseString = "";
		}
		public void Respond( string response ) {
			throw new NotImplementedException( );
		}
	}
}
