﻿using System;
using System.Collections.Generic;

namespace App
{
    class GLException : Exception
    {
        private List<string> callstack;
        private List<string> messages = new List<string>();

        // compile call stack into a single string
        private string callstackstring
        {
            get
            {
                string str = "";
                foreach (var s in callstack)
                    str += s + ": ";
                return str;
            }
        }

        // compile all messages into a single string
        private string messagestring
        {
            get
            {
                string str = "";
                foreach (var msg in messages)
                    str += msg + '\n';
                return str;
            }
        }

        public string Text => messagestring;

        public GLException()
            : this(new List<string>())
        {
        }

        private GLException(List<string> callstack)
            : base()
        {
            this.callstack = callstack;
        }

        private GLException(List<string> callstack, string callstackstring)
            : this(callstack)
        {
            callstack.Add(callstackstring);
        }

        public GLException(string callstackstring)
            : this()
        {
            callstack.Add(callstackstring);
        }

        public GLException(GLException err, string callstackstring)
            : this(err.callstack, callstackstring)
        {
        }

        public void Add(string message)
        {
            messages.Add(callstackstring + message);
        }

        public void PushCall(string text)
        {
            callstack.Add(text);
        }

        public static GLException operator +(GLException err, string callLevel)
        {
            return new GLException(err, callLevel);
        }

        public void PopCall()
        {
            if (callstack.Count > 0)
                callstack.RemoveAt(callstack.Count - 1);
        }

        public bool HasErrors()
        {
            return messages.Count > 0;
        }

        public void Throw(string message)
        {
            Add(message);
            throw this;
        }
    }
}