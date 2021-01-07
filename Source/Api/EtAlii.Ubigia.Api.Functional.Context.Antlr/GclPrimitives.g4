parser grammar GclPrimitives;

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

identifier                                          : IDENTIFIER ;
string_quoted                                       : STRING_QUOTED ;
string_quoted_non_empty                             : STRING_QUOTED_NON_EMPTY ;
