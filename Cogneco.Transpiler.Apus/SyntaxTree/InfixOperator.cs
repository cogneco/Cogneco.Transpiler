//
//  BinaryOperator.cs
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
using Kean.Extension;
using Text = Kean.IO.Text;

namespace Cogneco.Transpiler.Apus.SyntaxTree
{
	public class InfixOperator : Operator
	{
		readonly Associativity associativity;
		public Associativity Associativity { get { return this.associativity; } }
		public Expression Left { get; set; }
		public Expression Right { get; set; }
		InfixOperator(string symbol, int precedence, Associativity associativity) : base(symbol, precedence)
		{
			this.associativity = associativity;
		}
		protected override bool WriteHelper(Text.Indenter indenter)
		{
			var leftPrecedence = this.Precedence; 
			var rightPrecedence = this.Precedence;
			switch (this.Associativity)
			{
				case Associativity.Left:
					rightPrecedence++;
					break;
				case Associativity.Right:
					leftPrecedence++;
					break;
			}
			//leftPrecedence = rightPrecedence = int.MaxValue;
			return this.Left.Write(leftPrecedence, indenter) &&
			indenter.Write(this.Precedence > 200 ? this.Symbol : " " + this.Symbol + " ") &&
			this.Right.Write(rightPrecedence, indenter);
		}
		#region Static Create
		public static InfixOperator Create(Tokens.InfixOperator token)
		{
			InfixOperator result = InfixOperator.Create(token.Symbol);
			if (result.NotNull())
				result.Region = token.Region;
			return result;
		}
		public static InfixOperator Create(string symbol)
		{
			InfixOperator result;
			switch (symbol)
			{
				default:
					result = null;
					break;
			// Resolving
				case ".":
					result = new InfixOperator(symbol, 300, Associativity.None);
					break;
			// Exponentive
				case "<<":
				case ">>":
					result = new InfixOperator(symbol, 160, Associativity.None);
					break;
			// Multiplicative
				case "*":
				case "/":
				case "%":
				case "&":
					result = new InfixOperator(symbol, 150, Associativity.Left);
					break;
			// Additive
				case "+":
				case "-":
				case "|":
				case "^":
					result = new InfixOperator(symbol, 140, Associativity.Left);
					break;
			// Range
				case "..":
				case "...":
					result = new InfixOperator(symbol, 135, Associativity.None);
					break;
			// Cast
				case "is":
				case "as":
					result = new InfixOperator(symbol, 132, Associativity.None);
					break;
			// Comparative
				case "<":
				case "<=":
				case ">":
				case ">=":
				case "==":
				case "!=":
				case "===":
				case "!==":
				case "~=":
					result = new InfixOperator(symbol, 130, Associativity.None);
					break;
			// Conjuctive
				case "&&":
					result = new InfixOperator(symbol, 120, Associativity.Left);
					break;
			// Disjunctive
				case "||":
					result = new InfixOperator(symbol, 110, Associativity.Left);
					break;
			// Assignment
				case "=":
				case "*=":
				case "/=":
				case "+=":
				case "-=":
				case "<<=":
				case ">>=":
				case "&=":
				case "^=":
				case "|=":
				case "&&=": 
				case "||=":
					result = new InfixOperator(symbol, 90, Associativity.Right);
					break;
			}
			return result;
		}
		#endregion
	}
}

