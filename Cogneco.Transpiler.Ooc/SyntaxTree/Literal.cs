//
//  Literal.cs
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

namespace Cogneco.Transpiler.Ooc.SyntaxTree
{
	public abstract class Literal : Expression
	{
		public override int Precedence { get { return 500; } }
		public string Raw { get; set; }
		protected Literal(Tokens.Literal token)
		{
			this.Raw = token.Raw;
			this.Region = token.Region;
		}
		#region Static Create
		public static Literal Create(Tokens.Literal token)
		{
			return 
				token is Tokens.IntegerLiteral ? (Literal)new IntegerLiteral(token as Tokens.IntegerLiteral) :
				token is Tokens.FloatingPointLiteral ? (Literal)new FloatingPointLiteral(token as Tokens.FloatingPointLiteral) :
				null;
		}
		#endregion
	}
}

