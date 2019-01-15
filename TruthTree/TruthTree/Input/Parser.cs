// This code was generated by the Gardens Point Parser Generator
// Copyright (c) Wayne Kelly, QUT 2005-2010
// (see accompanying GPPGcopyright.rtf)

// GPPG version 1.4.4
// Machine:  KESSHORYU-PWN
// DateTime: 3/3/2011 5:15:46 PM
// UserName: KesshoRyu
// Input file <..\..\Users\KesshoRyu\Documents\Visual Studio 2010\Projects\TruthTree\TruthTree\grammar.y - 3/3/2011 4:58:07 PM>

// options: no-lines gplex

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using QUT.Gppg;

using TruthTree.Logic;

namespace TruthTree.Input
{
    public enum Tokens
    {
        error = 42,
        EOF = 43, ATOM = 44, AND = 45, OR = 46, IFF = 47, IF = 48,
        NOT = 49, FALSE = 50
    };

    public struct ValueType
    {
        public string atom;
        public Sentence sen;
        public SentenceType typ;
    }
    // Abstract base class for GPLEX scanners
    public abstract class ScanBase : AbstractScanner<ValueType, LexLocation>
    {
        private LexLocation __yylloc = new LexLocation();
        public override LexLocation yylloc { get { return __yylloc; } set { __yylloc = value; } }
        protected virtual bool yywrap() { return true; }
    }

    public class Parser : ShiftReduceParser<ValueType, LexLocation>
    {
#pragma warning disable 649
        private static Dictionary<int, string> aliasses;
#pragma warning restore 649
        private static Rule[] rules = new Rule[16];
        private static State[] states = new State[28];
        private static string[] nonTerms = new string[] {
      "sentence", "bcon", "line", "$accept", };

        static Parser()
        {
            states[0] = new State(new int[] { 40, 4, 44, 9, 50, 10, 42, 27, 43, -2 }, new int[] { -3, 1, -1, 3 });
            states[1] = new State(new int[] { 43, 2 });
            states[2] = new State(-1);
            states[3] = new State(-3);
            states[4] = new State(new int[] { 49, 24, 45, 18, 46, 19, 47, 20, 48, 21, 40, 4, 44, 9, 50, 10 }, new int[] { -2, 5, -1, 11 });
            states[5] = new State(new int[] { 40, 4, 44, 9, 50, 10 }, new int[] { -1, 6 });
            states[6] = new State(new int[] { 40, 4, 44, 9, 50, 10 }, new int[] { -1, 7 });
            states[7] = new State(new int[] { 41, 8 });
            states[8] = new State(-5);
            states[9] = new State(-10);
            states[10] = new State(-11);
            states[11] = new State(new int[] { 49, 22, 45, 18, 46, 19, 47, 20, 48, 21, 40, 4, 44, 9, 50, 10 }, new int[] { -2, 12, -1, 15 });
            states[12] = new State(new int[] { 40, 4, 44, 9, 50, 10 }, new int[] { -1, 13 });
            states[13] = new State(new int[] { 41, 14 });
            states[14] = new State(-6);
            states[15] = new State(new int[] { 45, 18, 46, 19, 47, 20, 48, 21 }, new int[] { -2, 16 });
            states[16] = new State(new int[] { 41, 17 });
            states[17] = new State(-7);
            states[18] = new State(-12);
            states[19] = new State(-13);
            states[20] = new State(-14);
            states[21] = new State(-15);
            states[22] = new State(new int[] { 41, 23 });
            states[23] = new State(-9);
            states[24] = new State(new int[] { 40, 4, 44, 9, 50, 10 }, new int[] { -1, 25 });
            states[25] = new State(new int[] { 41, 26 });
            states[26] = new State(-8);
            states[27] = new State(-4);

            for (int sNo = 0; sNo < states.Length; sNo++) states[sNo].number = sNo;

            rules[1] = new Rule(-4, new int[] { -3, 43 });
            rules[2] = new Rule(-3, new int[] { });
            rules[3] = new Rule(-3, new int[] { -1 });
            rules[4] = new Rule(-3, new int[] { 42 });
            rules[5] = new Rule(-1, new int[] { 40, -2, -1, -1, 41 });
            rules[6] = new Rule(-1, new int[] { 40, -1, -2, -1, 41 });
            rules[7] = new Rule(-1, new int[] { 40, -1, -1, -2, 41 });
            rules[8] = new Rule(-1, new int[] { 40, 49, -1, 41 });
            rules[9] = new Rule(-1, new int[] { 40, -1, 49, 41 });
            rules[10] = new Rule(-1, new int[] { 44 });
            rules[11] = new Rule(-1, new int[] { 50 });
            rules[12] = new Rule(-2, new int[] { 45 });
            rules[13] = new Rule(-2, new int[] { 46 });
            rules[14] = new Rule(-2, new int[] { 47 });
            rules[15] = new Rule(-2, new int[] { 48 });
        }

