using System;
using System.Collections.Generic;

namespace Functional.types
{
    [Serializable]
    public abstract class AstType
    { 

        public abstract string GetCName();
        public abstract string GetMangledName();
        public abstract bool TryMonomorphize(AstType ExpectedType, ref Dictionary<string, Ty> TypeArgs, string[] ExpectedTypeArgs);
        public abstract AstType Monomorphize(Dictionary<string, Ty> TypeArgs);

        // Historically, those methods are used instead of `is` and `as`
        // However right now they don't offer any special logic
        public bool Is<T>()
            => this is T;

        public T As<T>() where T : AstType
            => (T)this;

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
