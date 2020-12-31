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
    : LBRACE (IDENTIFIER COLON kvp_value) RBRACE
    | LBRACE (IDENTIFIER COLON kvp_value COMMA)+ IDENTIFIER COLON kvp_value RBRACE
    | LBRACE (IDENTIFIER COLON kvp_value) RBRACE
    | LBRACE (IDENTIFIER COLON kvp_value COMMA)+ IDENTIFIER COLON kvp_value RBRACE
    ;
kvp_value
    : STRING_QUOTED
    | INTEGER_LITERAL
    | INTEGER_LITERAL_UNSIGNED
    | FLOAT_LITERAL
    | FLOAT_LITERAL_UNSIGNED
    | BOOLEAN_LITERAL
    | datetime
    | object
    ;
