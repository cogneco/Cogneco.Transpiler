using System;
using Generic = System.Collections.Generic;
using Collection = Kean.Collection;
using Kean.Extension;
using Kean.Collection.Extension;

namespace Cogneco.Transpiler.Pom.Type
{
	public class Tuple : Expression
	{
		public Collection.IReadOnlyVector<Expression> Items { get; private set; }
		public Tuple(params Expression[] items)
			: this(new Collection.ReadOnlyVector<Expression>(items))
		{
		}
		public Tuple(Generic.IEnumerable<Expression> items)
			: this(new Collection.ReadOnlyVector<Expression>(items.ToArray()))
		{
		}
		public Tuple(Collection.IReadOnlyVector<Expression> items)
		{
			this.Items = items;
		}
	}
}

