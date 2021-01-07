parser grammar GclParser;

@header {
    #pragma warning disable CS0115 // CS0115: no suitable method found to override
    #pragma warning disable CS3021 // CS3021: The CLSCompliant attribute is not needed because the assembly does not have a CLSCompliant attribute
    // ReSharper disable InvalidXmlDocComment
    // ReSharper disable all
}

options {
     language = CSharp;
     tokenVocab = GclLexer;
}

import GclPrimitives;

schema: (WHITESPACE | NEWLINE)* sequence+ (WHITESPACE | NEWLINE)* EOF;

comment : WHITESPACE* COMMENT ;

sequence
    : schema_fragment comment? WHITESPACE* NEWLINE+ (WHITESPACE | NEWLINE)*                           #schema_pattern_1
    | comment WHITESPACE* NEWLINE+ (WHITESPACE | NEWLINE)*                                            #schema_pattern_2
    ;

schema_fragment : schema_fragment_title NEWLINE* (schema_fragment_body_newline_separated | schema_fragment_body_comma_separated);

schema_fragment_title : fragment_requirement? identifier WHITESPACE+ ATSIGN identifier LPAREN schema_path RPAREN WHITESPACE?;

schema_fragment_body_newline_separated : LBRACE (WHITESPACE | NEWLINE)* schema_fragment_body_line_newline_separated* (WHITESPACE | NEWLINE)* RBRACE ;
schema_fragment_body_comma_separated : LBRACE (WHITESPACE | NEWLINE)* schema_fragment_body_line_comma_separated* (WHITESPACE | NEWLINE)* RBRACE ;

schema_fragment_body_line_newline_separated
    : identifier WHITESPACE* NEWLINE (WHITESPACE | NEWLINE)*
    | string_quoted_non_empty WHITESPACE* NEWLINE (WHITESPACE | NEWLINE)*
    | schema_fragment WHITESPACE* NEWLINE (WHITESPACE | NEWLINE)*
    | schema_fragment_title WHITESPACE* NEWLINE (WHITESPACE | NEWLINE)*
    ;

schema_fragment_body_line_comma_separated
    : identifier WHITESPACE* COMMA (WHITESPACE | NEWLINE)*
    | string_quoted_non_empty WHITESPACE* COMMA (WHITESPACE | NEWLINE)*
    | schema_fragment WHITESPACE* COMMA (WHITESPACE | NEWLINE)*
    | schema_fragment_title WHITESPACE* COMMA (WHITESPACE | NEWLINE)*
    ;

schema_path : (identifier | WHITESPACE | FSLASH | BSLASH | HASHTAG | COLON)+;
fragment_requirement : EXCLAMATION | QUESTION ;

