﻿//
//  Identifier.cs
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

namespace Cogneco.Transpiler.FrontEnd.Apus.SyntaxTree
{
	public class Identifier : Expression
	{
		public override int Precedence { get { return 500; } }
		public Identifier Parent { get; set; }
		public Declaration Declaration { get; set; }
		readonly string name;
		public string Name { get { return this.name; } }
		public Identifier(string name)
		{
			this.name = name;
		}
		protected override string ToStringHelper()
		{
			return this.Name;
		}
		#region Static Parse
		internal static Identifier ParseIdentifier(Parser parser)
		{
			Tokens.Identifier current = parser.Current as Tokens.Identifier;
			parser.Next();
			Identifier result = null;
			if (parser.Current is Tokens.LeftParenthesis)
				;// function call do ParseTupleExpression
			else if (parser.Current is Tokens.BinaryOperator && (parser.Current as Tokens.BinaryOperator).Name == ".")
				;// parent resolve this with recursivecall
			else
				result = new Identifier(current.Name) { Region = current.Region };
			return result;
		}
		#endregion
	}
}

