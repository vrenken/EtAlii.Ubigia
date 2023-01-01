parser grammar SchemaParser;

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

import Primitives, PathParser, ScriptParser;

schema                                                  : (comment | WHITESPACE | NEWLINE)* header_option_namespace? structure_fragment? (comment | WHITESPACE | NEWLINE)* EOF ;

namespace                                               : schema_key (DOT schema_key)* ;
header_option_namespace                                 : WHITESPACE* LBRACK WHITESPACE* HEADER_OPTION_NAMESPACE WHITESPACE* EQUALS WHITESPACE* namespace WHITESPACE* RBRACK NEWLINE;
//header_option_context                                 : WHITESPACE* LBRACK WHITESPACE* HEADER_OPTION_CONTEXT WHITESPACE* EQUALS WHITESPACE* identity WHITESPACE* RBRACK NEWLINE;

requirement                                             : EXCLAMATION | QUESTION ;

structure_fragment_body_entry
    : structure_fragment
    | value_mutation_fragment
    | value_query_fragment
    | comment WHITESPACE* NEWLINE+ (WHITESPACE | NEWLINE)*
    ;

structure_plurality                                     : LBRACK RBRACK ;

mutation_value_primitive                                : primitive_value ;
mutation_value_variable                                 : DOLLAR (identifier | reserved_words) ;

mutation_value
    : mutation_value_variable
    | mutation_value_primitive
    ;

structure_fragment                                      : WHITESPACE* requirement? schema_key structure_plurality? WHITESPACE* (EQUALS WHITESPACE* node_annotation)? WHITESPACE* (WHITESPACE | NEWLINE)* LBRACE (WHITESPACE | NEWLINE)* structure_fragment_body? (WHITESPACE | NEWLINE)* RBRACE;
value_query_fragment                                    : WHITESPACE* schema_key_prefix? schema_key WHITESPACE* (EQUALS WHITESPACE* value_annotation)? WHITESPACE* ;
value_mutation_fragment                                 : WHITESPACE* schema_key_prefix? schema_key WHITESPACE* EQUALS WHITESPACE* mutation_value WHITESPACE* ;

schema_key_prefix_type_and_requirement_1                : value_type WHITESPACE* requirement ;
schema_key_prefix_type_and_requirement_2                : value_type requirement WHITESPACE* ;
schema_key_prefix_type_only                             : value_type WHITESPACE* ;
schema_key_prefix_requirement                           : requirement ;
schema_key_prefix
    : schema_key_prefix_type_and_requirement_1
    | schema_key_prefix_type_and_requirement_2
    | schema_key_prefix_type_only
    | schema_key_prefix_requirement
    ;

value_type
    : VALUE_TYPE_OBJECT
    | VALUE_TYPE_STRING
    | VALUE_TYPE_BOOL
    | VALUE_TYPE_FLOAT
    | VALUE_TYPE_INT
    | VALUE_TYPE_DATETIME
    | VALUE_TYPE_GUID
    ;

structure_fragment_body_newline_separated               : (structure_fragment_body_entry WHITESPACE* comment? NEWLINE+)* structure_fragment_body_entry comment? (WHITESPACE | NEWLINE)* ;
structure_fragment_body_comma_separated                 : (structure_fragment_body_entry WHITESPACE* COMMA WHITESPACE* comment? NEWLINE?)* structure_fragment_body_entry comment? (WHITESPACE | NEWLINE)* ;
structure_fragment_body
    : structure_fragment_body_newline_separated
    | structure_fragment_body_comma_separated
    ;
                                                        // @node-set(SOURCE, VALUE)
value_annotation_assign_and_select_with_key             : WHITESPACE+ ATSIGN ANNOTATION_NODE_SET WHITESPACE* LPAREN WHITESPACE* schema_path WHITESPACE* COMMA WHITESPACE* schema_key WHITESPACE* RPAREN ;
                                                        // @node-set(SOURCE)
value_annotation_assign_and_select_without_key          : WHITESPACE+ ATSIGN ANNOTATION_NODE_SET WHITESPACE* LPAREN WHITESPACE* schema_key WHITESPACE* RPAREN ;
                                                        // @value-clear(SOURCE)
value_annotation_clear_and_select_with_key              : WHITESPACE+ ATSIGN ANNOTATION_NODE_CLEAR WHITESPACE* LPAREN WHITESPACE* schema_path WHITESPACE* COMMA WHITESPACE* schema_key WHITESPACE* RPAREN ;
                                                        // @value-clear()
value_annotation_clear_and_select_without_key           : WHITESPACE+ ATSIGN ANNOTATION_NODE_CLEAR WHITESPACE* LPAREN WHITESPACE* RPAREN ;
                                                        // @value-clear(SOURCE)
value_annotation_clear_and_select                       : WHITESPACE+ ATSIGN ANNOTATION_NODE_CLEAR WHITESPACE* LPAREN WHITESPACE* schema_path WHITESPACE* RPAREN ;
                                                        // @value(SOURCE) or @value()
value_annotation_select                                 : WHITESPACE+ ATSIGN ANNOTATION_NODE WHITESPACE* LPAREN WHITESPACE* schema_path? WHITESPACE* RPAREN ;

value_annotation_select_current_node
    : ATSIGN                                                       // @
    | ATSIGN ANNOTATION_NODE WHITESPACE* LPAREN WHITESPACE* RPAREN // @node()
    ;

