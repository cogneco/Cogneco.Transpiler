using System;
using Uri = Kean.Uri;

namespace Cogneco.Transpiler.Apus.Tokens
{
	public class RightBracket : Separator
	{
		public RightBracket(Uri.Region region) : base("]", region)
		{
		}
	}
}

