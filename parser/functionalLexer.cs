//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.8
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from functional.g4 by ANTLR 4.8

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using System;
using System.IO;
using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.8")]
[System.CLSCompliant(false)]
public partial class functionalLexer : Lexer {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, T__5=6, T__6=7, T__7=8, T__8=9, 
		T__9=10, T__10=11, WS=12, INT=13, ID=14, STR=15, UNTERMINATEDSTR=16, PLUS=17, 
		MINUS=18, TIMES=19, LPAREN=20, RPAREN=21, EQUALS=22, DBLCOLON=23, ARROW=24, 
		AND=25, OR=26, LBRACK=27, RBRACK=28, COMMA=29, JOIN=30, DOT=31, NL=32, 
		COMMENT=33;
	public static string[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] ruleNames = {
		"T__0", "T__1", "T__2", "T__3", "T__4", "T__5", "T__6", "T__7", "T__8", 
		"T__9", "T__10", "WS", "INT", "ID", "STR", "UNTERMINATEDSTR", "PLUS", 
		"MINUS", "TIMES", "LPAREN", "RPAREN", "EQUALS", "DBLCOLON", "ARROW", "AND", 
		"OR", "LBRACK", "RBRACK", "COMMA", "JOIN", "DOT", "NL", "COMMENT"
	};


	public functionalLexer(ICharStream input)
	: this(input, Console.Out, Console.Error) { }

	public functionalLexer(ICharStream input, TextWriter output, TextWriter errorOutput)
	: base(input, output, errorOutput)
	{
		Interpreter = new LexerATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}

