﻿using System;
using Uri = Kean.Uri;

namespace Cogneco.Transpiler.Ooc.Tokens
{
	public class LeftBracket : Separator
	{
		public LeftBracket(Uri.Region region) : base("[", region)
		{
		}
	}
}

