using System;
using System.Collections.Generic;

namespace Functional.types
{
    public abstract class AstType
    {
        public abstract void ResolveNamedTypes(Dictionary<string, AstType> aliases);
        public abstract string GetCName();
        public abstract string GetMangledName();

        // Should be used instead of `is` operator, because it properly handles aliases
        public bool Is<T>()
        {
            if (this is NamedType at)
                return at.InnerType.Is<T>();
            return this is T;
        }

        // Should be used instead of casting, because it properly handles aliases
        public T As<T>() where T: AstType
        {
            if (this is NamedType at)
                return (T)at.InnerType;
            return (T)this;
        }

        public static bool operator ==(AstType t1, AstType t2)
        {
            // unconditional comparisons
            if (t1 is IntType && t2 is IntType)
                return true;
            if (t1 is BoolType && t2 is BoolType)
                return true;
            if (t1 is NilType && t2 is NilType)
                return true;
            if (t1 is StringType && t2 is StringType)
                return true;

            // conditional comparisons
            if (t1 is NamedType alt1 && t2 is NamedType alt2)
                return alt1.Equals(t1);
            if (t1 is AndType at1 && t2 is AndType at2)
                return at1.Equals(at2);
            if (t1 is OrType ot1 && t2 is OrType ot2)
                return ot1.Equals(ot2);
            if (t1 is ListType lt1 && t2 is ListType lt2)
                return lt1.Equals(lt2);
            if (t1 is FunctionType ft1 && t2 is FunctionType ft2)
                return ft1.Equals(ft2);

            return false;
        }

        public static bool operator !=(AstType t1, AstType t2)
            => !(t1 == t2);
    }
}
