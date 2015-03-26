//
//  SyntaxError.cs
//
//  Author:
//       Simon Mika <simon@mika.se>
//
//  Copyright (c) 2015 Simon Mika
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
using Kean.Extension;
using Error = Kean.Error;
using Uri = Kean.Uri;

namespace Cogneco.Transpiler.Ooc.Exception
{
	public class SyntaxError : Transpiler.Exception.SyntaxError
	{
		internal SyntaxError(string expected, string found, Uri.Region region) : this(null, expected, found, region)
		{
		}
		internal SyntaxError(string expected, Tokens.Lexer lexer)
			: this(null, expected, lexer.Current.NotNull() ? "\"" + lexer.Current.Raw + "\"" : "nothing", lexer.Current.NotNull() ? lexer.Current.Region : lexer.Last.Region)
		{
		}
		internal SyntaxError(System.Exception innerException, string expected, string found, Uri.Region region) : base(innerException, expected, found, region)
		{
		}
	}
}

