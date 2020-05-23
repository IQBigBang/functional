grammar functional;

WS: [ \t]+ -> skip;

INT: [0-9]+;
ID: [a-zA-Z][a-zA-Z0-9_]*; // IDs cannot start with an underscore
STR: UNTERMINATEDSTR '"'; // This is just copied from StackOverflow
UNTERMINATEDSTR: '"' (~["\\\r\n] | '\\' (. | EOF))*;

PLUS: '+';
MINUS: '-';
TIMES: '*';
LPAREN: '(';
RPAREN: ')';
EQUALS: '=';
DBLCOLON: '::';
ARROW: '->';
AND: '&';
OR: '|';
LBRACK: '[';
RBRACK: ']';
COMMA: ',';
JOIN: ':';
DOT: '.';
NL: ('\r\n'|'\n')+;


program
    : NL? definition* EOF
    ;
    
definition
    : 'module' (ID DOT)* ID NL # ModuleDefinition
    | 'import' (ID DOT)* ID NL # ImportDefinition
    | 'include' STR NL # IncludeDefinition
    | 'external' predicate NL # ExternalFunctionDefinition
    | predicate NL (stmt NL)* stmt NL? # FunctionDefinition
    | 'type' ID EQUALS definitiontypename NL # TypeDefinition
    ;
    
predicate
    : ID DBLCOLON anontypename
    ;
    
anontypename
    : (simpleanontypename ARROW)+ simpleanontypename # CompositeAnontypename
    | simpleanontypename # SimpleanontypenameAnontypename
    ;
    
simpleanontypename
    : 'List' simpleanontypename # ListSimpleanontypename
    | ID # NamedSimpleanontypename
    | LPAREN anontypename RPAREN # AnontypenameSimpleanontypename
    ;
    
definitiontypename
    : LPAREN (definitionsimpletypename AND)+ definitionsimpletypename RPAREN # AndtypeDefinitiontypename
    | LPAREN (ID definitionsimpletypename OR)+ ID definitionsimpletypename RPAREN # OrtypeDefinitiontypename
    ;
    
definitionsimpletypename
    : 'List' definitionsimpletypename # ListDefinitionsimpletypename
    | ID # NamedDefinitionsimpletypename
    ;
    
stmt
    : ID patt* EQUALS expr NL? whereclause # FuncDefStmt
    ;
    
whereclause
    : 'where' (bindpatt EQUALS expr NL)+ # WhereClause
    | # NoWhereClause 
    ;
    
patt
    : ID # BindPattern
    | '_' # DiscardPattern
    | LPAREN (patt AND)+ patt RPAREN # AndTypePattern
    | LPAREN ID patt RPAREN # OrTypePattern
    | LBRACK RBRACK # EmptyListPattern
    | LPAREN (patt JOIN)+ patt RPAREN # ListPattern
    | INT # ConstIntPattern
    ;
    
bindpatt // Used in the 'where' clause where only binding patterns can be used (not testing patterns like a constant value)
    : ID # BindBindPattern
    | '_' # DiscardBindPattern
    | LPAREN (bindpatt AND)+ bindpatt RPAREN # AndTypeBindPattern
    | LPAREN ID bindpatt RPAREN # OrTypeBindPattern
    | LPAREN (bindpatt JOIN)+ bindpatt RPAREN # ListBindPattern
    ;
    
expr
    : mathexpr JOIN expr # JoinExpr
    | mathexpr # MathexprExpr
    ;
    
mathexpr
    : mathexpr PLUS term # PlusMathexpr
    | mathexpr MINUS term # MinusMathexpr
    | term # TermMathexpr
    ;
   
term
    : term TIMES call # TimesTerm
    | call # CallTerm
    ;
    
call
    : atom atom+ # CallCall
    | atom # AtomCall
    ;
    
atom
    : INT # IntAtom
    | ID # VarAtom
    | STR # StringAtom
    | 'nil' # NilAtom
    | LPAREN expr RPAREN # ParenAtom
    | LBRACK (expr COMMA)* expr? RBRACK (DBLCOLON simpleanontypename)? # ListAtom
    ;