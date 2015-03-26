using System;
using Uri = Kean.Uri;

namespace Cogneco.Transpiler.Ooc.Tokens
{
	public class RightBracket : Separator
	{
		public RightBracket(Uri.Region region) : base("]", region)
		{
		}
	}
}

