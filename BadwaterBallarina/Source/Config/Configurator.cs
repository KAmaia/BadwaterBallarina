﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace BadwaterBallarina.Source.Config {
	class Configurator {
		public Configurator( ) {

		}
		public IRCConfig ReadAndCreateConfigFromFile( string filePath ) {
			//Listify this.  But not yet
			XmlDocument doc = new XmlDocument();
			doc.Load( filePath );
			Console.WriteLine( filePath );
			IRCConfig ircConfig = new IRCConfig();

			XmlNode root = doc.DocumentElement;

			//start reading the config.
			foreach ( XmlElement xe in root ) {

				//Get the Config's Name (Will be the same as the server name).
				//HAHAHA this one doesn't need validation!
				ircConfig.Name = xe.GetAttribute( "name" );
				
				//Validate Server Address
				//ToDo: RegEx!
				ircConfig.Addr = xe.GetAttribute( "addr" );
				
				//Will validate ports later.
				//ToDo: Validate Port Numbers.
				ircConfig.Port = int.Parse( xe.GetAttribute( "port" ) );

				//Get Our Username and Nick.
				ircConfig.User = xe.GetAttribute( "user_name" );
				ircConfig.Nick = xe.GetAttribute( "nick" );
				//Get the server password
				ircConfig.PassW = xe.GetAttribute( "passw" );
				
				Console.WriteLine( xe.Name );
				Console.WriteLine( xe.GetAttribute( "name" ) );
				Console.WriteLine( xe.GetAttribute( "addr" ) );
				Console.WriteLine( xe.GetAttribute( "port" ) );

				//ToDo: Replace this string list with actual channels.  
				//We'll Figure that out later.
				List<string> channelNames = new List<string>();
				foreach (XmlElement xe1 in xe ) {
					channelNames.Add(xe1.GetAttribute("addr"));
				}
				ircConfig.Channels = channelNames;

			}
			return ircConfig;
		}

	}

}







