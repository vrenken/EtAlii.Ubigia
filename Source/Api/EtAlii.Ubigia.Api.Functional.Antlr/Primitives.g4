parser grammar Primitives;

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

// Datetimes.
datetime_d4                                    : DIGIT DIGIT DIGIT DIGIT ;
datetime_d2                                    : DIGIT? DIGIT ;
datetime_d3                                    : DIGIT? DIGIT? DIGIT ;
datetime
    : datetime_d4 MINUS datetime_d2 MINUS datetime_d2 WHITESPACE datetime_d2 COLON datetime_d2 COLON datetime_d2 COLON datetime_d3     #datetime_format_1
    | datetime_d4 MINUS datetime_d2 MINUS datetime_d2 WHITESPACE datetime_d2 COLON datetime_d2 COLON datetime_d2                       #datetime_format_2
    | datetime_d4 MINUS datetime_d2 MINUS datetime_d2 WHITESPACE datetime_d2 COLON datetime_d2                                         #datetime_format_3
    | datetime_d4 MINUS datetime_d2 MINUS datetime_d2                                                                                  #datetime_format_4
    | datetime_d2 MINUS datetime_d2 MINUS datetime_d4 WHITESPACE datetime_d2 COLON datetime_d2 COLON datetime_d2 COLON datetime_d3     #datetime_format_5
    | datetime_d2 MINUS datetime_d2 MINUS datetime_d4 WHITESPACE datetime_d2 COLON datetime_d2 COLON datetime_d2                       #datetime_format_6
    | datetime_d2 MINUS datetime_d2 MINUS datetime_d4 WHITESPACE datetime_d2 COLON datetime_d2                                         #datetime_format_7
    | datetime_d2 MINUS datetime_d2 MINUS datetime_d4                                                                                  #datetime_format_8
    ;

timespan
    : integer_literal_unsigned COLON integer_literal_unsigned COLON integer_literal_unsigned COLON integer_literal_unsigned DOT integer_literal_unsigned ;

// Objects.
object_value
    : WHITESPACE* LBRACE (WHITESPACE | NEWLINE)* object_kv_pair_with_comma*? WHITESPACE* object_kv_pair_without_comma WHITESPACE* RBRACE WHITESPACE*
    | WHITESPACE* LBRACE (WHITESPACE | NEWLINE)* RBRACE WHITESPACE*
    ;

object_kv_pair_without_comma                        : WHITESPACE* object_kv_key WHITESPACE* COLON WHITESPACE* object_kv_value? (WHITESPACE | NEWLINE)* ;
object_kv_pair_with_comma                           : WHITESPACE* object_kv_key WHITESPACE* COLON WHITESPACE* object_kv_value? (WHITESPACE | NEWLINE)* COMMA (WHITESPACE | NEWLINE)*;

object_kv_key
    : identifier
    | string_quoted_non_empty
    ;

object_kv_value
    : primitive_value
    | object_value
    ;

primitive_value
    : string_quoted
    | string_quoted_non_empty
    | datetime
    | timespan
    | float_literal
    | float_literal_unsigned
    | integer_literal
    | integer_literal_unsigned
    | boolean_literal
    ;

reserved_words
    : BOOLEAN_LITERAL
    | ROOT_SUBJECT_PREFIX
    | ANNOTATION_NODE
    | ANNOTATION_NODES
    | HEADER_OPTION_NAMESPACE
    | HEADER_OPTION_CONTEXT
    | VALUE_TYPE_OBJECT
    | VALUE_TYPE_STRING
    | VALUE_TYPE_BOOL
    | VALUE_TYPE_FLOAT
    | VALUE_TYPE_INT
    | VALUE_TYPE_DATETIME
    | VALUE_TYPE_GUID
//    | ANNOTATION_NODE_ADD
//    | ANNOTATION_NODES_ADD
//    | ANNOTATION_NODE_LINK
//    | ANNOTATION_NODES_LINK
//    | ANNOTATION_NODE_REMOVE
//    | ANNOTATION_NODES_REMOVE
//    | ANNOTATION_NODE_UNLINK
//    | ANNOTATION_NODES_UN_LINK
//    | ANNOTATION_NODE_VALUE_SET
//    | ANNOTATION_NODE_VALUE_CLEAR
    ;

identifier                                          : IDENTIFIER | reserved_words ;
string_quoted                                       : STRING_QUOTED ;
string_quoted_non_empty                             : STRING_QUOTED_NON_EMPTY ;
integer_literal                                     : (PLUS | MINUS) DIGIT+ ;
integer_literal_unsigned                            : DIGIT+ ;
float_literal                                       : (PLUS | MINUS) DIGIT+ DOT DIGIT+ ;
float_literal_unsigned                              : DIGIT+ DOT DIGIT+ ;
boolean_literal                                     : BOOLEAN_LITERAL ;
