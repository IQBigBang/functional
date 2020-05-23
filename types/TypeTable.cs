using System;
using System.Collections.Generic;
using System.Linq;
using Functional.debug;

namespace Functional.types
{
    public class TypeTable
    {
        private Dictionary<string, AstType> table;

        public TypeTable()
        {
            table = new Dictionary<string, AstType>
            {
                // Initialize built-in types
                { "Int", new IntType() },
                { "Bool", new BoolType() },
                { "Nil", new NilType() },
                { "String", new StringType() }
            };
        }

        public AstType Get(string name)
        {
            if (table.TryGetValue(name, out AstType type))
                return type;
            // TODO line count print
            ErrorReporter.Error("Type `{0}` does not exist", name);
            return null;
        }

        public void Insert(string name, AstType type)
        {
            if (table.TryGetValue(name, out AstType definedType))
            {
                if (definedType != type)
                    ErrorReporter.Error("Duplicite type definition. Type {0} defined twice, first as {1} but then as {2}", name, definedType, type);
                return; // if there already is the same type under the same name, just ignore it => uniqueness
            }
            table[name] = type;
        }

        public override string ToString()
        {
            //return "[" + string.Join(", ", table.Select(pair => pair.Key)) + "]";
            return "[" + string.Join(", ", table.Select(pair => pair.Key + ":" + pair.Value)) + "]";
        }

    }
}
