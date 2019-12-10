﻿using System;

namespace Galaxy.Teams.Presentation.Ioc
{
    public static class TypeExtensions
    {
        public static bool IsNotAbstractNorNested(this Type t)
        {
            return !t.IsAbstract && !t.IsNested;
        }

        public static bool IsPathValid(this Type t, string path)
        {
            return !string.IsNullOrEmpty(t.Namespace) &&
                   t.Namespace.StartsWith(path, StringComparison.InvariantCulture);
        }
    }
}