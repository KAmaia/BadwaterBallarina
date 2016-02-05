using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BadwaterBallarina.Source.Morse {
	class MorseLookupTable {

		const int DIT_LENGTH = 100;
		const int DAH_LENGTH = DIT_LENGTH * 3;
		const int FREQ = 700;
		
		private static Dictionary<char, string> morseLookup = new Dictionary<char, string> {
			//letters
			{ 'a', ".-"    },
			{ 'b', "-..."  },
			{ 'c', "-.-."  },
			{ 'd', "-.."   },
			{ 'e', "."     },
			{ 'f', "..-."  },
			{ 'g', "--."   },
			{ 'h', "...."  },
			{ 'i', ".."    },
			{ 'j', ".---"  },
			{ 'k', "-.-"   },
			{ 'l', ".-..." },
			{ 'm', "--"    },
			{ 'n', "-."    },
			{ 'o', "---"   },
			{ 'p', ".--"   },
			{ 'q', "--.-"  },
			{ 'r', ".-."   },
			{ 's', "..."   },
			{ 't', "-"     },
			{ 'u', "..-"   },
			{ 'v', "...-"  },
			{ 'w', ".--"   },
			{ 'x', "-..-"  },
			{ 'y', "-.--"  },
			{ 'z', "--.."  },
			//numbers
			{ '0', "-----" },
			{ '1', ".----" },
			{ '2', "..---" },
			{ '3', "...--" },
			{ '4', "....-" },
			{ '5', "....." },
			{ '6', "-...." },
			{ '7', "--..." },
			{ '8', "---.." },
			{ '9', "----." },
			//punctuation
			{ '.', ".-.-.-" },
			{ ',', "--..--" },
			{ '-', "-....-" },
			{ '/', "-..-."  },
			{ '[', "-.--.-" },
			{ ']', "-.--.-" },
			{ '(', "-.--.-" },
			{ ')', "-.--.-" },
			{ '=', "-...-"  },
			{ '?', "..--.." },
		};

		public static string LookupChar( char c ) {

			if ( morseLookup.ContainsKey( c ) ) {
				return morseLookup[c];
			}
			else if ( c.Equals( ' ' ) ) {
				return " ";
			}
			else return "";

		}

	}
}
