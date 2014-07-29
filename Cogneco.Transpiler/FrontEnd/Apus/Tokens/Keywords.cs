using System;

namespace Cogneco.Transpiler.FrontEnd.Apus.Tokens
{
	public enum Keywords
	{
		// Keywords used in Declarations
		Class,
		Deinit,
		Enum,
		Extension,
		Func,
		Import,
		Init,
		Let,
		Interface,
		Static,
		Struct,
		Subscript,
		TypeAlias,
		Var,
		// Keywords used in Statements
		Break,
		Case,
		Continue,
		Default,
		Do,
		Else,
		Fallthrough,
		If,
		In,
		For,
		Return,
		Switch,
		Where,
		While,
		// Keywords used in Expressions and Types
		As,
		DynamicType,
		Is,
		New,
		Super,
		This,
		StaticThis,
		Type,
		Column,
		File,
		Function,
		Line,
		// Keywords reserved in particular contexts
		Associativity,
		DidSet,
		Get,
		Infix,
		InOut,
		Left,
		Mutating,
		None,
		NonMutating,
		Operator,
		Override,
		Postfix,
		Precedence,
		Prefix,
		Right,
		Set,
		WillSet
	}
}

