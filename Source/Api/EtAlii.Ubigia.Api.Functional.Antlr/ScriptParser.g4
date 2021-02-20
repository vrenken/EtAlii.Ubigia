parser grammar ScriptParser;

@header {
    #pragma warning disable CS0115 // CS0115: no suitable method found to override
    #pragma warning disable CS3021 // CS3021: The CLSCompliant attribute is not needed because the assembly does not have a CLSCompliant attribute
    // ReSharper disable InvalidXmlDocComment
    // ReSharper disable all
}

options {
     language = CSharp;
     tokenVocab = UbigiaLexer;
}

import Primitives, PathParser ;

script: (WHITESPACE | NEWLINE)* sequence+ (WHITESPACE | NEWLINE)* EOF;

comment : WHITESPACE* COMMENT ;

sequence
    : subject_operator_pair+ subject_optional? comment? WHITESPACE* NEWLINE+ (WHITESPACE | NEWLINE)*  #sequence_pattern_1
    | operator_subject_pair+ operator_optional? comment? WHITESPACE* NEWLINE+ (WHITESPACE | NEWLINE)* #sequence_pattern_2
    | subject comment? WHITESPACE* NEWLINE+ (WHITESPACE | NEWLINE)*                                   #sequence_pattern_3
    | comment WHITESPACE* NEWLINE+ (WHITESPACE | NEWLINE)*                                            #sequence_pattern_4
    ;

subject_operator_pair                               : subject operator ;
operator_subject_pair                               : operator subject ;
subject_optional                                    : subject ;
operator_optional                                   : operator ;

operator_assign : WHITESPACE* LCHEVR EQUALS WHITESPACE* ;
operator_add : WHITESPACE* PLUS EQUALS WHITESPACE* ;
operator_remove : WHITESPACE* MINUS EQUALS WHITESPACE* ;
operator
    : operator_assign
    | operator_add
    | operator_remove
    ;

subject
    : subject_root
    | subject_function
    | subject_constant_string
    | subject_constant_object
    | subject_root_definition
    | subject_rooted_path
    | subject_non_rooted_path
    | subject_variable
    ;

subject_non_rooted_path                             : non_rooted_path ;
subject_rooted_path                                 : rooted_path ;
subject_constant_object                             : object_value ;
subject_constant_string                             : string_quoted ;
subject_root                                        : ROOT_SUBJECT_PREFIX COLON identifier;
subject_variable                                    : DOLLAR identifier ;
subject_root_definition                             : identifier (DOT identifier)+ ;

// Functions.
subject_function
    : identifier WHITESPACE* LPAREN WHITESPACE* RPAREN WHITESPACE*
    | identifier WHITESPACE* LPAREN (WHITESPACE* subject_function_argument WHITESPACE* COMMA WHITESPACE*)*? subject_function_argument WHITESPACE* RPAREN WHITESPACE*
    ;

subject_function_argument
    : subject_function_argument_string_quoted
    | subject_function_argument_identifier
    //| subject_function_argument_value
    | subject_function_argument_variable
    | subject_function_argument_rooted_path
    | subject_function_argument_non_rooted_path
    ;

subject_function_argument_identifier                : identifier ;
subject_function_argument_string_quoted             : string_quoted ;
subject_function_argument_variable                  : DOLLAR identifier ;
subject_function_argument_non_rooted_path           : path_part+ ;
subject_function_argument_rooted_path               : identifier COLON path_part*;
// This one should replace both the identifier and string_quoted argument:
//subject_function_argument_value
//    : string_quoted
//    | string_quoted_non_empty
//    | datetime
//    | timespan
//    | float_literal
//    | float_literal_unsigned
//    | integer_literal
//    | integer_literal_unsigned
//    | boolean_literal
//    ;
