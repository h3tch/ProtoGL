﻿using System;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Reflection.BindingFlags;

namespace App.Glsl
{
    public class Converter
    {
        #region Regex

        private static readonly string s = @"\s";
        private static readonly string word = @"\b[\w\d]+\b";
        private static readonly string array = @"\[.*?\]";
        private static readonly string arrayWord = $"{word}\\s*{array}";
        private static Regex rexBuffer = new Regex($"{word}{s}+{word}{s}*\\{{.*?\\}}{s}*{word}(\\[.*?\\])?;", RegexOptions.Singleline);
        private static Regex buffer = new Regex(@"\buniform\s+[\w\d_]+\{[\s\w\d\[\];]*\}[\s\w\d]*;");
        private static Regex outBuffer = new Regex(@"\bout\s+[\w\d_]+\{[\s\w\d\[\];]*\}[\s\w\d]*;");
        private static Regex inBuffer = new Regex(@"\bin\s+[\w\d_]+\{[\s\w\d\[\];]*\}[\s\w\d]*;");
        private static Regex variable = new Regex(@"\b[\w\d_]+\s+[\w\d\[\]_]+;");
        private static Regex rexArrayBraces = new Regex($"{array}\\s*;", RegexOptions.Singleline);
        private static Regex version = new Regex(@"#version [0-9]{3}");
        private static Regex layout = new Regex(@"\blayout\s*\(.*\)");
        private static Regex number = new Regex(@"\b[0-9]*\.[0-9]+\b");
        private static Regex uniform = new Regex(@"\buniform\b");
        private static Regex IN = new Regex(@"\bin\s+[\w\d]+\s+[\w\d]+\s*;");
        private static Regex OUT = new Regex(@"\bout\b");
        private static Regex flat = new Regex(@"\bflat\b");
        private static Regex smooth = new Regex(@"\bsmooth\b");
        private static Regex PreDefOut = new Regex(@"\bout\s+gl_PerVertex\s*\{.*};", RegexOptions.Singleline);
        private static Regex rexMain = new Regex(@"\bvoid\s+main\b");
        private static Regex rexWord = new Regex(word);
        private static Func<string, string, string> typecast = delegate(string text, string type)
        {
            var match = Regex.Matches(text, @"\b" + type + @"\(.*\)");
            for (int i = match.Count - 1; i >= 0; i--)
                text = text.Insert(match[i].Index + type.Length, ")").Insert(match[i].Index, "(");
            return text;
        };

        #endregion

        /// <summary>
        /// Convert GLSL shader to C# code.
        /// </summary>
        /// <param name="text">GLSL shader.</param>
        /// <returns>Return C# code.</returns>
        public static string Shader2Csharp(string text)
        {
            foreach (var method in typeof(Converter).GetMethods(NonPublic | Static))
                text = (string)method.Invoke(null, new[] { text });
            return text;
        }

        #region Methods to Process Shaders

        private static string Version(string text) => version.Replace(text, string.Empty);

        private static string TypeCasts(string text)
        {
            text = typecast(text, "bool");
            text = typecast(text, "int");
            text = typecast(text, "uint");
            text = typecast(text, "float");
            text = typecast(text, "double");
            return text;
        }

        private static string InOutLayouts(string text)
        {
            foreach (var q in new[] { "in", "out" })
            {
                var matches = Regex.Matches(text, @"\blayout\s*\(.*\)\s+" + q + @"\s*;");
                for (int i = matches.Count - 1; i >= 0; i--)
                {
                    Match match = matches[i];
                    var replacement = Regex.Replace(match.Value, $"\\b{q}\\b", $"object __{q}__");
                    text = text.Remove(match.Index, match.Length).Insert(match.Index, replacement);
                }
            }
            return text;
        }

        private static string Layouts(string text)
        {
            for (Match match = layout.Match(text); match.Success; match = layout.Match(text))
                text = text.Insert(match.Index + match.Length, "]").Insert(match.Index, "[__");
            return text;
        }

