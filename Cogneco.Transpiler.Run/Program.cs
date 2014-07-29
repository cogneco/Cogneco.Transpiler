using System;
using Uri = Kean.Uri;
using Argument = Kean.Cli.Argument;
using IO = Kean.IO;
using Kean.Extension;

namespace Cogneco.Transpiler.Run
{
	class Program
	{
		public static void Main(string[] arguments)
		{
			Uri.Locator program = null;
			var argumentParser = new Argument.Parser();
			argumentParser.UnassociatedParameterHandler = argument => program = argument;
			argumentParser.Parse(arguments);
			using (var lexer = FrontEnd.Apus.Tokens.Lexer.Open(IO.CharacterReader.Open(program)))
				if (lexer.NotNull())
					foreach (var token in lexer)
						Console.Write(token + "|");
		}
	}
}
