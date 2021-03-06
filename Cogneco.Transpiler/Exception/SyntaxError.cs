﻿//
//  SyntaxError.cs
//
//  Author:
//       Simon Mika <simon@mika.se>
//
//  Copyright (c) 2014 Simon Mika
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using Error = Kean.Error;
using Uri = Kean.Uri;

namespace Cogneco.Transpiler.Exception
{
	public class SyntaxError : Exception
	{
		public string Expected { get; private set; }
		public string Found { get; private set; }
		public Uri.Region Region { get; private set; }
		protected internal SyntaxError(string expected, string found, Uri.Region region) : this(null, expected, found, region)
		{
		}
		protected internal SyntaxError(System.Exception innerException, string expected, string found, Uri.Region region) : base(innerException, Error.Level.Critical, "Syntax Error", "Expected {0}, but found {1}, at {2}.", expected, found, region.ToString())
		{
			this.Expected = expected;
			this.Found = found;
			this.Region = region;
		}
	}
}

