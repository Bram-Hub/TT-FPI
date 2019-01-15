%namespace TruthTree

%union {
	public string atom;
	public Sentence sen;
	public SentenceType typ;
}

%token <atom> ATOM
%token AND
%token OR
%token IFF
%token IF
%token NOT
%token FALSE

%type <sen> sentence
%type <typ> bcon

%start line

%%

line :				{ complete = new Sentence(); }
	 |	sentence	{ complete = $1; }
	 |	error		{ complete = null; Console.Error.WriteLine("ERROR"); }
	 ;

sentence :	'(' bcon sentence sentence ')'	{ $$ = new Sentence($2, $3, $4); }
		 |	'(' sentence bcon sentence ')'	{ $$ = new Sentence($3, $2, $4); }
		 |	'(' sentence sentence bcon ')'	{ $$ = new Sentence($4, $2, $3); }
		 |	'(' NOT sentence ')'			{ $$ = new Sentence(SentenceType.NOT, $3); }
		 |	'(' sentence NOT ')'			{ $$ = new Sentence(SentenceType.NOT, $2); }
		 |	ATOM							{ $$ = new Sentence(SentenceType.ATOM, $1[0]); }
		 |	FALSE							{ $$ = new Sentence(SentenceType.FALSE); }
		 ;

bcon :	AND	{ $$ = SentenceType.AND; }
	 |	OR	{ $$ = SentenceType.OR; }
	 |	IFF	{ $$ = SentenceType.IFF; }
	 |	IF	{ $$ = SentenceType.IF; }
	 ;

%%

Parser() : base(null) { }

private static Sentence complete = null;

public static Sentence parseString(string str) {
	Scanner scanner = new Scanner();
	scanner.SetSource(str, 0);
	
	Parser parse = new Parser();
	parse.Scanner = scanner;
	
	parse.Parse();
    return complete;
}