        private static string Constants(string text)
        {
            const string regex = @"\bconst\s+\w+\s+[\w\d]+\s*=\s*[\w\d.]+;";

            for (Match match = Regex.Match(text, regex); match.Success; match = Regex.Match(text, regex))
            {
                var index = text.IndexOf('=', match.Index);
                text = text
                    .Remove(match.Index + match.Length - 1, 1)
                    .Insert(match.Index + match.Length - 1, "; } }")
                    .Remove(index, 1)
                    .Insert(index, "{ get { return ");
            }

            return Regex.Replace(text, @"\bconst\b", string.Empty);
        }

        private static string Buffers(string text)
        {
            // process buffers
            var bufMatches = rexBuffer.Matches(text);
            for (int i_buf = bufMatches.Count - 1; i_buf >= 0; i_buf--)
            {
                // get buffer definitions
                var bufMatch = bufMatches[i_buf];
                var sub = bufMatch.Value;
                var idx = sub.IndexOf('{');
                var end = sub.IndexOf('}', idx);
                var bufDef = rexWord.Matches(sub);
                var bufType = bufDef[0];
                var bufName = bufDef[1];

                var braces = rexArrayBraces.Match(sub, end);
                string clazz, ctor;
                if (braces.Success)
                {
                    var array = braces.Value;
                    var value = array.Subrange(array.IndexOf('[') + 1, array.IndexOf(']')).Trim();
                    ctor = $"{bufName.Value}[{(value.Length == 0 ? "0" : value)}]";
                    clazz = $"{bufName.Value}[]";
                }
                else
                {
                    ctor = $"{bufName.Value}()";
                    clazz = $"{bufName.Value}";
                }

                // add class constructor to the end of the block
                sub = sub
                    .Insert(sub.Length - 1, $" = new {ctor}");
                if (braces.Success)
                    sub = sub.Remove(braces.Index, braces.Length);
                sub = sub
                    // add class name before instance name
                    .Insert(end + 1, clazz + ' ')
                    // replace type with 'class'
                    .Remove(bufType.Index, bufType.Length)
                    .Insert(bufType.Index, "class");

                // process variable names
                var varMatches = variable.Matches(sub.Substring(0, end), idx);
                for (int i = varMatches.Count - 1; i >= 0; i--)
                {
                    // get variable definitions
                    var varMatch = varMatches[i];
                    // insert 'public' keyword before variable names
                    sub = sub.Insert(varMatch.Index, "public ");
                }

                // commit changes to text
                text = text.Remove(bufMatch.Index, bufMatch.Length).Insert(bufMatch.Index, sub);
                bufMatch = buffer.Match(text, bufMatch.Index + sub.Length);
            }

            return text;
        }

        private static string Arrays(string text)
        {
            // type name [ . ] ;
            // type[] name = new type[ . ];
            var matches = Regex.Matches(text, $"{word}\\s+{arrayWord}\\s*;");
            for (int i = matches.Count - 1; i >= 0; i--)
            {
                // get variable definitions
                var match = matches[i];
                var Def = rexWord.Matches(match.Value);
                var Type = Def[0].Value;
                if (Type == "new")
                    continue;
                var Name = Def[1].Value;
                var braces = rexArrayBraces.Match(text, match.Index, match.Length);
                text = text
                    // replace array braces with return string
                    .Remove(braces.Index, braces.Length)
                    .Insert(braces.Index, $" = new {Type}{braces.Value}")
                    // add array braces to the class type
                    .Insert(match.Index + Type.Length, "[]");
            }

            // type name [ . ] = type [ . ] ( type ( word , word ) , ...);
            // type[] name = new type[] { new type { first = word, second = word }, ... };

            // type name [ . ] = { { word, word }, ... };
            // type[] name = new type[] { new type { first = word, second = word }, ... };

            return text;
        }

        private static string Uniforms(string text) => uniform.Replace(text, string.Empty);

        private static string Discard(string text) => Regex.Replace(text, @"\bdiscard\b", "return");

