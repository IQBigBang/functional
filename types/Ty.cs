using System;
using System.Collections.Generic;
using System.Linq;
using Functional.engines;

namespace Functional.types
{
    /// <summary>
    /// General class for a yet possibly unresolved type
    /// </summary>
    public class Ty
    {
        public string Name { get; }

        private bool IsAutoNamed;

        public AstType Type { get { return Tt.Get(Name); } }

        public AstType MaybeType { get { return Tt.TryGet(Name); } }

        private TypeTable Tt;

        /// <summary>
        /// Get an already defined type by name
        /// </summary>
        /// <param name="name">The type name</param>
        /// <param name="tt">The type table</param>
        public Ty(string name, ref TypeTable tt)
        {
            Name = name;
            Tt = tt;
            IsAutoNamed = false;
        }

        /// <summary>
        /// Define a new type and add it to the type table
        /// </summary>
        /// <param name="name">The type name</param>
        /// <param name="type">The actual type</param>
        /// <param name="tt">The type table</param>
        public Ty(string name, AstType type, ref TypeTable tt, string fileAndLine)
        {
            Name = name;
            Tt = tt;
            IsAutoNamed = false;
            Tt.Insert(name, type, fileAndLine);
        }

        /// <summary>
        /// Define a new name-agnostic type (like a function type)
        /// </summary>
        /// <param name="type">Type.</param>
        /// <param name="tt">Tt.</param>
        // FileAndLine is passed as an empty string, because it is used to throw duplicite type definition error and that can't happen with auto-named types
        public Ty(AstType type, ref TypeTable tt) : this(type.GetMangledName(), type, ref tt, "")
        {
            IsAutoNamed = true;
        }

        public string GetCName()
        {
            // AndTypes and OrTypes must always have names
            if (Type.Is<AndType>() || Type.Is<OrType>())
                return Name +"_t*";
            return Type.GetCName();
        }

        public string GetMangledName()
        {
            if (MaybeType is null) return Name; // This is for generic types
            if (Type.Is<AndType>() || Type.Is<OrType>())
                return Name;
            return Type.GetMangledName();
        }

        /// <summary>
        /// Checks if it is possible to monomorphize this type
        /// </summary>
        /// <returns><c>true</c>, if monomorphize was tryed, <c>false</c> otherwise.</returns>
        /// <param name="ExpectedType">The type that this type is supposed to be after monomorphization</param>
        /// <param name="TypeArgs">A list of already resolved type arguments.</param>
        /// <param name="ExpectedTypeArgs">A list of all the expected type arguments.</param>
        public bool TryMonomorphize(Ty ExpectedType, ref Dictionary<string, Ty> TypeArgs, string[] ExpectedTypeArgs)
        {
            // if the type is already resolved, check for equality
            if (TypeArgs.ContainsKey(this.Name))
                return TypeArgs[this.Name] == ExpectedType;
            // if it is expected to be resolved, do it
            if (ExpectedTypeArgs.Contains(this.Name))
            {
                TypeArgs[this.Name] = ExpectedType;
                return true;
            }
            // otherwise check the inner type
            return this.Type.TryMonomorphize(ExpectedType.Type, ref TypeArgs, ExpectedTypeArgs);
        }

        /// <summary>
        /// Actually monomorphizes the type. The returned type should not contain anything generic.
        /// </summary>
        /// <param name="TypeArgs">The resolved type arguments</param>
        public Ty Monomorphize(Dictionary<string, Ty> TypeArgs)
        {
            if (TypeArgs.ContainsKey(this.Name))
            {
                Console.WriteLine("Substituting generic {0} for {1}", Name, TypeArgs[this.Name]);
                return TypeArgs[this.Name];
            }
            Console.WriteLine("Passing down monomorphization from {0}", Name);

            if (IsAutoNamed) return new Ty(this.Type.Monomorphize(TypeArgs), ref Tt);
            return new Ty(Name, Type.Monomorphize(TypeArgs), ref Tt, "");
        }

        public override string ToString()
        {
            if (MaybeType is null) return Name; // This is for generic types
            if (Type.Is<AndType>() || Type.Is<OrType>())
                return Name;
            return Type.ToString();
        }

        public static explicit operator AstType(Ty ty)
        {
            return ty.Type;
        }

        public static bool operator==(Ty ty1, Ty ty2)
        {
            // Types with same names are ALWAYS equal
            if (ty1.Name == ty2.Name)
                return true;
            return ty1.Type == ty2.Type; // But aliases are also possible
        }

        public static bool operator!=(Ty ty1, Ty ty2)
        {
            if (ty1.Name == ty2.Name)
                return false;
            return ty1.Type != ty2.Type;
        }

        public override bool Equals(object obj)
        {
            if (obj is Ty ty)
                return this == ty;
            return false;
        }
    }
}
