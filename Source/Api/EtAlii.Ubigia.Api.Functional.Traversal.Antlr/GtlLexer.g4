lexer grammar GtlLexer;

@header {
    #pragma warning disable CS0115 // CS0115: no suitable method found to override
    #pragma warning disable CS3021 // CS3021: The CLSCompliant attribute is not needed because the assembly does not have a CLSCompliant attribute
    // ReSharper disable InvalidXmlDocComment
    // ReSharper disable all
}

// Hierarchical
TRAVERSER_CHILDREN      : '\\' ;
TRAVERSER_CHILDREN_ALL  : '\\\\';
TRAVERSER_PARENT        : '/' ;
TRAVERSER_PARENTS_ALL   : '//' ;

ROOT_SEPARATOR          : ':';

// Sequential
// Temporal

COMMENT_PREFIX          : '--';

OPERATOR_ASSIGN         : '<=';
OPERATOR_ADD            : '+=';
OPERATOR_REMOVE         : '-=';

STRING_QUOTED_DOUBLE    : '"' (  '\\"' | ~[<"])*? '"' ;
STRING_QUOTED_SINGLE    : '\'' (  '\\\'' | ~[<'])*? '\'' ;
//STRING_NONQUOTED        : (~[\r\n/\\])+;

NEWLINE                 : ('\r\n' | '\n')+;// -> channel(HIDDEN);

SPACE                   : [ \t]+ ;
WORD                    : [a-zA-Z]+ ;
ANY                     : .+? ;

WHITESPACE              : [ \t\f\r\n]+;// -> channel(HIDDEN); // skip whitespaces
//DISCARDABLE: . -> channel(HIDDEN); // keeping whitespace tokenised makes it easier for syntax highlighting


//
//BOOLEAN                 : (BOOLEAN_FALSE | BOOLEAN_TRUE);
//BOOLEAN_TRUE            : ('TRUE' | 'true' | 'True');
//BOOLEAN_FALSE           : ('FALSE' | 'false' | 'False');
//NEWLINE                 : ('\r'? '\n' | '\r')+;
//COMMENT                 : '--' ~[\r\n]*;
//
//QUOTED_TEXT
//    : '"' (~["\r\n] | '\\"')* '"'
//    | '\'' (~['\r\n] | '\'')* '\''
//    ;
//
//fragment CONSTANT
//    : NAME
//    | QUOTED_TEXT
//    | (LOWERCASE | UPPERCASE | [.])+
//    ;
//
//OPERATOR
//    : OPERATOR_ASSIGN
//    | OPERATOR_ADD
//    | OPERATOR_REMOVE
//    ;
//
//
//ROOT                : ('ROOT' | 'root' | 'Root') ':';
//
//NAME                : NAME_START_CHAR NAME_CHAR*;
//
//fragment LETTER:
//	[a-zA-Z_äöüÄÖÜáéíóúÁÉÍÓÚâêîôûÂÊÎÔÛàèìòùÀÈÌÒÙãẽĩõũÃẼĨÕŨçÇ];
//
//fragment LETTERORDIGIT:
//	[a-zA-Z0-9_äöüÄÖÜáéíóúÁÉÍÓÚâêîôûÂÊÎÔÛàèìòùÀÈÌÒÙãẽĩõũÃẼĨÕŨçÇ];
//
//fragment
//NAME_CHAR
//   : NAME_START_CHAR
//   | '0'..'9'
//   | '_'
//   | '\u00B7'
//   | '\u0300'..'\u036F'
//   | '\u203F'..'\u2040'
//   ;
//fragment
//NAME_START_CHAR
//   : 'A'..'Z' | 'a'..'z'
//   | '\u00C0'..'\u00D6'
//   | '\u00D8'..'\u00F6'
//   | '\u00F8'..'\u02FF'
//   | '\u0370'..'\u037D'
//   | '\u037F'..'\u1FFF'
//   | '\u200C'..'\u200D'
//   | '\u2070'..'\u218F'
//   | '\u2C00'..'\u2FEF'
//   | '\u3001'..'\uD7FF'
//   | '\uF900'..'\uFDCF'
//   | '\uFDF0'..'\uFFFD'
//   ;
//

//fragment TEXT_ANY                : ~[\r\n]+ ;

// OLD HELLO WORLD

//fragment A              : ('A'|'a') ;
//fragment S              : ('S'|'s') ;
//fragment Y              : ('Y'|'y') ;

//fragment LOWERCASE      : [a-z] ;
//fragment UPPERCASE      : [A-Z] ;

//SAYS                    : S A Y S ;
//WORD                    : (LOWERCASE | UPPERCASE)+ ;
//TEXT_ESCAPED            : '"' .*? '"' ;
//TEXT_UNESCAPED        : .*? ;

// WHITESPACE              : (' '|'\t')+ -> skip ;
//WHITESPACE : [ \r\t\n]+ -> skip ;
