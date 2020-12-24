parser grammar GtlParser;

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

script                  : sequence EOF;
sequence
    : subject
    | subject operator subject
    | subject operator
    | COMMENT
    | NEWLINE;

operator                : OPERATOR;
subject
    : subject_root
    | subject_function
    | subject_constant
    | subject_root_definition
    | subject_rooted_path
    | subject_nonrooted_path
    | subject_variable
    ;

subject_root            : ROOT (QUOTED_TEXT | NAME);
subject_function        : EOF;
subject_constant        : EOF;
subject_root_definition : EOF;
subject_rooted_path     : EOF;
subject_nonrooted_path  : EOF;
subject_variable        : EOF;


// OLD HELLO WORLD
chat                    : line line EOF ;
line                    : name SAYS opinion NEWLINE;
name                    : WORD ;
opinion                 : TEXT_ESCAPED ;