        protected override void Initialize()
        {
            this.InitSpecialTokens((int)Tokens.error, (int)Tokens.EOF);
            this.InitStates(states);
            this.InitRules(rules);
            this.InitNonTerminals(nonTerms);
        }

        protected override void DoAction(int action)
        {
            switch (action)
            {
                case 2: // line -> /* empty */
                    { complete = new Sentence(); }
                    break;
                case 3: // line -> sentence
                    { complete = ValueStack[ValueStack.Depth - 1].sen; }
                    break;
                case 4: // line -> error
                    { complete = null; Console.Error.WriteLine("ERROR"); }
                    break;
                case 5: // sentence -> '(', bcon, sentence, sentence, ')'
                    { CurrentSemanticValue.sen = new Sentence(ValueStack[ValueStack.Depth - 4].typ, ValueStack[ValueStack.Depth - 3].sen, ValueStack[ValueStack.Depth - 2].sen); }
                    break;
                case 6: // sentence -> '(', sentence, bcon, sentence, ')'
                    { CurrentSemanticValue.sen = new Sentence(ValueStack[ValueStack.Depth - 3].typ, ValueStack[ValueStack.Depth - 4].sen, ValueStack[ValueStack.Depth - 2].sen); }
                    break;
                case 7: // sentence -> '(', sentence, sentence, bcon, ')'
                    { CurrentSemanticValue.sen = new Sentence(ValueStack[ValueStack.Depth - 2].typ, ValueStack[ValueStack.Depth - 4].sen, ValueStack[ValueStack.Depth - 3].sen); }
                    break;
                case 8: // sentence -> '(', NOT, sentence, ')'
                    { CurrentSemanticValue.sen = new Sentence(SentenceType.NOT, ValueStack[ValueStack.Depth - 2].sen); }
                    break;
                case 9: // sentence -> '(', sentence, NOT, ')'
                    { CurrentSemanticValue.sen = new Sentence(SentenceType.NOT, ValueStack[ValueStack.Depth - 3].sen); }
                    break;
                case 10: // sentence -> ATOM
                    { CurrentSemanticValue.sen = new Sentence(SentenceType.ATOM, ValueStack[ValueStack.Depth - 1].atom[0]); }
                    break;
                case 11: // sentence -> FALSE
                    { CurrentSemanticValue.sen = new Sentence(SentenceType.FALSE); }
                    break;
                case 12: // bcon -> AND
                    { CurrentSemanticValue.typ = SentenceType.AND; }
                    break;
                case 13: // bcon -> OR
                    { CurrentSemanticValue.typ = SentenceType.OR; }
                    break;
                case 14: // bcon -> IFF
                    { CurrentSemanticValue.typ = SentenceType.IFF; }
                    break;
                case 15: // bcon -> IF
                    { CurrentSemanticValue.typ = SentenceType.IF; }
                    break;
            }
        }

        protected override string TerminalToString(int terminal)
        {
            if (aliasses != null && aliasses.ContainsKey(terminal))
                return aliasses[terminal];
            else if (((Tokens)terminal).ToString() != terminal.ToString(CultureInfo.InvariantCulture))
                return ((Tokens)terminal).ToString();
            else
                return CharToString((char)terminal);
        }


        Parser() : base(null) { }

        private static Sentence complete = null;

        public static Sentence parseString(string str)
        {
            Scanner scanner = new Scanner();
            scanner.SetSource(str, 0);

            Parser parse = new Parser();
            parse.Scanner = scanner;

            parse.Parse();
            return complete;
        }
    }
}