﻿using System;
using Uri = Kean.Uri;

namespace Cogneco.Transpiler.Ooc.Tokens
{
	public class RightBrace : Separator
	{
		public RightBrace(Uri.Region region) : base("}", region)
		{
		}
	}
}
