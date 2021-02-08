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

reserved_schema_words
    : ANNOTATION_NODE_ADD
    | ANNOTATION_NODES_ADD
    | ANNOTATION_NODE_LINK
    | ANNOTATION_NODES_LINK
    | ANNOTATION_NODE_REMOVE
    | ANNOTATION_NODES_REMOVE
    | ANNOTATION_NODE
    | ANNOTATION_NODES
    | ANNOTATION_NODE_UNLINK
    | ANNOTATION_NODES_UNLINK
    | ANNOTATION_NODE_SET
    | ANNOTATION_NODE_CLEAR
    | HEADER_OPTION_NAMESPACE
    | HEADER_OPTION_CONTEXT
    | HEADER_OPTION_CONTEXT
    ;
