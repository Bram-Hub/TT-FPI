%namespace TruthTree

%%

"and"		{ return (int)Tokens.AND; }
"or"		{ return (int)Tokens.OR; }
"not"		{ return (int)Tokens.NOT; }
"iff"		{ return (int)Tokens.IFF; }
"if"		{ return (int)Tokens.IF; }

"\("		{ return '('; }
"\)"		{ return ')'; }

"false"		{ return (int)Tokens.FALSE; }

[a-zA-Z]+	{ yylval.atom = yytext; return (int)Tokens.ATOM; }

.			{ Console.Error.WriteLine("Error on character: " + yytext); }

%%