﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScintillaNET;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace UnitTests
{
    [TestClass]
    public class UnitTestFXLexer
    {
        private static CodeEditor editor;
        private static FxLexer lexer;
        private static string code;

        public TestContext TestContext { get; set; }

        [ClassInitialize]
        public static void ClassInitialize(TestContext a)
        {
            Theme.DarkTheme();
            code = File.ReadAllText("../../../UnitTests/data/test.tech");
            lexer = new FxLexer("../../../App/resources/keywordsXML.xml");
            editor = new CodeEditor(lexer, code);
        }

        [TestMethod]
        public void TestLexing()
        {
            lexer.Style(editor, 0, editor.TextLength);
        }

        [TestMethod]
        public void TestMaxLexerStyles()
        {
            Assert.AreEqual(Math.Min(lexer.MaxStyle, 128), lexer.MaxStyle);
        }

        [TestMethod]
        public void TestLexer()
        {
            var stylesA = new int[editor.TextLength];
            var stylesB = new int[editor.TextLength];

            lexer.Style(editor, 0, editor.TextLength);
            for (int i = 0; i < editor.TextLength; i++)
                stylesA[i] = editor.GetStyleAt(i);

            lexer.Style(editor, 0, editor.TextLength);
            for (int i = 0; i < editor.TextLength; i++)
                stylesB[i] = editor.GetStyleAt(i);

            Assert.IsTrue(Enumerable.SequenceEqual(stylesA, stylesB));

            lexer.Style(editor, 0, editor.TextLength);
            for (int i = editor.TextLength / 2; i < editor.TextLength; i++)
                stylesB[i] = editor.GetStyleAt(i);

            Assert.IsTrue(Enumerable.SequenceEqual(stylesA, stylesB));
        }

        [TestMethod]
        public void TestMethodFindBufferKeyword()
        {
            var word = "buffer";
            var style = editor.GetStyleAt(editor.Text.IndexOf("buffer buf_pos"));
            var keywords = lexer.SelectKeywords(style, word);
            foreach (var keyword in keywords)
                Assert.IsTrue(keyword.StartsWith(word));
            Assert.AreEqual(1, keywords.Count());
        }

        [TestMethod]
        public void TestMethodFindBufferCommandsKeyword()
        {
            var word = "";
            var style = editor.GetStyleAt(editor.Text.IndexOf("usage staticDraw"));
            var keywords = lexer.SelectKeywords(style, word);
            Assert.AreEqual(4, keywords.Count());

            word = "usa";
            keywords = lexer.SelectKeywords(style, word);
            Assert.AreEqual(1, keywords.Count());
            Assert.AreEqual("usage", keywords.First());

            word = "";
            style = editor.GetStyleAt(editor.Text.IndexOf("staticDraw"));
            keywords = lexer.SelectKeywords(style, word);
            Assert.AreEqual(9, keywords.Count());

            word = "sta";
            keywords = lexer.SelectKeywords(style, word);
            foreach (var keyword in keywords)
                Assert.IsTrue(keyword.StartsWith(word));
            Assert.AreEqual(3, keywords.Count());
        }

        [TestMethod]
        public void TestMethodFindKeywords()
        {
            var style = editor.GetStyleAt(editor.Text.IndexOf("buffer buf_pos"));

            var word = "s";
            var keywords = lexer.SelectKeywords(style, word);
            Assert.AreEqual(2, keywords.Count());

            word = "t";
            keywords = lexer.SelectKeywords(style, word);
            Assert.AreEqual(3, keywords.Count());

            word = "tex";
            keywords = lexer.SelectKeywords(style, word);
            Assert.AreEqual(2, keywords.Count());

            word = "textu";
            keywords = lexer.SelectKeywords(style, word);
            Assert.AreEqual(1, keywords.Count());

            style = 0;
            word = "";
            keywords = lexer.SelectKeywords(style, word);
            Assert.AreEqual(0, keywords.Count());
        }

        [TestMethod]
        public void TestMethodFindBufferHint()
        {
            var word = "buffer";
            var style = editor.GetStyleAt(code.IndexOf(word));
            var hint = lexer.GetKeywordHint(style, word);
            Assert.IsNotNull(hint);
        }
    }
}
