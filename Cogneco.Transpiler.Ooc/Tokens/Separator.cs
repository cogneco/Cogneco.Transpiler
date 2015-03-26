//
//  Separator.cs
//
//  Author:
//       Simon Mika <simon@mika.se>
//
//  Copyright (c) 2014 Simon Mika
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using Uri = Kean.Uri;
using System.Security.Cryptography.X509Certificates;

namespace Cogneco.Transpiler.Ooc.Tokens
{
	public abstract class Separator : Token
	{
		protected Separator(string raw, Uri.Region region) : base(raw, region)
		{
		}
		public static Separator Parse(char data, Uri.Region region)
		{
			Separator result = null;
			switch (data)
			{
				case '(':
					result = new LeftParenthesis(region);
					break;
				case ')':
					result = new RightParenthesis(region);
					break;
				case '[':
					result = new LeftBracket(region);
					break;
				case ']':
					result = new RightBracket(region);
					break;
				case '{':
					result = new LeftBrace(region);
					break;
				case '}':
					result = new RightBrace(region);
					break;
				case ',':
					result = new Comma(region);
					break;
				case ';':
					result = new Semicolon(region);
					break;
			}
			return result;
		}
	}
}

