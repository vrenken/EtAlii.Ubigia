lexer grammar GtlLexer;

@header {
    #pragma warning disable CS0115 // CS0115: no suitable method found to override
    #pragma warning disable CS3021 // CS3021: The CLSCompliant attribute is not needed because the assembly does not have a CLSCompliant attribute
    // ReSharper disable InvalidXmlDocComment
    // ReSharper disable all
}

// Regex
fragment MATCHER_REGEX_QUOTED_DOUBLE                : '["' (  '\\"' | ~["])*? '"]' ;
fragment MATCHER_REGEX_QUOTED_SINGLE                : '[\'' (  '\\\'' | ~['])*? '\']' ;
PATH_PART_MATCHER_REGEX
    : MATCHER_REGEX_QUOTED_DOUBLE
    | MATCHER_REGEX_QUOTED_SINGLE
    ;

// Constant
PATH_PART_MATCHER_CONSTANT
    : STRING_QUOTED
    // This is a weird rule. It is needed to get the sample*.txt files operational but might become obsolete later.
    | STRING_UNQUOTED
    | IDENTITY
    ;

// Wildcards
MATCHER_WILDCARD_BEFORE_NONQUOTED          : AST ~[\r\n]+? ;
MATCHER_WILDCARD_BEFORE_QUOTED_DOUBLE      : '"' AST (  '\\"' | ~["\r\n])*? '"' ;
MATCHER_WILDCARD_BEFORE_QUOTED_SINGLE      : '\'' AST (  '\\\'' | ~['\r\n])*? '\'' ;
MATCHER_WILDCARD_AFTER_NONQUOTED           : ~[\r\n]+? ~[{] AST ;
MATCHER_WILDCARD_AFTER_QUOTED_DOUBLE       : '"' (  '\\"' | ~["\r\n])*? ~[{] AST '"' ;
MATCHER_WILDCARD_AFTER_QUOTED_SINGLE       : '\'' (  '\\\'' ~['\r\n])*? ~[{] AST '\'' ;

// Roots
fragment ROOT_SEPARATOR                             : ':' ;
fragment ROOT_SUBJECT_PREFIX                        : 'root' ROOT_SEPARATOR ;
ROOT_SUBJECT                                        : ROOT_SUBJECT_PREFIX IDENTITY ;
PATH_PART_MATCHER_ROOT                              : IDENTITY ROOT_SEPARATOR ;

// Comments
fragment COMMENT_PREFIX                             : '--';
COMMENT                                             : COMMENT_PREFIX (~[\r\n])*;


// String
fragment STRING_QUOTED_DOUBLE                       : '"' (  '\\"' | ~["] | ~[\r\n])*? '"' ;
fragment STRING_QUOTED_SINGLE                       : '\'' (  '\\\'' | ~['] | ~[\r\n])*? '\'' ;
fragment STRING_QUOTED
    : STRING_QUOTED_DOUBLE
    | STRING_QUOTED_SINGLE
    ;
fragment STRING_UNQUOTED                            : [a-zA-Z0-9]+ ;
SUBJECT_CONSTANT_STRING                             : STRING_QUOTED ;

// Objects.
fragment OBJECT
    : LBRACE (IDENTITY COLON KVP_VALUE) RBRACE
    | LBRACE (IDENTITY COLON KVP_VALUE COMMA)+ IDENTITY COLON KVP_VALUE RBRACE
    | LBRACE (IDENTITY COLON KVP_VALUE) RBRACE
    | LBRACE (IDENTITY COLON KVP_VALUE COMMA)+ IDENTITY COLON KVP_VALUE RBRACE
    ;
fragment KVP_VALUE
    : STRING_QUOTED
    | INTEGER_LITERAL
    | INTEGER_LITERAL_UNSIGNED
    | FLOAT_LITERAL
    | BOOLEAN_LITERAL
    | DATETIME
    | OBJECT
    ;
SUBJECT_CONSTANT_OBJECT                             : OBJECT ;
// Specials =======================================================================

fragment SPACE                                      : [ \t]+ ;
fragment WORD                                       : [a-zA-Z]+ ;

fragment WHITESPACE                                 : [ \t\f\r\n]+ ;
EOL                                                 : ('\r'? '\n' | '\r')+ ;

// keeping whitespace tokenised makes it easier for syntax highlighting
DISCARD                                             : ( WHITESPACE | EOL ) -> skip ;

// Primitives =======================================================================


// Datetimes.
fragment DATETIME_DATE_SEPARATOR                    : MINUS ;
fragment DATETIME_DATE_YYYY                         : DIGIT DIGIT DIGIT DIGIT ;
fragment DATETIME_DATE_MM                           : DIGIT DIGIT ;
fragment DATETIME_DATE_DD                           : DIGIT DIGIT ;
fragment DATETIME_TIME_SEPARATOR                    : COLON ;
fragment DATETIME_TIME_HH                           : DIGIT DIGIT ;
fragment DATETIME_TIME_MM                           : DIGIT DIGIT ;
fragment DATETIME_TIME_SS                           : DIGIT DIGIT ;
fragment DATETIME_MS_SEPARATOR                      : COLON ;
fragment DATETIME_MS                                : DIGIT DIGIT DIGIT ;
fragment DATETIME
    : DATETIME_DATE_YYYY DATETIME_DATE_SEPARATOR DATETIME_DATE_MM DATETIME_DATE_SEPARATOR DATETIME_DATE_DD ' ' DATETIME_TIME_HH DATETIME_TIME_SEPARATOR DATETIME_TIME_MM DATETIME_TIME_SEPARATOR DATETIME_TIME_SS DATETIME_MS_SEPARATOR DATETIME_MS
    | DATETIME_DATE_YYYY DATETIME_DATE_SEPARATOR DATETIME_DATE_MM DATETIME_DATE_SEPARATOR DATETIME_DATE_DD ' ' DATETIME_TIME_HH DATETIME_TIME_SEPARATOR DATETIME_TIME_MM DATETIME_TIME_SEPARATOR DATETIME_TIME_SS
    | DATETIME_DATE_YYYY DATETIME_DATE_SEPARATOR DATETIME_DATE_MM DATETIME_DATE_SEPARATOR DATETIME_DATE_DD ' ' DATETIME_TIME_HH DATETIME_TIME_SEPARATOR DATETIME_TIME_MM
    | DATETIME_DATE_YYYY DATETIME_DATE_SEPARATOR DATETIME_DATE_MM DATETIME_DATE_SEPARATOR DATETIME_DATE_DD
    ;


// Identities.
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
IDENTITY                                   : IDENTITY_START_CHAR IDENTITY_CHAR*;


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

// =====================================================================================================================
// New implementation

BYTE_ORDER_MARK: '\u00EF\u00BB\u00BF';

// Bools
fragment BOOLEAN_TRUE_LITERAL                       : ('TRUE' | 'true' | 'True');
fragment BOOLEAN_FALSE_LITERAL                      : ('FALSE' | 'false' | 'False');
BOOLEAN_LITERAL
    : BOOLEAN_FALSE_LITERAL
    | BOOLEAN_TRUE_LITERAL
    ;

// Integers
INTEGER_LITERAL_UNSIGNED                            : DIGIT+ ;
INTEGER_LITERAL
    : INTEGER_LITERAL_UNSIGNED
    | [+-] INTEGER_LITERAL_UNSIGNED
    ;

// Hex values.
HEX_LITERAL                                         : HEX+ ;

// Floats
fragment FLOAT_LITERAL_UNSIGNED                     : [0-9]+ '.' [0-9]+ ;
fragment FLOAT_LITERAL
    : FLOAT_LITERAL_UNSIGNED
    | [+-] FLOAT_LITERAL_UNSIGNED
    ;

// Guids.
GUID
    : GUID_BLOCK_8 '-' GUID_BLOCK_4 '-' GUID_BLOCK_4 '-' GUID_BLOCK_4 '-' GUID_BLOCK_8 GUID_BLOCK_4
    | GUID_BLOCK_8 GUID_BLOCK_4 GUID_BLOCK_4 GUID_BLOCK_4 GUID_BLOCK_8 GUID_BLOCK_4
    ;
fragment GUID_BLOCK_4                               : HEX HEX HEX HEX ;
fragment GUID_BLOCK_8                               : HEX HEX HEX HEX HEX HEX HEX HEX ;

// Characters.

fragment DIGIT                                      : [0-9] ;
fragment HEX                                        : [A-Fa-f0-9] ;

LPAREN                                              : '(';
RPAREN                                              : ')';
LBRACE                                              : '{';
RBRACE                                              : '}';
LBRACK                                              : '[';
RBRACK                                              : ']';
LCHEVR                                              : '<';
RCHEVR                                              : '>';
SEMI                                                : ';';
COLON                                               : ':';
COMMA                                               : ',';
DOT                                                 : '.';
MINUS                                               : '-';
PLUS                                                : '+';
FSLASH                                              : '/';
BSLASH                                              : '\\';
EQUALS                                              : '=';
AMP                                                 : '&';
AST                                                 : '*';
DOLLAR                                              : '$';