        private static string AddDebugCode(string text)
        {
            var datatypes = new[] {
                "bool", "int", "uint", "float", "double",
                "bvec2", "ivec2", "uvec2", "vec2", "dvec2",
                "bvec3", "ivec3", "uvec3", "vec3", "dvec3",
                "bvec4", "ivec4", "uvec4", "vec4", "dvec4",
                "mat2", "dmat2", "mat3", "dmat3", "mat4", "dmat4",
                "return", "new"
            };
            datatypes = datatypes.Concat(datatypes.Select(x => x + "[]")).ToArray();

            var s = @"\s*";
            var word = @"\b[\w\d]+\b";
            var pattern = $"{word}{s}({Helpers.RegexMatchingBrace(@"\[",@"\]")})?";
            var rexVar = new Regex($"{pattern}({s}\\.{s}{pattern})*", RegexOptions.RightToLeft);
            var rexExclude = new Regex(@"\s*(=|\*=|/=|\+=|\-=|\+\+|\-\-|\()");
            var rexType = new Regex($"\\b.+?\\b{s}", RegexOptions.RightToLeft);

            var regexFunc = Compiler.RegexFunction;
            double tmp;
            
            var funcs = regexFunc.Matches(text);
            for (var i_func = funcs.Count - 1; i_func >= 0; i_func--)
            {
                Match func = funcs[i_func];
                var start = func.Value.IndexOf('{');
                var str = func.Value.Substring(start);
                var length = str.Length;

                var variables = rexVar.Matches(str);
                foreach (Match variable in rexVar.Matches(str))
                {
                    var varname = variable.Value.Trim();
                    var vartype = rexType.Match(str, 0, variable.Index).Value?.Trim();
                    var invalidChar = rexExclude.Match(str, variable.Index + variable.Length);
                    if (datatypes.Any(x => x == varname || x == vartype)
                        || double.TryParse(varname, out tmp)
                        || (invalidChar.Success && invalidChar.Index == variable.Index + variable.Length))
                        continue;
                    str = str
                        .Insert(variable.Index + varname.Length, $", \"{varname}\")")
                        .Insert(variable.Index, "TraceVariable(");
                }
                
                text = text.Remove(func.Index + start, length).Insert(func.Index + start, str);
            }

            return text;
        }

        private static string Floats(string text)
        {
            var match = number.Matches(text);
            for (int i = match.Count - 1; i >= 0; i--)
                text = text.Insert(match[i].Index + match[i].Length, "f");
            return text;
        }

        private static string Inputs(string text)
        {
            var matches = IN.Matches(text);
            for (int i = matches.Count - 1; i >= 0; i--)
            {
                var match = matches[i];
                var words = rexWord.Matches(match.Value);
                var type = words[1].Value;
                var name = words[2].Value;
                text = text.Remove(match.Index + match.Length - 1, 1).Insert(
                    match.Index + match.Length - 1,
                    $" {{ get {{ return GetInputVarying<{type}>(\"{name}\"); }} }}");
                text = text.Insert(match.Index + 2, "]").Insert(match.Index, "[__");
            }
            return text;
        }

        private static string PredefinedOutputs(string text)
        {
            var matches = PreDefOut.Matches(text);
            for (int i = matches.Count - 1; i >= 0; i--)
            {
                var match = matches[i];
                var nNewLines = match.Value.Count(x => x == '\n');
                text = text.Remove(match.Index, match.Length)
                    .Insert(match.Index, new string('\n', nNewLines));
            }
            return text;
        }

        private static string Outputs(string text) => OUT.Replace(text, "[__out]");

        private static string Flat(string text) => flat.Replace(text, "[__flat]");

        private static string Smooth(string text) => smooth.Replace(text, "[__smooth]");

        private static string MainFunc(string text) => rexMain.Replace(text, "public override void main");

        #endregion

        static class Helpers
        {
            public static string RegexMatchingBrace(string open, string close)
            {
                var oc = $"{open}{close}";
                return  $"{open}[^{oc}]*(((?<Open>{open})[^{oc}]*)+" +
                    $"((?<Close-Open>{close})[^{oc}]*)+)*(?(Open)(?!)){close}";
            }
        }
    }
}