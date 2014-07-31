using System;
using Uri = Kean.Uri;

namespace Cogneco.Transpiler.Apus.Tokens
{
	public class RightBrace : Separator
	{
		public RightBrace(Uri.Region region) : base("}", region)
		{
		}
	}
}
