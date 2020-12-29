lexer grammar GtlLexer;

@header {
    #pragma warning disable CS0115 // CS0115: no suitable method found to override
    #pragma warning disable CS3021 // CS3021: The CLSCompliant attribute is not needed because the assembly does not have a CLSCompliant attribute
    // ReSharper disable InvalidXmlDocComment
    // ReSharper disable all
}

// Hierarchical
PATH_PART_TRAVERSER_CHILDREN                        : '\\' ;
PATH_PART_TRAVERSER_CHILDREN_ALL                    : '\\\\' ;
PATH_PART_TRAVERSER_PARENT                          : '/' ;
PATH_PART_TRAVERSER_PARENTS_ALL                     : '//' ;

// Sequential
PATH_PART_TRAVERSER_PREVIOUS_SINGLE                 : '<' ;
PATH_PART_TRAVERSER_PREVIOUS_MULTIPLE               : '<' INTEGER_DECIMAL ;
PATH_PART_TRAVERSER_PREVIOUS_FIRST                  : '<<' ;
PATH_PART_TRAVERSER_NEXT_SINGLE                     : '>' ;
PATH_PART_TRAVERSER_NEXT_MULTIPLE                   : '>' INTEGER_DECIMAL ;
PATH_PART_TRAVERSER_NEXT_LAST                       : '>>' ;

// Temporal
PATH_PART_TRAVERSER_DOWNDATE                        : '{' ;
PATH_PART_TRAVERSER_DOWNDATES                       : '{' INTEGER_DECIMAL ;
PATH_PART_TRAVERSER_DOWNDATES_ALL                   : '{*' ;
PATH_PART_TRAVERSER_DOWNDATES_OLDEST                : '{{' ;
PATH_PART_TRAVERSER_UPDATES                         : '}' ;
PATH_PART_TRAVERSER_UPDATES_ALL                     : '}*' ;
PATH_PART_TRAVERSER_UPDATES_NEWEST                  : '}}' ;

