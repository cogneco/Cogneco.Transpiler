using System;

namespace Cogneco.Transpiler.Pom.Type
{
	public class Function 
		: Expression
	{
		public Expression Argument { get; private set; }
		public Expression Result { get; private set; }
		public Function(Expression argument, Expression result)
		{
			this.Argument = argument;
			this.Result = result;
		}
	}
}

