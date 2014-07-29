using System;
using Uri = Kean.Uri;

namespace Cogneco.Transpiler.FrontEnd.Apus.Tokens
{
	public class RightBracket : Separator
	{
		public RightBracket(Uri.Region region) : base("]", region)
		{
		}
	}
}

