using System;
using Uri = Kean.Uri;
using Argument = Kean.Cli.Argument;
using IO = Kean.IO;
using Kean.Extension;
using Collection = Kean.Collection;

namespace Cogneco.Transpiler.Run
{
	class Program
	{
		public static void Main(string[] arguments)
		{
			var compiler = new Ooc.Compiler();
			var modules = new Collection.List<Uri.Locator>();
			var argumentParser = new Argument.Parser();
			argumentParser.UnassociatedParameterHandler = argument => modules.Add(argument);
			argumentParser.Parse(arguments);
			compiler.Compile(modules);
		}
	}
}
