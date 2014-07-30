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
	public class Parser : FrontEnd.Parser<Tokens.Token, Statement>
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
		protected override Generic.IEnumerable<Statement> Parse()
		{
			while (!this.Empty)
				if (this.Current is Tokens.Keyword)
				{
					switch ((this.Current as Tokens.Keyword).Name)
					{
						case Tokens.Keywords.Let:
							yield return this.ParseVariableDeclaration(true);
							break;
						case Tokens.Keywords.Var:
							yield return this.ParseVariableDeclaration(false);
							break;
						default:
							this.Next();
							break;
					}
				}
				else
					this.Next();
		}
		protected VariableDeclaration ParseVariableDeclaration(bool constant)
		{
			var result = new VariableDeclaration(constant) { Region = this.Current.Region };
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
			var current = this.Current as Tokens.PostfixOperator;
			if (this.Next().IsNull())
				new Exception.SyntaxError("type expression following postfix \":\"", "nothing", current.Region);
			Type result = null;
			if (this.Current is Tokens.Identifier)
			{
				result = new TypeIdentifier((this.Current as Tokens.Identifier).Name) { Region = this.Current.Region };
				this.Next();
			}
			return result;
		}
		protected Expression ParseExpression()
		{
			return this.ParseExpression(0, new Collection.Stack<Expression>());
		}
		protected Expression ParseExpression(int precedence, Collection.IStack<Expression> stack)
		{
			Expression result = null;
			Tokens.Token current = this.Current;
			if (current is Tokens.Literal)
			{
				result = Literal.Create(current as Tokens.Literal);
				this.Next();
			}
			else if (current is Tokens.Identifier)
				result = ParseIdentifier();
			else if (current is Tokens.BinaryOperator)
			{
				BinaryOperator o = BinaryOperator.Create(current as Tokens.BinaryOperator);
				if (o.NotNull() && precedence < o.Precedence)
				{
					if ((o.Left = stack.Pop()).IsNull())
						new Exception.SyntaxError("left hand expression of binary operator \"" + o.Symbol + "\"", "nothing", o.Region).Throw();
					if (this.Next().IsNull() || (o.Right = this.ParseExpression(o.Associativity == Associativity.Right ? o.Precedence + 1 : o.Precedence, stack)).IsNull())
						new Exception.SyntaxError("right expression of binary operator \"" + o.Symbol + "\"", "nothing", o.Region).Throw();
					result = o;
				}
			}
			else if (current is Tokens.LeftParenthesis)
			{
				if (this.Next().IsNull() || (result = this.ParseExpression()).IsNull())
					new Exception.SyntaxError("matching end parenthesis", "nothing", current.Region).Throw();
				if (!(this.Current is Tokens.RightParenthesis))
					new Exception.SyntaxError("matching end parenthesis", "\"" + this.Current.Raw + "\"", this.Current.Region).Throw();
				this.Next();
			}
			if (result.NotNull())
			{
				result = this.ParseExpression(precedence, stack.Push(result)) ?? stack.Pop();
				if (this.Current.Is<Tokens.PostfixOperator>(t => t.Name == ":"))
					result.AssignedType = this.ParseType();
			}
			return result;
		}
		protected Identifier ParseIdentifier()
		{
			Tokens.Identifier current = this.Current as Tokens.Identifier;
			this.Next();
			Identifier result = null;
			if (this.Current is Tokens.LeftParenthesis)
				;// function call do ParseTupleExpression
			else if (this.Current is Tokens.BinaryOperator && (this.Current as Tokens.BinaryOperator).Name == ".")
				;// parent resolve this with recursivecall
			else
				result = new Identifier(current.Name) { Region = current.Region };
			return result;
		}
	}
}

