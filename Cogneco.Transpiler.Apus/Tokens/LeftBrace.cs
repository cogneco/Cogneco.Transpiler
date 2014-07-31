using System;
using Uri = Kean.Uri;

namespace Cogneco.Transpiler.Apus.Tokens
{
	public class LeftBrace : Separator
	{
		public LeftBrace(Uri.Region region) : base("{", region)
		{
		}
	}
}
