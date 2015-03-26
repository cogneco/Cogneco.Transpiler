using System;
using Uri = Kean.Uri;

namespace Cogneco.Transpiler.Ooc.Tokens
{
	public class LeftBrace : Separator
	{
		public LeftBrace(Uri.Region region) : base("{", region)
		{
		}
	}
}
