//
//  Tokenizer.cs
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
using Kean;
using Kean.Extension;
using Generic = System.Collections.Generic;
using IO = Kean.IO;
using Uri = Kean.Uri;
using Text = Kean.IO.Text;
using Kean.IO.Extension;
using Kean.Collection.Extension;
using Error = Kean.Error;

namespace Cogneco.Transpiler.Apus.Tokens
{
	class Tokenizer : Generic.IEnumerable<Token>, IDisposable
	{
		IO.ICharacterReader reader;
		public Uri.Locator Resource { get { return this.reader.Resource; } }
		public Uri.Position Position { get { return new IO.Text.Position(this.reader); } }
		Tokenizer(IO.ICharacterReader reader)
		{
			this.reader = reader;
		}
		Text.Mark Mark()
		{
			return new Text.Mark(this.reader);
		}
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
		public System.Collections.Generic.IEnumerator<Token> GetEnumerator()
		{
			this.reader.Next();
			Token last = null;
			bool empty = false;
			while (!empty)
			{
				empty = this.reader.Empty;
				IO.Text.Mark mark = this.Mark();
				if (this.IsWhiteSpace(this.reader.Last)) // White Space
					yield return last = new WhiteSpace(this.reader.ReadFromCurrentUntil(c => !this.IsWhiteSpace(c)), mark);
				else if (this.IsSeparator(this.reader.Last)) // White Space
				{
					yield return last = Separator.Parse(this.reader.Last, mark);
					empty = !this.reader.Next();
				}
				else if (this.IsOperator(this.reader.Last))
				{
					string r = this.reader.ReadFromCurrentUntil(c => !this.IsOperator(c));
					if (r == "//")
					{
						yield return new Comment(r + this.reader.ReadFromCurrentUntil(c => (c == '\n' || c == '\r')), mark);
						empty = !this.reader.Next();
					}
					else if (r == "/*")
					{
						int depth = 0;
						char previous = '\0';
						yield return new Comment(r + this.reader.ReadFromCurrentUntil(c =>
						{
							if (previous == '/' && c == '*')
								depth++;
							return previous == '*' && (previous = c) == '/' && depth-- == 0;
						}) + "/", mark);
						empty = !this.reader.Next();
					}
					else
						yield return last = 
							last is WhiteSpace && !(this.IsSeparator(this.reader.Last) || this.IsWhiteSpace(this.reader.Last)) ? (Operator)new PrefixOperator(r, mark) :
							!(last is WhiteSpace) && (this.IsSeparator(this.reader.Last) || this.IsWhiteSpace(this.reader.Last) || this.reader.Last == '.') ? (Operator)new PostfixOperator(r, mark) :
							new BinaryOperator(r, mark);
				}
				else if (this.StartsNumber(this.reader.Last))
				{
					bool floatingPoint = false;
					string r = this.reader.ReadFromCurrentUntil(c => !((floatingPoint = c == '.') || this.IsWithinNumber(c)));
					yield return last = floatingPoint ? (Literal)FloatingPointLiteral.Parse(r, mark) : IntegerLiteral.Parse(r, mark);
				}
				else if (this.StartsIdentifier(this.reader.Last)) // Keyword, Identifier, Boolean Literal or Null Literal
					yield return last = Identifier.Parse(this.reader.ReadFromCurrentUntil(c => !this.IsWithinIdentifier(c)), mark);
				else
					new Exception.LexicalError("a valid token", "invalid character (\"" + this.reader.Last + "\" " + ((int)this.reader.Last).ToString("x") + ")", mark).Throw();
			}
		}
		bool IsWhiteSpace(char c)
		{
			return char.IsWhiteSpace(c);
		}
		bool IsSeparator(char c)
		{
			return c == '(' || c == '[' || c == '{' || c == ')' || c == ']' || c == '}' || c == ',' || c == ';';
		}
		bool IsOperator(char c)
		{
			return c == '/' || c == '=' || c == '-' || c == '+' || c == '!' || c == '*' || c == '%' || c == '<' || c == '>' || c == '&' || c == '|' || c == '^' || c == '~' || c == '.' || c == '?' || c == ':';
		}
		bool StartsNumber(char c)
		{
			return char.IsDigit(c);
		}
		bool IsWithinNumber(char c)
		{
			return char.IsLetterOrDigit(c) || c == '_' || c == '.';
		}
		bool StartsIdentifier(char c)
		{
			return char.IsLetter(c) || c == '_';
		}
		bool IsWithinIdentifier(char c)
		{
			return char.IsLetterOrDigit(c) || c == '_';
		}
		public bool Close()
		{
			bool result = this.reader.NotNull();
			if (result && (result = this.reader.Close()))
				this.reader = null;
			return result;
		}
		#region IDisposable implementation
		~Tokenizer ()
		{
			Error.Log.Call(() => this.Close());
		}
		void IDisposable.Dispose()
		{
			this.Close();
		}
		#endregion
		public static Tokenizer Open(IO.ICharacterReader reader)
		{
			return reader.NotNull() ? new Tokenizer(reader) : null;
		}
	}
}