// Regex
fragment MATCHER_REGEX_QUOTED_DOUBLE                : '["' (  '\\"' | ~["])*? '"]' ;
fragment MATCHER_REGEX_QUOTED_SINGLE                : '[\'' (  '\\\'' | ~['])*? '\']' ;
PATH_PART_MATCHER_REGEX
    : MATCHER_REGEX_QUOTED_DOUBLE
    | MATCHER_REGEX_QUOTED_SINGLE
    ;

// Variable
PATH_PART_MATCHER_VARIABLE                          : VARIABLE ;

// Constant
PATH_PART_MATCHER_CONSTANT                          : STRING ;

// Identifier
fragment MATCHER_IDENTIFIER_PREFIX                  : '/&' ;
PATH_PART_MATCHER_IDENTIFIER                        : MATCHER_IDENTIFIER_PREFIX IDENTIFIER ;

// Wildcards
fragment MATCHER_WILDCARD_CHAR                      : '*';
fragment MATCHER_WILDCARD_BEFORE_NONQUOTED          : MATCHER_WILDCARD_CHAR ~[\r\n]+? ;
fragment MATCHER_WILDCARD_BEFORE_QUOTED_DOUBLE      : '"' MATCHER_WILDCARD_CHAR (  '\\"' | ~["\r\n])*? '"' ;
fragment MATCHER_WILDCARD_BEFORE_QUOTED_SINGLE      : '\'' MATCHER_WILDCARD_CHAR (  '\\\'' | ~['\r\n])*? '\'' ;
fragment MATCHER_WILDCARD_AFTER_NONQUOTED           : ~[\r\n]+? MATCHER_WILDCARD_CHAR ;
fragment MATCHER_WILDCARD_AFTER_QUOTED_DOUBLE       : '"' (  '\\"' | ~["\r\n])*? MATCHER_WILDCARD_CHAR '"' ;
fragment MATCHER_WILDCARD_AFTER_QUOTED_SINGLE       : '\'' (  '\\\'' ~['\r\n])*? MATCHER_WILDCARD_CHAR '\'' ;

PATH_PART_MATCHER_WILDCARD
    : MATCHER_WILDCARD_BEFORE_NONQUOTED
    | MATCHER_WILDCARD_BEFORE_QUOTED_DOUBLE
    | MATCHER_WILDCARD_BEFORE_QUOTED_SINGLE
    | MATCHER_WILDCARD_AFTER_NONQUOTED
    | MATCHER_WILDCARD_AFTER_QUOTED_DOUBLE
    | MATCHER_WILDCARD_AFTER_QUOTED_SINGLE
    ;

// Operators
fragment OPERATOR_ASSIGN                            : '<=';
fragment OPERATOR_ADD                               : '+=';
fragment OPERATOR_REMOVE                            : '-=';
OPERATOR
    : OPERATOR_ASSIGN
    | OPERATOR_ADD
    | OPERATOR_REMOVE
    ;

fragment ROOT_SEPARATOR                             : ':';
ROOT                                                : IDENTITY ROOT_SEPARATOR ;
// Comments
fragment COMMENT_PREFIX                             : '--';
COMMENT                                             : '--' STRING;


// String
fragment STRING_QUOTED_DOUBLE                       : '"' (  '\\"' | ~["] | ~[\r\n])*? '"' ;
fragment STRING_QUOTED_SINGLE                       : '\'' (  '\\\'' | ~['] | ~[\r\n])*? '\'' ;
//fragment STRING_NONQUOTED                           : ~[\r\n]+?;
fragment STRING_NONQUOTED                           : (SPACE | WORD | DIGIT_DECIMAL | SPECIAL_CHARACTER)+;

STRING
    : STRING_NONQUOTED
    | STRING_QUOTED_DOUBLE
    | STRING_QUOTED_SINGLE
    ;

// Variables
fragment VARIABLE_PREFIX                            : '$' ;
VARIABLE                                            : VARIABLE_PREFIX IDENTITY ;

// Specials =======================================================================

fragment SPACE                                      : [ \t]+ ;
fragment WORD                                       : [a-zA-Z]+ ;
fragment DIGIT_DECIMAL                              : [0-9] ;
fragment DIGIT_HEX                                  : [A-Fa-f0-9] ;
fragment SPECIAL_CHARACTER                          : [.()] ;

fragment WHITESPACE                                 : [ \t\f\r\n]+ ;
EOL                                                 : ('\r'? '\n' | '\r')+ ;

// keeping whitespace tokenised makes it easier for syntax highlighting
DISCARD                                             : ( WHITESPACE | EOL ) -> skip ;

// Primitives =======================================================================
BOOLEAN_TRUE                                        : ('TRUE' | 'true' | 'True');
BOOLEAN_FALSE                                       : ('FALSE' | 'false' | 'False');
BOOLEAN
    : BOOLEAN_FALSE
    | BOOLEAN_TRUE
    ;

INTEGER_DECIMAL                                     : DIGIT_DECIMAL+ ;
INTEGER_HEX                                         : DIGIT_HEX+ ;
GUID                                                : GUID_BLOCK_8 '-' GUID_BLOCK_4 '-' GUID_BLOCK_4 '-' GUID_BLOCK_4 '-' GUID_BLOCK_8 GUID_BLOCK_4 ;
fragment GUID_BLOCK_4                               : DIGIT_HEX DIGIT_HEX DIGIT_HEX DIGIT_HEX ;
fragment GUID_BLOCK_8                               : DIGIT_HEX DIGIT_HEX DIGIT_HEX DIGIT_HEX DIGIT_HEX DIGIT_HEX DIGIT_HEX DIGIT_HEX ;

IDENTIFIER                                          : GUID '.' GUID '.' GUID '.' INTEGER_DECIMAL '.' INTEGER_DECIMAL '.' INTEGER_DECIMAL ;


fragment IDENTITY_CHAR
   : IDENTITY_START_CHAR
   | '0'..'9'
   | '_'
   | '\u00B7'
   | '\u0300'..'\u036F'
   | '\u203F'..'\u2040'
   ;
fragment IDENTITY_START_CHAR
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
IDENTITY                                            : IDENTITY_START_CHAR IDENTITY_CHAR*;


//
//ROOT                : ('ROOT' | 'root' | 'Root') ':';
//
//fragment LETTER:
//	[a-zA-Z_äöüÄÖÜáéíóúÁÉÍÓÚâêîôûÂÊÎÔÛàèìòùÀÈÌÒÙãẽĩõũÃẼĨÕŨçÇ];
//
//fragment LETTERORDIGIT:
//	[a-zA-Z0-9_äöüÄÖÜáéíóúÁÉÍÓÚâêîôûÂÊÎÔÛàèìòùÀÈÌÒÙãẽĩõũÃẼĨÕŨçÇ];
//
//