	private static readonly string[] _LiteralNames = {
		null, "'module'", "'import'", "'include'", "'external'", "'type'", "'List'", 
		"'where'", "'_'", "'nil'", "'true'", "'false'", null, null, null, null, 
		null, "'+'", "'-'", "'*'", "'('", "')'", "'='", "'::'", "'->'", "'&'", 
		"'|'", "'['", "']'", "','", "':'", "'.'"
	};
	private static readonly string[] _SymbolicNames = {
		null, null, null, null, null, null, null, null, null, null, null, null, 
		"WS", "INT", "ID", "STR", "UNTERMINATEDSTR", "PLUS", "MINUS", "TIMES", 
		"LPAREN", "RPAREN", "EQUALS", "DBLCOLON", "ARROW", "AND", "OR", "LBRACK", 
		"RBRACK", "COMMA", "JOIN", "DOT", "NL", "COMMENT"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "functional.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string[] ChannelNames { get { return channelNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override string SerializedAtn { get { return new string(_serializedATN); } }

	static functionalLexer() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}
	private static char[] _serializedATN = {
		'\x3', '\x608B', '\xA72A', '\x8133', '\xB9ED', '\x417C', '\x3BE7', '\x7786', 
		'\x5964', '\x2', '#', '\xE1', '\b', '\x1', '\x4', '\x2', '\t', '\x2', 
		'\x4', '\x3', '\t', '\x3', '\x4', '\x4', '\t', '\x4', '\x4', '\x5', '\t', 
		'\x5', '\x4', '\x6', '\t', '\x6', '\x4', '\a', '\t', '\a', '\x4', '\b', 
		'\t', '\b', '\x4', '\t', '\t', '\t', '\x4', '\n', '\t', '\n', '\x4', '\v', 
		'\t', '\v', '\x4', '\f', '\t', '\f', '\x4', '\r', '\t', '\r', '\x4', '\xE', 
		'\t', '\xE', '\x4', '\xF', '\t', '\xF', '\x4', '\x10', '\t', '\x10', '\x4', 
		'\x11', '\t', '\x11', '\x4', '\x12', '\t', '\x12', '\x4', '\x13', '\t', 
		'\x13', '\x4', '\x14', '\t', '\x14', '\x4', '\x15', '\t', '\x15', '\x4', 
		'\x16', '\t', '\x16', '\x4', '\x17', '\t', '\x17', '\x4', '\x18', '\t', 
		'\x18', '\x4', '\x19', '\t', '\x19', '\x4', '\x1A', '\t', '\x1A', '\x4', 
		'\x1B', '\t', '\x1B', '\x4', '\x1C', '\t', '\x1C', '\x4', '\x1D', '\t', 
		'\x1D', '\x4', '\x1E', '\t', '\x1E', '\x4', '\x1F', '\t', '\x1F', '\x4', 
		' ', '\t', ' ', '\x4', '!', '\t', '!', '\x4', '\"', '\t', '\"', '\x3', 
		'\x2', '\x3', '\x2', '\x3', '\x2', '\x3', '\x2', '\x3', '\x2', '\x3', 
		'\x2', '\x3', '\x2', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', 
		'\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x4', '\x3', 
		'\x4', '\x3', '\x4', '\x3', '\x4', '\x3', '\x4', '\x3', '\x4', '\x3', 
		'\x4', '\x3', '\x4', '\x3', '\x5', '\x3', '\x5', '\x3', '\x5', '\x3', 
		'\x5', '\x3', '\x5', '\x3', '\x5', '\x3', '\x5', '\x3', '\x5', '\x3', 
		'\x5', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', 
		'\x6', '\x3', '\a', '\x3', '\a', '\x3', '\a', '\x3', '\a', '\x3', '\a', 
		'\x3', '\b', '\x3', '\b', '\x3', '\b', '\x3', '\b', '\x3', '\b', '\x3', 
		'\b', '\x3', '\t', '\x3', '\t', '\x3', '\n', '\x3', '\n', '\x3', '\n', 
		'\x3', '\n', '\x3', '\v', '\x3', '\v', '\x3', '\v', '\x3', '\v', '\x3', 
		'\v', '\x3', '\f', '\x3', '\f', '\x3', '\f', '\x3', '\f', '\x3', '\f', 
		'\x3', '\f', '\x3', '\r', '\x6', '\r', '\x87', '\n', '\r', '\r', '\r', 
		'\xE', '\r', '\x88', '\x3', '\r', '\x3', '\r', '\x3', '\xE', '\x6', '\xE', 
		'\x8E', '\n', '\xE', '\r', '\xE', '\xE', '\xE', '\x8F', '\x3', '\xF', 
		'\x3', '\xF', '\a', '\xF', '\x94', '\n', '\xF', '\f', '\xF', '\xE', '\xF', 
		'\x97', '\v', '\xF', '\x3', '\x10', '\x3', '\x10', '\x3', '\x10', '\x3', 
		'\x11', '\x3', '\x11', '\x3', '\x11', '\x3', '\x11', '\x3', '\x11', '\x5', 
		'\x11', '\xA1', '\n', '\x11', '\a', '\x11', '\xA3', '\n', '\x11', '\f', 
		'\x11', '\xE', '\x11', '\xA6', '\v', '\x11', '\x3', '\x12', '\x3', '\x12', 
		'\x3', '\x13', '\x3', '\x13', '\x3', '\x14', '\x3', '\x14', '\x3', '\x15', 
		'\x3', '\x15', '\x3', '\x16', '\x3', '\x16', '\x3', '\x17', '\x3', '\x17', 
		'\x3', '\x18', '\x3', '\x18', '\x3', '\x18', '\x3', '\x19', '\x3', '\x19', 
		'\x3', '\x19', '\x3', '\x1A', '\x3', '\x1A', '\x3', '\x1B', '\x3', '\x1B', 
		'\x3', '\x1C', '\x3', '\x1C', '\x3', '\x1D', '\x3', '\x1D', '\x3', '\x1E', 
		'\x3', '\x1E', '\x3', '\x1F', '\x3', '\x1F', '\x3', ' ', '\x3', ' ', '\x3', 
		'!', '\x3', '!', '\x3', '!', '\x6', '!', '\xCB', '\n', '!', '\r', '!', 
		'\xE', '!', '\xCC', '\x3', '\"', '\x3', '\"', '\x3', '\"', '\x3', '\"', 
		'\a', '\"', '\xD3', '\n', '\"', '\f', '\"', '\xE', '\"', '\xD6', '\v', 
		'\"', '\x3', '\"', '\x3', '\"', '\x3', '\"', '\a', '\"', '\xDB', '\n', 
		'\"', '\f', '\"', '\xE', '\"', '\xDE', '\v', '\"', '\x3', '\"', '\x3', 
		'\"', '\x2', '\x2', '#', '\x3', '\x3', '\x5', '\x4', '\a', '\x5', '\t', 
		'\x6', '\v', '\a', '\r', '\b', '\xF', '\t', '\x11', '\n', '\x13', '\v', 
		'\x15', '\f', '\x17', '\r', '\x19', '\xE', '\x1B', '\xF', '\x1D', '\x10', 
		'\x1F', '\x11', '!', '\x12', '#', '\x13', '%', '\x14', '\'', '\x15', ')', 
		'\x16', '+', '\x17', '-', '\x18', '/', '\x19', '\x31', '\x1A', '\x33', 
		'\x1B', '\x35', '\x1C', '\x37', '\x1D', '\x39', '\x1E', ';', '\x1F', '=', 
		' ', '?', '!', '\x41', '\"', '\x43', '#', '\x3', '\x2', '\b', '\x4', '\x2', 
		'\v', '\v', '\"', '\"', '\x3', '\x2', '\x32', ';', '\x4', '\x2', '\x43', 
		'\\', '\x63', '|', '\x6', '\x2', '\x32', ';', '\x43', '\\', '\x61', '\x61', 
		'\x63', '|', '\x6', '\x2', '\f', '\f', '\xF', '\xF', '$', '$', '^', '^', 
		'\x4', '\x2', '\f', '\f', '\xF', '\xF', '\x2', '\xEB', '\x2', '\x3', '\x3', 
		'\x2', '\x2', '\x2', '\x2', '\x5', '\x3', '\x2', '\x2', '\x2', '\x2', 
		'\a', '\x3', '\x2', '\x2', '\x2', '\x2', '\t', '\x3', '\x2', '\x2', '\x2', 
		'\x2', '\v', '\x3', '\x2', '\x2', '\x2', '\x2', '\r', '\x3', '\x2', '\x2', 
		'\x2', '\x2', '\xF', '\x3', '\x2', '\x2', '\x2', '\x2', '\x11', '\x3', 
		'\x2', '\x2', '\x2', '\x2', '\x13', '\x3', '\x2', '\x2', '\x2', '\x2', 
		'\x15', '\x3', '\x2', '\x2', '\x2', '\x2', '\x17', '\x3', '\x2', '\x2', 
		'\x2', '\x2', '\x19', '\x3', '\x2', '\x2', '\x2', '\x2', '\x1B', '\x3', 
		'\x2', '\x2', '\x2', '\x2', '\x1D', '\x3', '\x2', '\x2', '\x2', '\x2', 
		'\x1F', '\x3', '\x2', '\x2', '\x2', '\x2', '!', '\x3', '\x2', '\x2', '\x2', 
		'\x2', '#', '\x3', '\x2', '\x2', '\x2', '\x2', '%', '\x3', '\x2', '\x2', 
		'\x2', '\x2', '\'', '\x3', '\x2', '\x2', '\x2', '\x2', ')', '\x3', '\x2', 
		'\x2', '\x2', '\x2', '+', '\x3', '\x2', '\x2', '\x2', '\x2', '-', '\x3', 
		'\x2', '\x2', '\x2', '\x2', '/', '\x3', '\x2', '\x2', '\x2', '\x2', '\x31', 
		'\x3', '\x2', '\x2', '\x2', '\x2', '\x33', '\x3', '\x2', '\x2', '\x2', 
		'\x2', '\x35', '\x3', '\x2', '\x2', '\x2', '\x2', '\x37', '\x3', '\x2', 
		'\x2', '\x2', '\x2', '\x39', '\x3', '\x2', '\x2', '\x2', '\x2', ';', '\x3', 
		'\x2', '\x2', '\x2', '\x2', '=', '\x3', '\x2', '\x2', '\x2', '\x2', '?', 
		'\x3', '\x2', '\x2', '\x2', '\x2', '\x41', '\x3', '\x2', '\x2', '\x2', 
		'\x2', '\x43', '\x3', '\x2', '\x2', '\x2', '\x3', '\x45', '\x3', '\x2', 
		'\x2', '\x2', '\x5', 'L', '\x3', '\x2', '\x2', '\x2', '\a', 'S', '\x3', 
		'\x2', '\x2', '\x2', '\t', '[', '\x3', '\x2', '\x2', '\x2', '\v', '\x64', 
		'\x3', '\x2', '\x2', '\x2', '\r', 'i', '\x3', '\x2', '\x2', '\x2', '\xF', 
		'n', '\x3', '\x2', '\x2', '\x2', '\x11', 't', '\x3', '\x2', '\x2', '\x2', 
		'\x13', 'v', '\x3', '\x2', '\x2', '\x2', '\x15', 'z', '\x3', '\x2', '\x2', 
		'\x2', '\x17', '\x7F', '\x3', '\x2', '\x2', '\x2', '\x19', '\x86', '\x3', 
		'\x2', '\x2', '\x2', '\x1B', '\x8D', '\x3', '\x2', '\x2', '\x2', '\x1D', 
		'\x91', '\x3', '\x2', '\x2', '\x2', '\x1F', '\x98', '\x3', '\x2', '\x2', 
		'\x2', '!', '\x9B', '\x3', '\x2', '\x2', '\x2', '#', '\xA7', '\x3', '\x2', 
		'\x2', '\x2', '%', '\xA9', '\x3', '\x2', '\x2', '\x2', '\'', '\xAB', '\x3', 
		'\x2', '\x2', '\x2', ')', '\xAD', '\x3', '\x2', '\x2', '\x2', '+', '\xAF', 
		'\x3', '\x2', '\x2', '\x2', '-', '\xB1', '\x3', '\x2', '\x2', '\x2', '/', 
		'\xB3', '\x3', '\x2', '\x2', '\x2', '\x31', '\xB6', '\x3', '\x2', '\x2', 
		'\x2', '\x33', '\xB9', '\x3', '\x2', '\x2', '\x2', '\x35', '\xBB', '\x3', 
		'\x2', '\x2', '\x2', '\x37', '\xBD', '\x3', '\x2', '\x2', '\x2', '\x39', 
		'\xBF', '\x3', '\x2', '\x2', '\x2', ';', '\xC1', '\x3', '\x2', '\x2', 
		'\x2', '=', '\xC3', '\x3', '\x2', '\x2', '\x2', '?', '\xC5', '\x3', '\x2', 
		'\x2', '\x2', '\x41', '\xCA', '\x3', '\x2', '\x2', '\x2', '\x43', '\xCE', 
		'\x3', '\x2', '\x2', '\x2', '\x45', '\x46', '\a', 'o', '\x2', '\x2', '\x46', 
		'G', '\a', 'q', '\x2', '\x2', 'G', 'H', '\a', '\x66', '\x2', '\x2', 'H', 
		'I', '\a', 'w', '\x2', '\x2', 'I', 'J', '\a', 'n', '\x2', '\x2', 'J', 
		'K', '\a', 'g', '\x2', '\x2', 'K', '\x4', '\x3', '\x2', '\x2', '\x2', 
		'L', 'M', '\a', 'k', '\x2', '\x2', 'M', 'N', '\a', 'o', '\x2', '\x2', 
		'N', 'O', '\a', 'r', '\x2', '\x2', 'O', 'P', '\a', 'q', '\x2', '\x2', 
		'P', 'Q', '\a', 't', '\x2', '\x2', 'Q', 'R', '\a', 'v', '\x2', '\x2', 
		'R', '\x6', '\x3', '\x2', '\x2', '\x2', 'S', 'T', '\a', 'k', '\x2', '\x2', 
		'T', 'U', '\a', 'p', '\x2', '\x2', 'U', 'V', '\a', '\x65', '\x2', '\x2', 
		'V', 'W', '\a', 'n', '\x2', '\x2', 'W', 'X', '\a', 'w', '\x2', '\x2', 
		'X', 'Y', '\a', '\x66', '\x2', '\x2', 'Y', 'Z', '\a', 'g', '\x2', '\x2', 
		'Z', '\b', '\x3', '\x2', '\x2', '\x2', '[', '\\', '\a', 'g', '\x2', '\x2', 
		'\\', ']', '\a', 'z', '\x2', '\x2', ']', '^', '\a', 'v', '\x2', '\x2', 
		'^', '_', '\a', 'g', '\x2', '\x2', '_', '`', '\a', 't', '\x2', '\x2', 
		'`', '\x61', '\a', 'p', '\x2', '\x2', '\x61', '\x62', '\a', '\x63', '\x2', 
		'\x2', '\x62', '\x63', '\a', 'n', '\x2', '\x2', '\x63', '\n', '\x3', '\x2', 
		'\x2', '\x2', '\x64', '\x65', '\a', 'v', '\x2', '\x2', '\x65', '\x66', 
		'\a', '{', '\x2', '\x2', '\x66', 'g', '\a', 'r', '\x2', '\x2', 'g', 'h', 
		'\a', 'g', '\x2', '\x2', 'h', '\f', '\x3', '\x2', '\x2', '\x2', 'i', 'j', 
		'\a', 'N', '\x2', '\x2', 'j', 'k', '\a', 'k', '\x2', '\x2', 'k', 'l', 
		'\a', 'u', '\x2', '\x2', 'l', 'm', '\a', 'v', '\x2', '\x2', 'm', '\xE', 
		'\x3', '\x2', '\x2', '\x2', 'n', 'o', '\a', 'y', '\x2', '\x2', 'o', 'p', 
		'\a', 'j', '\x2', '\x2', 'p', 'q', '\a', 'g', '\x2', '\x2', 'q', 'r', 
		'\a', 't', '\x2', '\x2', 'r', 's', '\a', 'g', '\x2', '\x2', 's', '\x10', 
		'\x3', '\x2', '\x2', '\x2', 't', 'u', '\a', '\x61', '\x2', '\x2', 'u', 
		'\x12', '\x3', '\x2', '\x2', '\x2', 'v', 'w', '\a', 'p', '\x2', '\x2', 
		'w', 'x', '\a', 'k', '\x2', '\x2', 'x', 'y', '\a', 'n', '\x2', '\x2', 
		'y', '\x14', '\x3', '\x2', '\x2', '\x2', 'z', '{', '\a', 'v', '\x2', '\x2', 
		'{', '|', '\a', 't', '\x2', '\x2', '|', '}', '\a', 'w', '\x2', '\x2', 
		'}', '~', '\a', 'g', '\x2', '\x2', '~', '\x16', '\x3', '\x2', '\x2', '\x2', 
		'\x7F', '\x80', '\a', 'h', '\x2', '\x2', '\x80', '\x81', '\a', '\x63', 
		'\x2', '\x2', '\x81', '\x82', '\a', 'n', '\x2', '\x2', '\x82', '\x83', 
		'\a', 'u', '\x2', '\x2', '\x83', '\x84', '\a', 'g', '\x2', '\x2', '\x84', 
		'\x18', '\x3', '\x2', '\x2', '\x2', '\x85', '\x87', '\t', '\x2', '\x2', 
		'\x2', '\x86', '\x85', '\x3', '\x2', '\x2', '\x2', '\x87', '\x88', '\x3', 
		'\x2', '\x2', '\x2', '\x88', '\x86', '\x3', '\x2', '\x2', '\x2', '\x88', 
		'\x89', '\x3', '\x2', '\x2', '\x2', '\x89', '\x8A', '\x3', '\x2', '\x2', 
		'\x2', '\x8A', '\x8B', '\b', '\r', '\x2', '\x2', '\x8B', '\x1A', '\x3', 
		'\x2', '\x2', '\x2', '\x8C', '\x8E', '\t', '\x3', '\x2', '\x2', '\x8D', 
		'\x8C', '\x3', '\x2', '\x2', '\x2', '\x8E', '\x8F', '\x3', '\x2', '\x2', 
		'\x2', '\x8F', '\x8D', '\x3', '\x2', '\x2', '\x2', '\x8F', '\x90', '\x3', 
		'\x2', '\x2', '\x2', '\x90', '\x1C', '\x3', '\x2', '\x2', '\x2', '\x91', 
		'\x95', '\t', '\x4', '\x2', '\x2', '\x92', '\x94', '\t', '\x5', '\x2', 
		'\x2', '\x93', '\x92', '\x3', '\x2', '\x2', '\x2', '\x94', '\x97', '\x3', 
		'\x2', '\x2', '\x2', '\x95', '\x93', '\x3', '\x2', '\x2', '\x2', '\x95', 
		'\x96', '\x3', '\x2', '\x2', '\x2', '\x96', '\x1E', '\x3', '\x2', '\x2', 
		'\x2', '\x97', '\x95', '\x3', '\x2', '\x2', '\x2', '\x98', '\x99', '\x5', 
		'!', '\x11', '\x2', '\x99', '\x9A', '\a', '$', '\x2', '\x2', '\x9A', ' ', 
		'\x3', '\x2', '\x2', '\x2', '\x9B', '\xA4', '\a', '$', '\x2', '\x2', '\x9C', 
		'\xA3', '\n', '\x6', '\x2', '\x2', '\x9D', '\xA0', '\a', '^', '\x2', '\x2', 
		'\x9E', '\xA1', '\v', '\x2', '\x2', '\x2', '\x9F', '\xA1', '\a', '\x2', 
		'\x2', '\x3', '\xA0', '\x9E', '\x3', '\x2', '\x2', '\x2', '\xA0', '\x9F', 
		'\x3', '\x2', '\x2', '\x2', '\xA1', '\xA3', '\x3', '\x2', '\x2', '\x2', 
		'\xA2', '\x9C', '\x3', '\x2', '\x2', '\x2', '\xA2', '\x9D', '\x3', '\x2', 
		'\x2', '\x2', '\xA3', '\xA6', '\x3', '\x2', '\x2', '\x2', '\xA4', '\xA2', 
		'\x3', '\x2', '\x2', '\x2', '\xA4', '\xA5', '\x3', '\x2', '\x2', '\x2', 
		'\xA5', '\"', '\x3', '\x2', '\x2', '\x2', '\xA6', '\xA4', '\x3', '\x2', 
		'\x2', '\x2', '\xA7', '\xA8', '\a', '-', '\x2', '\x2', '\xA8', '$', '\x3', 
		'\x2', '\x2', '\x2', '\xA9', '\xAA', '\a', '/', '\x2', '\x2', '\xAA', 
		'&', '\x3', '\x2', '\x2', '\x2', '\xAB', '\xAC', '\a', ',', '\x2', '\x2', 
		'\xAC', '(', '\x3', '\x2', '\x2', '\x2', '\xAD', '\xAE', '\a', '*', '\x2', 
		'\x2', '\xAE', '*', '\x3', '\x2', '\x2', '\x2', '\xAF', '\xB0', '\a', 
		'+', '\x2', '\x2', '\xB0', ',', '\x3', '\x2', '\x2', '\x2', '\xB1', '\xB2', 
		'\a', '?', '\x2', '\x2', '\xB2', '.', '\x3', '\x2', '\x2', '\x2', '\xB3', 
		'\xB4', '\a', '<', '\x2', '\x2', '\xB4', '\xB5', '\a', '<', '\x2', '\x2', 
		'\xB5', '\x30', '\x3', '\x2', '\x2', '\x2', '\xB6', '\xB7', '\a', '/', 
		'\x2', '\x2', '\xB7', '\xB8', '\a', '@', '\x2', '\x2', '\xB8', '\x32', 
		'\x3', '\x2', '\x2', '\x2', '\xB9', '\xBA', '\a', '(', '\x2', '\x2', '\xBA', 
		'\x34', '\x3', '\x2', '\x2', '\x2', '\xBB', '\xBC', '\a', '~', '\x2', 
		'\x2', '\xBC', '\x36', '\x3', '\x2', '\x2', '\x2', '\xBD', '\xBE', '\a', 
		']', '\x2', '\x2', '\xBE', '\x38', '\x3', '\x2', '\x2', '\x2', '\xBF', 
		'\xC0', '\a', '_', '\x2', '\x2', '\xC0', ':', '\x3', '\x2', '\x2', '\x2', 
		'\xC1', '\xC2', '\a', '.', '\x2', '\x2', '\xC2', '<', '\x3', '\x2', '\x2', 
		'\x2', '\xC3', '\xC4', '\a', '<', '\x2', '\x2', '\xC4', '>', '\x3', '\x2', 
		'\x2', '\x2', '\xC5', '\xC6', '\a', '\x30', '\x2', '\x2', '\xC6', '@', 
		'\x3', '\x2', '\x2', '\x2', '\xC7', '\xC8', '\a', '\xF', '\x2', '\x2', 
		'\xC8', '\xCB', '\a', '\f', '\x2', '\x2', '\xC9', '\xCB', '\a', '\f', 
		'\x2', '\x2', '\xCA', '\xC7', '\x3', '\x2', '\x2', '\x2', '\xCA', '\xC9', 
		'\x3', '\x2', '\x2', '\x2', '\xCB', '\xCC', '\x3', '\x2', '\x2', '\x2', 
		'\xCC', '\xCA', '\x3', '\x2', '\x2', '\x2', '\xCC', '\xCD', '\x3', '\x2', 
		'\x2', '\x2', '\xCD', '\x42', '\x3', '\x2', '\x2', '\x2', '\xCE', '\xCF', 
		'\a', '\x31', '\x2', '\x2', '\xCF', '\xD0', '\a', '\x31', '\x2', '\x2', 
		'\xD0', '\xD4', '\x3', '\x2', '\x2', '\x2', '\xD1', '\xD3', '\n', '\a', 
		'\x2', '\x2', '\xD2', '\xD1', '\x3', '\x2', '\x2', '\x2', '\xD3', '\xD6', 
		'\x3', '\x2', '\x2', '\x2', '\xD4', '\xD2', '\x3', '\x2', '\x2', '\x2', 
		'\xD4', '\xD5', '\x3', '\x2', '\x2', '\x2', '\xD5', '\xDC', '\x3', '\x2', 
		'\x2', '\x2', '\xD6', '\xD4', '\x3', '\x2', '\x2', '\x2', '\xD7', '\xDB', 
		'\a', '\f', '\x2', '\x2', '\xD8', '\xD9', '\a', '\xF', '\x2', '\x2', '\xD9', 
		'\xDB', '\a', '\f', '\x2', '\x2', '\xDA', '\xD7', '\x3', '\x2', '\x2', 
		'\x2', '\xDA', '\xD8', '\x3', '\x2', '\x2', '\x2', '\xDB', '\xDE', '\x3', 
		'\x2', '\x2', '\x2', '\xDC', '\xDA', '\x3', '\x2', '\x2', '\x2', '\xDC', 
		'\xDD', '\x3', '\x2', '\x2', '\x2', '\xDD', '\xDF', '\x3', '\x2', '\x2', 
		'\x2', '\xDE', '\xDC', '\x3', '\x2', '\x2', '\x2', '\xDF', '\xE0', '\b', 
		'\"', '\x2', '\x2', '\xE0', '\x44', '\x3', '\x2', '\x2', '\x2', '\xE', 
		'\x2', '\x88', '\x8F', '\x95', '\xA0', '\xA2', '\xA4', '\xCA', '\xCC', 
		'\xD4', '\xDA', '\xDC', '\x3', '\b', '\x2', '\x2',
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
