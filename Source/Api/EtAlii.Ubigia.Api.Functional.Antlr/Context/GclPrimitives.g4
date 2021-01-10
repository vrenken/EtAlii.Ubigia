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

reserved_words
    : BOOLEAN_LITERAL
    | ROOT_SUBJECT_PREFIX
//    | ANNOTATION_NODE_ADD
//    | ANNOTATION_NODES_ADD
//    | ANNOTATION_NODE_LINK
//    | ANNOTATION_NODES_LINK
//    | ANNOTATION_NODE_REMOVE
//    | ANNOTATION_NODES_REMOVE
//    | ANNOTATION_NODE
//    | ANNOTATION_NODES
//    | ANNOTATION_NODE_UNLINK
//    | ANNOTATION_NODES_UN_LINK
//    | ANNOTATION_NODE_VALUE_SET
//    | ANNOTATION_NODE_VALUE_CLEAR
    ;

identifier                                          : IDENTIFIER | reserved_words;
string_quoted                                       : STRING_QUOTED ;
string_quoted_non_empty                             : STRING_QUOTED_NON_EMPTY ;
