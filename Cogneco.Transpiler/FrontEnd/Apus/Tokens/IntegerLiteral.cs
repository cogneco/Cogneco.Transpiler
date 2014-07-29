//
//  IntegerLiteral.cs
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

namespace Cogneco.Transpiler.FrontEnd.Apus.Tokens
{
	public class IntegerLiteral : Literal
	{
		public readonly long Value;
		IntegerLiteral(long value, string raw, Uri.Region region) : base(raw, region)
		{
			this.Value = value;
		}
		public static IntegerLiteral Parse(string raw, Uri.Region region)
		{
			IntegerLiteral result = null;
			long v;
			if (long.TryParse(raw, out v))
				result = new IntegerLiteral(v, raw, region);
			else
				new Exception.LexicalError("an integel literal", "\"" + raw + "\"", region).Throw();
			return result;
		}

	}
}

