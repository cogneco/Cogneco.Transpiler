//
//  Parser.cs
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
using Generic = System.Collections.Generic;
using Uri = Kean.Uri;
using IO = Kean.IO;
using Collection = Kean.Collection;

namespace Cogneco.Transpiler.FrontEnd.Apus.SyntaxTree
{
	public class Parser : FrontEnd.Parser<Tokens.Token, Module>
	{
		public Parser()
		{
		}
		public override Tokens.Token Next()
		{
			Tokens.Token result = base.Next();
			if (result is Tokens.WhiteSpace || result is Tokens.Comment)
				result = this.Next();
			return result;
		}
		protected override Generic.IEnumerable<Tokens.Token> OpenLexer(IO.ICharacterReader reader)
		{
			return Tokens.Lexer.Open(reader);
		}
		protected override void Parse()
		{
			//while (!this.Empty)
			if (this.Current is Tokens.Keyword)
			{
				switch ((this.Current as Tokens.Keyword).Name)
				{
					case Tokens.Keywords.Var:
						this.Result.Declarations.Add(this.ParseVariableDeclaration());
						break;
				}
			}
		}
		protected VariableDeclaration ParseVariableDeclaration()
		{
			var result = new VariableDeclaration() { Region = this.Current.Region };
			this.Next();
			result.Pattern = this.ParsePattern();
			if (!this.Current.Is<Tokens.BinaryOperator>(t => t.Name == "="))
				new Exception.SyntaxError("binary operator \"=\"", this.Current.Raw, this.Current.Region).Throw();
			this.Next();
			result.Expression = this.ParseExpression();
			return result;
		}
		protected Pattern ParsePattern()
		{
			Pattern result;
			if (this.Current is Tokens.Identifier)
				result = new IdentifierPattern(this.Current as Tokens.Identifier);
//			else if (this.Current is Tokens.LeftParenthesis)
//			{
//				// FIXME: Parse TuplePattern
//			}
			else
			{
				new Exception.SyntaxError("Identifier, Tuple Pattern or Wildcard", this.Current.Raw, this.Current.Region).Throw();
				result = null;
			}
			if (this.Next().Is<Tokens.PostfixOperator>(t => t.Name == ":"))
				result.AssignedType = this.ParseType();
			return result;
		}
		protected Type ParseType()
		{
			return null;
		}
		protected Expression ParseExpression()
		{
			return this.ParseExpression(0, new Collection.Stack<Expression>());
		}
		protected Expression ParseExpression(int precedence, Collection.IStack<Expression> stack)
		{
			Expression result = null;
			if (this.Current is Tokens.Literal)
				result = Literal.Create(this.Current as Tokens.Literal);
//			else if (this.Current is Tokens.Identifier)
//				result =
			else if (this.Current is Tokens.BinaryOperator)
			{
				BinaryOperator o = BinaryOperator.Create((this.Current as Tokens.BinaryOperator).Name);
				if (o.NotNull() && precedence > o.Precedence)
				{
					o.Left = stack.Pop();
					o.Right = this.ParseExpression(o.Associativity == Associativity.Right ? o.Precedence + 1 : o.Precedence, stack);
					result = o;
				}
			}
			if (result.NotNull())
			{
				this.Next();
				result = ParseExpression(result.Precedence, stack.Push(result)) ?? stack.Pop();
				if (this.Current.Is<Tokens.PostfixOperator>(t => t.Name == ":"))
					result.AssignedType = this.ParseType();
			}
			return result;
		}
	}
}

