﻿using System;
using System.Linq;

namespace SqlToCSharp.Extensions
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Extension method on 'Type', which returns display name of a 'Type'.
        /// </summary>
        /// <param name="t">Instance of type 'Type'</param>
        /// <returns>Display name of 'Type'.</returns>
        public static string GetDisplayName(this Type t)
        {
            if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
                return string.Format("{0}?", GetDisplayName(t.GetGenericArguments()[0]));
            if (t.IsGenericType)
                return string.Format("{0}<{1}>",
                                     t.Name.Remove(t.Name.IndexOf('`')),
                                     string.Join(",", t.GetGenericArguments().Select(at => at.GetDisplayName())));
            if (t.IsArray)
                return string.Format("{0}[{1}]",
                                     GetDisplayName(t.GetElementType()),
                                     new string(',', t.GetArrayRank() - 1));
            return t.Name;
        }
    }
}
