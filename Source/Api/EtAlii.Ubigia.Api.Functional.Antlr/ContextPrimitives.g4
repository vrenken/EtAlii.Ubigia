parser grammar ContextPrimitives;

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
