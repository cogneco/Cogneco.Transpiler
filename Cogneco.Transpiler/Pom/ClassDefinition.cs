using System;
using Collection = Kean.Collection;

namespace Cogneco.Transpiler.Pom
{
	public class ClassDefinition 
		: Definition
	{
		public Collection.ReadOnlyVector<FunctionDefinition> Methods { get; private set; }
		public ClassDefinition(Definition parent, string name, Type.Expression type)
			: base(parent, name, type)
		{
		}
	}
}

