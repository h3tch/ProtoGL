﻿using System;
using System.Collections.Generic;

namespace App
{
    /// <summary>
    /// Specialized dictionary to handle OpenGL objects.
    /// </summary>
    /// <typeparam name="T">
    /// Type to be stored in the dictionary. Has to be a GLObject type.</typeparam>
    class Dict<T> : Dictionary<string, T>
        where T : GLObject
    {
        public TResult GetValue<TResult>(string key)
            where TResult : T
        {
            T obj = default(T);
            if (key != null && base.TryGetValue(key, out obj) && obj is TResult)
                return (TResult)obj;
            return default(TResult);
        }

        public bool TryGetValue<TResult>(string key, out TResult obj, CompileException err = null)
            where TResult : T
        {
            // try to find the object instance
            if ((obj = GetValue<TResult>(key)) != null)
                return true;

            // get class name of object type
            var classname = typeof(TResult).Name.Substring(2).ToLower();
            err?.Add($"The name '{key}' could not be found or "
                + $"does not reference an object of type '{classname}'.");
            return false;
        }

        public TResult GetValue<TResult>(string key, string info)
            where TResult : T
        {
            T tmp = default(T);
            if (base.TryGetValue(key, out tmp) && tmp is TResult)
                return (TResult)tmp;
            throw new CompileException(info);
        }

        public bool TryGetValue<TResult>(string key, ref TResult obj)
            where TResult : T
        {
            T tmp;
            if (obj != null || !base.TryGetValue(key, out tmp) || !(tmp is TResult))
                return false;
            obj = (TResult)tmp;
            return true;
        }
    }
}
