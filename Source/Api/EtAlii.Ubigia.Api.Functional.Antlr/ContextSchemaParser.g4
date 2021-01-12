parser grammar ContextSchemaParser;

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

import ContextPrimitives, TraversalPrimitives, TraversalPathParser;

schema                                                  : (comment | WHITESPACE | NEWLINE)* structure_fragment (comment | WHITESPACE | NEWLINE)* EOF ;

//schema_fragment                                         : schema_fragment_title NEWLINE* (schema_fragment_body_newline_separated | schema_fragment_body_comma_separated);

//schema_fragment_title                                   : requirement? schema_key WHITESPACE+ ATSIGN identifier LPAREN schema_path RPAREN WHITESPACE?;

//schema_fragment_body_newline_separated                  : LBRACE (WHITESPACE | NEWLINE)* schema_fragment_body_line_newline_separated* (WHITESPACE | NEWLINE)* RBRACE ;
//schema_fragment_body_comma_separated                    : LBRACE (WHITESPACE | NEWLINE)* schema_fragment_body_line_comma_separated* (WHITESPACE | NEWLINE)* RBRACE ;

//schema_fragment_body_line_newline_separated
//    : schema_key WHITESPACE* NEWLINE (WHITESPACE | NEWLINE)*
//    | string_quoted_non_empty WHITESPACE* NEWLINE (WHITESPACE | NEWLINE)*
//    | schema_fragment WHITESPACE* NEWLINE (WHITESPACE | NEWLINE)*
//    | schema_fragment_title WHITESPACE* NEWLINE (WHITESPACE | NEWLINE)*
//    ;
//
//schema_fragment_body_line_comma_separated
//    : schema_key WHITESPACE* COMMA (WHITESPACE | NEWLINE)*
//    | string_quoted_non_empty WHITESPACE* COMMA (WHITESPACE | NEWLINE)*
//    | schema_fragment WHITESPACE* COMMA (WHITESPACE | NEWLINE)*
//    | schema_fragment_title WHITESPACE* COMMA (WHITESPACE | NEWLINE)*
//    ;


requirement : EXCLAMATION | QUESTION ;


structure_fragment_body_entry
    : structure_fragment
    | value_query_fragment
    | value_mutation_fragment
    | comment WHITESPACE* NEWLINE+ (WHITESPACE | NEWLINE)*
    ;

structure_fragment                                      : WHITESPACE* requirement? schema_key node_annotation? WHITESPACE* (WHITESPACE | NEWLINE)* LBRACE (WHITESPACE | NEWLINE)* structure_fragment_body (WHITESPACE | NEWLINE)* RBRACE;
value_query_fragment                                    : WHITESPACE* requirement? schema_key value_annotation? WHITESPACE* ;
value_mutation_fragment                                 : WHITESPACE* schema_key WHITESPACE* LCHEVR EQUALS WHITESPACE* string_quoted_non_empty WHITESPACE* WHITESPACE* ;

structure_fragment_body_newline_separated : (structure_fragment_body_entry WHITESPACE* NEWLINE+)* structure_fragment_body_entry (WHITESPACE | NEWLINE)* ;
structure_fragment_body_comma_separated : (structure_fragment_body_entry WHITESPACE* COMMA WHITESPACE* NEWLINE?)* structure_fragment_body_entry (WHITESPACE | NEWLINE)* ;
structure_fragment_body
    : structure_fragment_body_newline_separated
    | structure_fragment_body_comma_separated
    ;
                                                        // @node-set(SOURCE, VALUE)
value_annotation_assign_and_select_with_key             : WHITESPACE+ ATSIGN ANNOTATION_NODE_SET WHITESPACE* LPAREN WHITESPACE* schema_path WHITESPACE* COMMA WHITESPACE* schema_key WHITESPACE* RPAREN ;
                                                        // @node-set(SOURCE)
value_annotation_assign_and_select_without_key          : WHITESPACE+ ATSIGN ANNOTATION_NODE_SET WHITESPACE* LPAREN WHITESPACE* schema_path WHITESPACE* RPAREN ;
                                                        // @value-clear(SOURCE)
value_annotation_clear_and_select_with_key              : WHITESPACE+ ATSIGN ANNOTATION_NODE_CLEAR WHITESPACE* LPAREN WHITESPACE* schema_path WHITESPACE* COMMA WHITESPACE* schema_key WHITESPACE* RPAREN ;
                                                        // @value-clear()
value_annotation_clear_and_select_without_key           : WHITESPACE+ ATSIGN ANNOTATION_NODE_CLEAR WHITESPACE* LPAREN WHITESPACE* schema_path WHITESPACE* RPAREN ;
                                                        // @value-clear(SOURCE)
value_annotation_clear_and_select                       : WHITESPACE+ ATSIGN ANNOTATION_NODE_CLEAR WHITESPACE* LPAREN WHITESPACE* schema_path WHITESPACE* RPAREN ;
                                                        // @value(SOURCE) or @value()
