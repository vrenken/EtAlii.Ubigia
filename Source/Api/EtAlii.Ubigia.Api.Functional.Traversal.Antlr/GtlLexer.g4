lexer grammar GtlLexer;

@header {
    #pragma warning disable CS0115 // CS0115: no suitable method found to override
    #pragma warning disable CS3021 // CS3021: The CLSCompliant attribute is not needed because the assembly does not have a CLSCompliant attribute
    // ReSharper disable InvalidXmlDocComment
    // ReSharper disable all
}


fragment DOUBLE_QUOTE_STRING
    : '"' ~[<"]* '"'
    ;
fragment SINGLE_QUOTE_STRING
    : '\'' ~[<']* '\''
    ;


BOOLEAN                 : (BOOLEAN_FALSE | BOOLEAN_TRUE);
BOOLEAN_TRUE            : ('TRUE' | 'true' | 'True');
BOOLEAN_FALSE           : ('FALSE' | 'false' | 'False');
NEWLINE                 : ('\r'? '\n' | '\r')+;
COMMENT                 : '--' .*? NEWLINE;

QUOTED_TEXT
    : '"' (~[\\"] | ESCAPE_SEQUENCE | QUOTED_CONTENT)* '"'
    | '\'' (~[\\'] | ESCAPE_SEQUENCE | QUOTED_CONTENT)* '\''
    ;

QUOTED_TEXT_DOUBLE      : '"' .*? '"';
QUOTED_TEXT_SINGLE      : '\'' .*? '\'';

fragment QUOTED_CONTENT
	: '\\' ('\r' '\n'? | '\n')
	;

ESCAPE_SEQUENCE
	: ESCAPE_IDENTITY | ESCAPE_ENCODED | ESCAPE_SEMICOLON
	;

fragment ESCAPE_IDENTITY
	: '\\' ~[A-Za-z0-9;]
	;
fragment ESCAPE_ENCODED
	: '\\t' | '\\r' | '\\n'
	;

fragment
ESCAPE_SEMICOLON
	: '\\;'
	;

OPERATOR
    : OPERATOR_ASSIGN
    | OPERATOR_ADD
    | OPERATOR_REMOVE
    ;

OPERATOR_ASSIGN     : '<=';
OPERATOR_ADD        : '+=';
OPERATOR_REMOVE     : '-=';

ROOT                : ('ROOT' | 'root' | 'Root') ':';

NAME                : NAME_START_CHAR NAME_CHAR*;

fragment
NAME_CHAR
   : NAME_START_CHAR
   | '0'..'9'
   | '_'
   | '\u00B7'
   | '\u0300'..'\u036F'
   | '\u203F'..'\u2040'
   ;
fragment
NAME_START_CHAR
   : 'A'..'Z' | 'a'..'z'
   | '\u00C0'..'\u00D6'
   | '\u00D8'..'\u00F6'
   | '\u00F8'..'\u02FF'
   | '\u0370'..'\u037D'
   | '\u037F'..'\u1FFF'
   | '\u200C'..'\u200D'
   | '\u2070'..'\u218F'
   | '\u2C00'..'\u2FEF'
   | '\u3001'..'\uD7FF'
   | '\uF900'..'\uFDCF'
   | '\uFDF0'..'\uFFFD'
   ;



// OLD HELLO WORLD

fragment A              : ('A'|'a') ;
fragment S              : ('S'|'s') ;
fragment Y              : ('Y'|'y') ;

fragment LOWERCASE      : [a-z] ;
fragment UPPERCASE      : [A-Z] ;

SAYS                    : S A Y S ;
WORD                    : (LOWERCASE | UPPERCASE)+ ;
TEXT_ESCAPED            : '"' .*? '"' ;
//TEXT_UNESCAPED        : .*? ;
WHITESPACE              : (' '|'\t')+ -> skip ;

