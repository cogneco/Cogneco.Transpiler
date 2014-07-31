using System;
using Uri = Kean.Uri;
using Generic = System.Collections.Generic;

namespace Cogneco.Transpiler.Apus.Tokens
{
	public abstract class Token
	{
		public readonly string Raw;
		public readonly Uri.Region Region;
		protected Token(string raw, Uri.Region region)
		{
			this.Raw = raw;
			this.Region = region;
		}
		public override string ToString()
		{
			return this.Raw;
		}
	}
}

