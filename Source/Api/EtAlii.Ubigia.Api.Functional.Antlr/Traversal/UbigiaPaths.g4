parser grammar UbigiaPaths;

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

import GtlPrimitives ;

non_rooted_path                                     : path_part+ ;
rooted_path                                         : identifier COLON path_part* ;

path_part
    : path_part_matcher_traversing_wildcard
    | path_part_matcher_wildcard
    | path_part_matcher_tag
    | path_part_matcher_conditional
    | path_part_matcher_constant
    | path_part_matcher_variable
    | path_part_matcher_identifier
    | path_part_traverser_parents_all      // Hierarchical
    | path_part_traverser_parent
    | path_part_traverser_children_all
    | path_part_traverser_children
    | path_part_traverser_downdates_oldest    // Temporal
    | path_part_traverser_downdates_multiple
    | path_part_traverser_downdates_all
    | path_part_traverser_downdate
    | path_part_traverser_updates_newest
    | path_part_traverser_updates_multiple
    | path_part_traverser_updates_all
    | path_part_traverser_updates
    | path_part_traverser_previous_multiple    // Sequential
    | path_part_traverser_previous_first
    | path_part_traverser_previous_single
    | path_part_traverser_next_last
    | path_part_traverser_next_multiple
    | path_part_traverser_next_single
    | path_part_matcher_typed
    | path_part_matcher_regex
    ;

// Hierarchical
path_part_traverser_parents_all                     : FSLASH FSLASH;                    // //
path_part_traverser_parent                          : FSLASH ;                          // /
path_part_traverser_children_all                    : BSLASH BSLASH ;                   // \\
path_part_traverser_children                        : BSLASH ;                          // \

// Sequential
path_part_traverser_previous_first                  : LCHEVR LCHEVR ;                   // <<
path_part_traverser_previous_single                 : LCHEVR ;                          // <
path_part_traverser_previous_multiple               : LCHEVR integer_literal_unsigned ; // <12
path_part_traverser_next_last                       : RCHEVR RCHEVR ;                   // >>
path_part_traverser_next_single                     : RCHEVR ;                          // >
path_part_traverser_next_multiple                   : RCHEVR integer_literal_unsigned ; // >12

// Temporal
path_part_traverser_downdates_oldest                : LBRACE LBRACE ;                   // {{
path_part_traverser_downdates_all                   : LBRACE ASTERIKS ;                 // {*
path_part_traverser_downdate                        : LBRACE ;                          // {
path_part_traverser_downdates_multiple              : LBRACE integer_literal_unsigned ; // {12
path_part_traverser_updates_newest                  : RBRACE RBRACE ;                   // }}
path_part_traverser_updates_all                     : RBRACE ASTERIKS ;                 // }*
path_part_traverser_updates                         : RBRACE ;                          // }
path_part_traverser_updates_multiple                : RBRACE integer_literal_unsigned ; // }12

// Identifier
path_part_matcher_identifier                        : UBIGIA_IDENTIFIER ;

path_part_matcher_typed                             : LBRACK identifier RBRACK ;

// Wildcards.
matcher_wildcard_quoted                             : string_quoted_non_empty? ASTERIKS string_quoted_non_empty? ;
matcher_wildcard_nonquoted                          : identifier? ASTERIKS identifier? ;
path_part_matcher_wildcard
    : matcher_wildcard_quoted
    | matcher_wildcard_nonquoted
    ;

path_part_matcher_traversing_wildcard : ASTERIKS integer_literal_unsigned ASTERIKS ;

path_part_matcher_tag_name_only                     : identifier HASHTAG ;
path_part_matcher_tag_tag_only                      : HASHTAG identifier ;
path_part_matcher_tag_and_name                      : identifier HASHTAG identifier ;
path_part_matcher_tag
    : path_part_matcher_tag_name_only
    | path_part_matcher_tag_tag_only
    | path_part_matcher_tag_and_name
    ;

// Constant
path_part_matcher_constant_quoted                   : string_quoted_non_empty ;
path_part_matcher_constant_unquoted                 : (LETTER | DOT | DIGIT)+ ;
path_part_matcher_constant_identifier               : identifier ;
path_part_matcher_constant_integer                  : integer_literal_unsigned ;
path_part_matcher_constant
    : path_part_matcher_constant_quoted
    | path_part_matcher_constant_unquoted
    | path_part_matcher_constant_identifier
    | path_part_matcher_constant_integer
    ;

// Regex.
path_part_matcher_regex                             : LBRACK string_quoted_non_empty RBRACK ;

// Variable
path_part_matcher_variable                          : DOLLAR identifier ;

// Conditional
path_part_matcher_conditional                       : DOT (path_part_matcher_condition AMPERSAND)* path_part_matcher_condition ;

path_part_matcher_condition                         : path_part_matcher_property WHITESPACE* path_part_matcher_condition_comparison WHITESPACE* path_part_matcher_value ;

path_part_matcher_property
    : identifier
    | string_quoted_non_empty
    ;

path_part_matcher_value
    : string_quoted
    | string_quoted_non_empty
    | datetime
    | timespan
    | float_literal
    | float_literal_unsigned
    | integer_literal
    | integer_literal_unsigned
    | boolean_literal
    ;

path_part_matcher_condition_comparison
    : EXCLAMATION? EQUALS
    | LCHEVR EQUALS?
    | RCHEVR EQUALS?
    ;
