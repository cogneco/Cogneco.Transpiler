using System;

namespace Cogneco.Transpiler.Pom
{
	public class FunctionExpression 
		: Expression
	{
		public FunctionDefinition Definition { get; private set; }
		public FunctionExpression(FunctionDefinition definition, Type.Expression type)
			: base(type)
		{
			this.Definition = definition;
		}
	}
}

