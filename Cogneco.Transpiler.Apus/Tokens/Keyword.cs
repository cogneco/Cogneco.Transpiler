using System;
using Uri = Kean.Uri;

namespace Cogneco.Transpiler.Apus.Tokens
{
	public class Keyword : Token
	{
		public readonly Keywords Name;
		public Keyword(Keywords name, string raw, Uri.Region region) : base(raw, region)
		{
			this.Name = name;
		}
	}
}

