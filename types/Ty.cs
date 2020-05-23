using System;
using System.Collections.Generic;

namespace Functional.types
{
    /// <summary>
    /// General class for a yet possibly unresolved type
    /// </summary>
    public class Ty
    {
        public string Name { get; }
        public AstType Type { get { return Tt.Get(Name); } }

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
        }

        /// <summary>
        /// Define a new type and add it to the type table
        /// </summary>
        /// <param name="name">The type name</param>
        /// <param name="type">The actual type</param>
        /// <param name="tt">The type table</param>
        public Ty(string name, AstType type, ref TypeTable tt)
        {
            Name = name;
            Tt = tt;
            Tt.Insert(name, type);
        }

        /// <summary>
        /// Define a new name-agnostic type (like a function type)
        /// </summary>
        /// <param name="type">Type.</param>
        /// <param name="tt">Tt.</param>
        public Ty(AstType type, ref TypeTable tt) : this(type.GetMangledName(), type, ref tt)
        {
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
            if (Type.Is<AndType>() || Type.Is<OrType>())
                return Name;
            return Type.GetMangledName();
        }

        /// <summary>
        /// Returns a string that is a valid type in Functional
        /// For AndTypes and OrTypes returns their definition in usable format
        /// </summary>
        public override string ToString()
        {
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