value_annotation_select                                 : WHITESPACE+ ATSIGN ANNOTATION_NODE WHITESPACE* LPAREN WHITESPACE* schema_path? WHITESPACE* RPAREN ;
value_annotation
    : value_annotation_assign_and_select_with_key
    | value_annotation_assign_and_select_without_key
    | value_annotation_clear_and_select_with_key
    | value_annotation_clear_and_select_without_key
    | value_annotation_clear_and_select
    | value_annotation_select
    ;

                                                        // @nodes-add(PATH, NAME)
node_annotation_add_and_select_multiple_nodes           : WHITESPACE+ ATSIGN ANNOTATION_NODES_ADD WHITESPACE* LPAREN WHITESPACE* schema_path WHITESPACE* COMMA WHITESPACE* schema_key WHITESPACE* RPAREN ;
                                                        // @node-add(PATH, NAME)
node_annotation_add_and_select_single_node              : WHITESPACE+ ATSIGN ANNOTATION_NODE_ADD WHITESPACE* LPAREN WHITESPACE* schema_path WHITESPACE* COMMA WHITESPACE* schema_key WHITESPACE* RPAREN ;
                                                        // @nodes-link(SOURCE, TARGET, TARGET_LINK)
node_annotation_link_and_select_multiple_nodes          : WHITESPACE+ ATSIGN ANNOTATION_NODES_LINK WHITESPACE* LPAREN WHITESPACE* schema_path WHITESPACE* COMMA WHITESPACE* schema_path WHITESPACE* COMMA WHITESPACE* schema_key WHITESPACE* RPAREN ;
                                                        // @node-link(SOURCE, TARGET, TARGET_LINK)
node_annotation_link_and_select_single_node             : WHITESPACE+ ATSIGN ANNOTATION_NODE_LINK WHITESPACE* LPAREN WHITESPACE* schema_path WHITESPACE* COMMA WHITESPACE* schema_path WHITESPACE* COMMA WHITESPACE* schema_key WHITESPACE* RPAREN;
                                                        // @nodes-remove(SOURCE, NAME)
node_annotation_remove_and_select_multiple_nodes        : WHITESPACE+ ATSIGN ANNOTATION_NODES_REMOVE WHITESPACE* LPAREN WHITESPACE* schema_path WHITESPACE* COMMA WHITESPACE* schema_key WHITESPACE* RPAREN ;
                                                        // @node-remove(SOURCE, NAME)
node_annotation_remove_and_select_single_node           : WHITESPACE+ ATSIGN ANNOTATION_NODE_REMOVE WHITESPACE* LPAREN WHITESPACE* schema_path WHITESPACE* COMMA WHITESPACE* schema_key WHITESPACE* RPAREN ;
                                                        // @nodes(SOURCE)
node_annotation_select_multiple_nodes                   : WHITESPACE+ ATSIGN ANNOTATION_NODES WHITESPACE* LPAREN WHITESPACE* schema_path? WHITESPACE* RPAREN ;
                                                        // @node(SOURCE)
node_annotation_select_single_node                      : WHITESPACE+ ATSIGN ANNOTATION_NODE WHITESPACE* LPAREN WHITESPACE* schema_path? WHITESPACE* RPAREN ;
                                                        // @nodes-unlink(SOURCE, TARGET, TARGET_LINK)
node_annotation_unlink_and_select_multiple_nodes        : WHITESPACE+ ATSIGN ANNOTATION_NODES_UNLINK WHITESPACE* LPAREN WHITESPACE* schema_path WHITESPACE* COMMA WHITESPACE* schema_path WHITESPACE* COMMA WHITESPACE* schema_key WHITESPACE* RPAREN ;
                                                        // @node-unlink(SOURCE, TARGET, TARGET_LINK)
node_annotation_unlink_and_select_single_node           : WHITESPACE+ ATSIGN ANNOTATION_NODE_UNLINK WHITESPACE* LPAREN WHITESPACE* schema_path WHITESPACE* COMMA WHITESPACE* schema_path WHITESPACE* COMMA WHITESPACE* schema_key WHITESPACE* RPAREN ;
node_annotation
    : node_annotation_add_and_select_multiple_nodes
    | node_annotation_add_and_select_single_node
    | node_annotation_link_and_select_multiple_nodes
    | node_annotation_link_and_select_single_node
    | node_annotation_remove_and_select_multiple_nodes
    | node_annotation_remove_and_select_single_node
    | node_annotation_select_multiple_nodes
    | node_annotation_select_single_node
    | node_annotation_unlink_and_select_multiple_nodes
    | node_annotation_unlink_and_select_single_node
    ;

schema_key                                              : identifier | string_quoted_non_empty;
//schema_path                                             : (identifier | DOUBLEQUOTE | WHITESPACE | FSLASH | BSLASH | HASHTAG | COLON | COLON | ASTERIKS)+ | string_quoted_non_empty; // Should be valid rooted or non rooted path.
schema_path                                             : rooted_path | non_rooted_path ;
comment                                                 : WHITESPACE* COMMENT ;
