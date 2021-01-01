parser grammar GtlPrimitives;

@header {
    #pragma warning disable CS0115 // CS0115: no suitable method found to override
    #pragma warning disable CS3021 // CS3021: The CLSCompliant attribute is not needed because the assembly does not have a CLSCompliant attribute
    // ReSharper disable InvalidXmlDocComment
    // ReSharper disable all
}

options {
     language = CSharp;
     tokenVocab = GtlLexer;
}

// Datetimes.
datetime_date_yyyy                                  : DIGIT DIGIT DIGIT DIGIT ;
datetime_date_mm                                    : DIGIT DIGIT ;
datetime_date_dd                                    : DIGIT DIGIT ;
datetime_time_hh                                    : DIGIT DIGIT ;
datetime_time_mm                                    : DIGIT DIGIT ;
datetime_time_ss                                    : DIGIT DIGIT ;
datetime_ms                                         : DIGIT DIGIT DIGIT ;
datetime
    : datetime_date_yyyy MINUS datetime_date_mm MINUS datetime_date_dd SPACE datetime_time_hh COLON datetime_time_mm COLON datetime_time_ss COLON datetime_ms
    | datetime_date_yyyy MINUS datetime_date_mm MINUS datetime_date_dd SPACE datetime_time_hh COLON datetime_time_mm COLON datetime_time_ss
    | datetime_date_yyyy MINUS datetime_date_mm MINUS datetime_date_dd SPACE datetime_time_hh COLON datetime_time_mm
    | datetime_date_yyyy MINUS datetime_date_mm MINUS datetime_date_dd
    ;

// Objects.
object
    : LBRACE NEWLINE* object_kv_pair_with_comma+ object_kv_pair_without_comma RBRACE
    | LBRACE NEWLINE* object_kv_pair_without_comma RBRACE
    | LBRACE NEWLINE* RBRACE
    ;

object_kv_pair_without_comma                        : object_kv_key COLON object_kv_value NEWLINE* ;
object_kv_pair_with_comma                           : object_kv_key COLON object_kv_value NEWLINE* COMMA NEWLINE*;

object_kv_key
    : identifier
    | string_quoted
    ;

object_kv_value
    : string_quoted
    | integer_literal
    | integer_literal_unsigned
    | float_literal
    | float_literal_unsigned
    | boolean_literal
    | datetime
    | object
    ;

identifier                                          : IDENTIFIER ;
string_quoted                                       : STRING_QUOTED ;
integer_literal                                     : INTEGER_LITERAL ;
integer_literal_unsigned                            : INTEGER_LITERAL_UNSIGNED ;
float_literal                                       : FLOAT_LITERAL ;
float_literal_unsigned                              : FLOAT_LITERAL_UNSIGNED ;
boolean_literal                                     : BOOLEAN_LITERAL ;
