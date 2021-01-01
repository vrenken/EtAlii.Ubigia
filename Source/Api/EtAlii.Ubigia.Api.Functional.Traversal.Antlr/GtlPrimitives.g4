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
datetime_date_mm                                    : DIGIT? DIGIT ;
datetime_date_dd                                    : DIGIT? DIGIT ;
datetime_time_hh                                    : DIGIT? DIGIT ;
datetime_time_mm                                    : DIGIT? DIGIT ;
datetime_time_ss                                    : DIGIT? DIGIT ;
datetime_ms                                         : DIGIT? DIGIT? DIGIT ;
datetime
    : datetime_date_yyyy MINUS datetime_date_mm MINUS datetime_date_dd datetime_time_hh COLON datetime_time_mm COLON datetime_time_ss COLON datetime_ms     #datetime_format_1
    | datetime_date_yyyy MINUS datetime_date_mm MINUS datetime_date_dd datetime_time_hh COLON datetime_time_mm COLON datetime_time_ss                       #datetime_format_2
    | datetime_date_yyyy MINUS datetime_date_mm MINUS datetime_date_dd datetime_time_hh COLON datetime_time_mm                                              #datetime_format_3
    | datetime_date_yyyy MINUS datetime_date_mm MINUS datetime_date_dd                                                                                      #datetime_format_4
    | datetime_date_dd MINUS datetime_date_mm MINUS datetime_date_yyyy datetime_time_hh COLON datetime_time_mm COLON datetime_time_ss COLON datetime_ms     #datetime_format_5
    | datetime_date_dd MINUS datetime_date_mm MINUS datetime_date_yyyy datetime_time_hh COLON datetime_time_mm COLON datetime_time_ss                       #datetime_format_6
    | datetime_date_dd MINUS datetime_date_mm MINUS datetime_date_yyyy datetime_time_hh COLON datetime_time_mm                                              #datetime_format_7
    | datetime_date_dd MINUS datetime_date_mm MINUS datetime_date_yyyy                                                                                      #datetime_format_8
    ;

timespan
    : integer_literal_unsigned COLON integer_literal_unsigned COLON integer_literal_unsigned COLON integer_literal_unsigned DOT integer_literal_unsigned ;

// Objects.
object
    : LBRACE NEWLINE* object_kv_pair_with_comma*? object_kv_pair_without_comma RBRACE
    | LBRACE NEWLINE* RBRACE
    ;

object_kv_pair_without_comma                        : object_kv_key COLON object_kv_value NEWLINE* ;
object_kv_pair_with_comma                           : object_kv_key COLON object_kv_value NEWLINE* COMMA NEWLINE*;

object_kv_key
    : identifier
    | string_quoted
    ;

object_kv_value
    : datetime
    | timespan
    | string_quoted
    | float_literal
    | float_literal_unsigned
    | integer_literal
    | integer_literal_unsigned
    | boolean_literal
    | object
    ;

identifier                                          : IDENTIFIER ;
string_quoted                                       : STRING_QUOTED ;
integer_literal                                     : (PLUS | MINUS) DIGIT+ ;
integer_literal_unsigned                            : DIGIT+ ;
float_literal                                       : (PLUS | MINUS) DIGIT+ DOT DIGIT+ ;
float_literal_unsigned                              : DIGIT+ DOT DIGIT+ ;
boolean_literal                                     : BOOLEAN_LITERAL ;
