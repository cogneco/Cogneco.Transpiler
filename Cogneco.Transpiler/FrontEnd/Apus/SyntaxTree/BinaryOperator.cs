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

namespace Cogneco.Transpiler.FrontEnd.Apus.SyntaxTree
{
	public class BinaryOperator : Operator
	{
		readonly string symbol;
		public override string Symbol { get { return this.symbol; } }
		readonly int precedence;
		public override int Precedence { get { return this.precedence; } }
		readonly Associativity associativity;
		public Associativity Associativity { get { return this.associativity; } }
		public Expression Left { get; set; }
		public Expression Right { get; set; }
		BinaryOperator(string symbol, int precedence, Associativity associativity)
		{
			this.symbol = symbol;
			this.precedence = precedence;
			this.associativity = associativity;
		}
		#region Static Create
		public static BinaryOperator Create(string symbol)
		{
			BinaryOperator result;
			switch (symbol)
			{
				default:
					result = null;
					break;
			// Exponentive
				case "<<":
				case ">>":
					result = new BinaryOperator(symbol, 160, Associativity.None);
					break;
			// Multiplicative
				case "*":
				case "/":
				case "%":
				case "&":
					result = new BinaryOperator(symbol, 150, Associativity.Left);
					break;
			// Additive
				case "+":
				case "-":
				case "|":
				case "^":
					result = new BinaryOperator(symbol, 140, Associativity.Left);
					break;
			// Range
				case "..":
				case "...":
					result = new BinaryOperator(symbol, 135, Associativity.None);
					break;
			// Cast
				case "is":
				case "as":
					result = new BinaryOperator(symbol, 132, Associativity.None);
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
					result = new BinaryOperator(symbol, 130, Associativity.None);
					break;
			// Conjuctive
				case "&&":
					result = new BinaryOperator(symbol, 120, Associativity.Left);
					break;
			// Disjunctive
				case "||":
					result = new BinaryOperator(symbol, 110, Associativity.Left);
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
					result = new BinaryOperator(symbol, 90, Associativity.Right);
					break;
			}
			return result;
		}
		#endregion
	}
}