// Structure.
value_annotation_map_sequence                            : WHITESPACE+ sequence ;

value_annotation
    : value_annotation_assign_and_select_with_key
    | value_annotation_assign_and_select_without_key
    | value_annotation_clear_and_select_with_key
    | value_annotation_clear_and_select_without_key
    | value_annotation_clear_and_select
    | value_annotation_select
    | value_annotation_select_current_node
    | value_annotation_map_sequence
    ;

node_identity
    : node_identity_literal
    | node_identity_variable
    ;

node_identity_literal                                   : identifier | string_quoted_non_empty | reserved_words ;
node_identity_variable                                  : DOLLAR (identifier | reserved_words) ;

                                                        // @node-link(SOURCE, TARGET, TARGET_LINK)
node_annotation_link_and_select_single_node             : ATSIGN ANNOTATION_NODE_LINK WHITESPACE* LPAREN WHITESPACE* schema_path WHITESPACE* COMMA WHITESPACE* schema_path WHITESPACE* COMMA WHITESPACE* schema_path WHITESPACE* RPAREN;
                                                        // @nodes-remove(SOURCE, NAME)
node_annotation_remove_and_select_multiple_nodes        : ATSIGN ANNOTATION_NODES_REMOVE WHITESPACE* LPAREN WHITESPACE* schema_path WHITESPACE* COMMA WHITESPACE* schema_key WHITESPACE* RPAREN ;
                                                        // @node-remove(SOURCE, NAME)
node_annotation_remove_and_select_single_node           : ATSIGN ANNOTATION_NODE_REMOVE WHITESPACE* LPAREN WHITESPACE* schema_path WHITESPACE* COMMA WHITESPACE* schema_key WHITESPACE* RPAREN ;
                                                        // @nodes-unlink(SOURCE, TARGET, TARGET_LINK)
node_annotation_unlink_and_select_multiple_nodes        : ATSIGN ANNOTATION_NODES_UNLINK WHITESPACE* LPAREN WHITESPACE* schema_path WHITESPACE* COMMA WHITESPACE* schema_path WHITESPACE* COMMA WHITESPACE* schema_path WHITESPACE* RPAREN ;
                                                        // @node-unlink(SOURCE, TARGET, TARGET_LINK)
node_annotation_unlink_and_select_single_node           : ATSIGN ANNOTATION_NODE_UNLINK WHITESPACE* LPAREN WHITESPACE* schema_path WHITESPACE* COMMA WHITESPACE* schema_path WHITESPACE* COMMA WHITESPACE* schema_path WHITESPACE* RPAREN ;
                                                        // @nodes-add(PATH, NAME)
node_annotation_add_and_select_multiple_nodes           : ATSIGN ANNOTATION_NODES_ADD WHITESPACE* LPAREN WHITESPACE* schema_path WHITESPACE* COMMA WHITESPACE* node_identity WHITESPACE* RPAREN ;
//                                                        // @node-add(PATH)
//node_annotation_add_and_select_single_node_without_key  : ATSIGN ANNOTATION_NODE_ADD WHITESPACE* LPAREN WHITESPACE* schema_path WHITESPACE* RPAREN ;
                                                        // @node-add(PATH, NAME)
node_annotation_add_and_select_single_node              : ATSIGN ANNOTATION_NODE_ADD WHITESPACE* LPAREN WHITESPACE* schema_path WHITESPACE* COMMA WHITESPACE* node_identity WHITESPACE* RPAREN ;
                                                        // @nodes-link(SOURCE, TARGET, TARGET_LINK)
node_annotation_link_and_select_multiple_nodes          : ATSIGN ANNOTATION_NODES_LINK WHITESPACE* LPAREN WHITESPACE* schema_path WHITESPACE* COMMA WHITESPACE* schema_path WHITESPACE* COMMA WHITESPACE* schema_path WHITESPACE* RPAREN ;
                                                        // @nodes(SOURCE)
node_annotation_select_multiple_nodes                   : ATSIGN ANNOTATION_NODES WHITESPACE* LPAREN WHITESPACE* schema_path? WHITESPACE* RPAREN ;
                                                        // @node(SOURCE)
node_annotation_select_single_node                      : ATSIGN ANNOTATION_NODE WHITESPACE* LPAREN WHITESPACE* schema_path WHITESPACE* RPAREN ;

node_annotation_select_current_node
    : ATSIGN                                                       // @
    | ATSIGN ANNOTATION_NODE WHITESPACE* LPAREN WHITESPACE* RPAREN // @node()
    ;

// Structure.
node_annotation_map_sequence                            : WHITESPACE+ sequence ;
node_annotation
    : node_annotation_add_and_select_multiple_nodes
    | node_annotation_add_and_select_single_node
//    | node_annotation_add_and_select_single_node_without_key
    | node_annotation_link_and_select_multiple_nodes
    | node_annotation_link_and_select_single_node
    | node_annotation_remove_and_select_multiple_nodes
    | node_annotation_remove_and_select_single_node
    | node_annotation_select_multiple_nodes
    | node_annotation_select_current_node
    | node_annotation_select_single_node
    | node_annotation_unlink_and_select_multiple_nodes
    | node_annotation_unlink_and_select_single_node
    | node_annotation_map_sequence
    ;

schema_key                                              : identifier | string_quoted_non_empty | reserved_words ;
schema_path                                             : rooted_path | non_rooted_path ;
comment                                                 : WHITESPACE* COMMENT ;
