using System;
using Kean;
using Kean.Extension;
using Generic = System.Collections.Generic;
using IO = Kean.IO;
using Uri = Kean.Uri;
using Text = Kean.IO.Text;
using Kean.IO.Extension;
using Kean.Collection.Extension;

namespace Cogneco.Transpiler.Ooc.Tokens
{
	public class Identifier : Token
	{
		public readonly string Name;
		public Identifier(string name, string raw, Uri.Region region) : base(raw, region)
		{
			this.Name = name;
		}
		public static Token Parse(string data, Uri.Region region)
		{
			Token result;
			switch (data)
			{
			// Identifier
				default:
					result = new Identifier(data, data, region);
					break;
			// Keywords used in Declarations
				case "class":
					result = new Keyword(Keywords.Class, data, region);
					break;
				case "deinit":
					result = new Keyword(Keywords.Deinit, data, region);
					break;
				case "enum":
					result = new Keyword(Keywords.Enum, data, region);
					break;
				case "extension":
					result = new Keyword(Keywords.Extension, data, region);
					break;
				case "func":
					result = new Keyword(Keywords.Func, data, region);
					break;
				case "import":
					result = new Keyword(Keywords.Import, data, region);
					break;
				case "init":
					result = new Keyword(Keywords.Init, data, region);
					break;
				case "let":
					result = new Keyword(Keywords.Let, data, region);
					break;
				case "interface":
					result = new Keyword(Keywords.Interface, data, region);
					break;
				case "static":
					result = new Keyword(Keywords.Static, data, region);
					break;
				case "struct":
					result = new Keyword(Keywords.Struct, data, region);
					break;
				case "subscript":
					result = new Keyword(Keywords.Subscript, data, region);
					break;
				case "typealias":
					result = new Keyword(Keywords.TypeAlias, data, region);
					break;
				case "var":
					result = new Keyword(Keywords.Var, data, region);
					break;
			// Keywords used in Statements
				case "break":
					result = new Keyword(Keywords.Break, data, region);
					break;
				case "case":
					result = new Keyword(Keywords.Case, data, region);
					break;
				case "continue":
					result = new Keyword(Keywords.Continue, data, region);
					break;
				case "default":
					result = new Keyword(Keywords.Default, data, region);
					break;
				case "do":
					result = new Keyword(Keywords.Do, data, region);
					break;
				case "else":
					result = new Keyword(Keywords.Else, data, region);
					break;
				case "fallthrough":
					result = new Keyword(Keywords.Fallthrough, data, region);
					break;
				case "if":
					result = new Keyword(Keywords.If, data, region);
					break;
				case "in":
					result = new Keyword(Keywords.In, data, region);
					break;
				case "for":
					result = new Keyword(Keywords.For, data, region);
					break;
				case "return":
					result = new Keyword(Keywords.Return, data, region);
					break;
				case "switch":
					result = new Keyword(Keywords.Switch, data, region);
					break;
				case "where":
					result = new Keyword(Keywords.Where, data, region);
					break;
				case "while":
					result = new Keyword(Keywords.While, data, region);
					break;
			// Keywords used in Expressions and Types
				case "as":
					result = new Keyword(Keywords.As, data, region);
					break;
				case "dynamicType":
					result = new Keyword(Keywords.DynamicType, data, region);
					break;
				case "is":
					result = new Keyword(Keywords.Is, data, region);
					break;
//				case "new":
//					result = new Keyword(Keywords.New, data, region);
//					break;
//				case "base":
//					result = new Keyword(Keywords.Super, data, region);
//					break;
//				case "this":
//					result = new Keyword(Keywords.This, data, region);
//					break;
//				case "This":
//					result = new Keyword(Keywords.StaticThis, data, region);
//					break;
				case "type":
					result = new Keyword(Keywords.Type, data, region);
					break;
				case "__COLUMN__":
					result = new Keyword(Keywords.Column, data, region);
					break;
				case "__FILE__":
					result = new Keyword(Keywords.File, data, region);
					break;
				case "__FUNCTION__":
					result = new Keyword(Keywords.Function, data, region);
					break;
				case "__LINE__":
					result = new Keyword(Keywords.Line, data, region);
					break;
			// Keywords reserved in particular contexts
				case "associativity":
					result = new Keyword(Keywords.Associativity, data, region);
					break;
				case "didSet":
					result = new Keyword(Keywords.DidSet, data, region);
					break;
				case "get":
					result = new Keyword(Keywords.Get, data, region);
					break;
				case "infix":
					result = new Keyword(Keywords.Infix, data, region);
					break;
				case "inout":
					result = new Keyword(Keywords.InOut, data, region);
					break;
				case "left":
					result = new Keyword(Keywords.Left, data, region);
					break;
				case "mutating":
					result = new Keyword(Keywords.Mutating, data, region);
					break;
				case "none":
					result = new Keyword(Keywords.None, data, region);
					break;
				case "nonmutating":
					result = new Keyword(Keywords.NonMutating, data, region);
					break;
				case "operator":
					result = new Keyword(Keywords.Operator, data, region);
					break;
				case "override":
					result = new Keyword(Keywords.Override, data, region);
					break;
				case "postfix":
					result = new Keyword(Keywords.Postfix, data, region);
					break;
				case "precedence":
					result = new Keyword(Keywords.Precedence, data, region);
					break;
				case "prefix":
					result = new Keyword(Keywords.Prefix, data, region);
					break;
				case "right":
					result = new Keyword(Keywords.Right, data, region);
					break;
				case "set":
					result = new Keyword(Keywords.Set, data, region);
					break;
				case "willSet":
					result = new Keyword(Keywords.WillSet, data, region);
					break;
			// Literals
				case "true":
					result = new BooleanLiteral(true, region);
					break;
				case "false":
					result = new BooleanLiteral(false, region);
					break;
				case "null":
					result = new NullLiteral(region);
					break;
			}
			return result;
		}
	}
}

