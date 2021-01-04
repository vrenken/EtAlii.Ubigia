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

import GtlPrimitives ;

script: sequence+ EOF;

comment : COMMENT ;

sequence
    : subject operator subject comment NEWLINE?
    | subject operator subject NEWLINE?
    | operator subject comment NEWLINE?
    | operator subject NEWLINE?
    | subject comment NEWLINE?
    | subject NEWLINE?
    | comment NEWLINE?
    ;

operator_assign : LCHEVR EQUALS ;
operator_add : PLUS EQUALS ;
operator_remove : MINUS EQUALS ;
operator
    : operator_assign
    | operator_add
    | operator_remove
    ;

subject
    : subject_constant_object
    | subject_non_rooted_path
    | subject_rooted_path
    | subject_root_definition
    | subject_constant_string
    | subject_variable
    | subject_function
    | subject_root
    ;

subject_non_rooted_path                             : path_part+ ;
subject_rooted_path                                 : identifier COLON path_part* ;
subject_constant_object                             : object ;
subject_constant_string                             : string_quoted ;
subject_root                                        : ROOT_SUBJECT_PREFIX COLON identifier;
subject_variable                                    : DOLLAR identifier ;
subject_root_definition                             : identifier (DOT identifier)* ;

// Functions.
subject_function
    : identifier LPAREN RPAREN
    | identifier LPAREN (subject_function_argument COMMA)*? subject_function_argument RPAREN
    ;

subject_function_argument
    : subject_function_argument_string_quoted
    | subject_function_argument_identifier
    | subject_function_argument_variable
    | subject_function_argument_rooted_path
    | subject_function_argument_non_rooted_path
    ;

subject_function_argument_identifier                : identifier ;
subject_function_argument_string_quoted             : string_quoted ;
subject_function_argument_variable                  : DOLLAR identifier ;
subject_function_argument_non_rooted_path           : path_part+ ;
subject_function_argument_rooted_path               : identifier COLON path_part*;

path_part : (path_part_traverser | path_part_match) ;

path_part_match
    : path_part_matcher_identifier
    | path_part_matcher_constant
    | path_part_matcher_variable
    | path_part_matcher_wildcard
    | path_part_matcher_traversing_wildcard
    | path_part_matcher_tag
    | path_part_matcher_typed
    | path_part_matcher_regex
    ;

path_part_traverser
    : path_part_traverser_parents_all      // Hierarchical
    | path_part_traverser_parent
    | path_part_traverser_children_all
    | path_part_traverser_children
    | path_part_traverser_previous_first    // Sequential
    | path_part_traverser_previous_multiple
    | path_part_traverser_previous_single
    | path_part_traverser_next_last
    | path_part_traverser_next_multiple
    | path_part_traverser_next_single
    | path_part_traverser_downdates_oldest    // Temporal
    | path_part_traverser_downdates_multiple
    | path_part_traverser_downdates_all
    | path_part_traverser_downdate
    | path_part_traverser_updates_newest
    | path_part_traverser_updates_multiple
    | path_part_traverser_updates_all
    | path_part_traverser_updates
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
matcher_wildcard                                    : ASTERIKS ;
matcher_wildcard_before_nonquoted                   : ASTERIKS NO_NEWLINE+? ;
matcher_wildcard_before_quoted_double               : DOUBLEQUOTE ASTERIKS ( NO_DOUBLEQUOTE | NO_NEWLINE)*? DOUBLEQUOTE ;
matcher_wildcard_before_quoted_single               : SINGLEQUOTE ASTERIKS ( NO_SINGLEQUOTE | NO_NEWLINE)*? SINGLEQUOTE ;
matcher_wildcard_after_nonquoted                    : NO_NEWLINE+? NO_LBRACES ASTERIKS ;
matcher_wildcard_after_quoted_double                : DOUBLEQUOTE ( NO_DOUBLEQUOTE | NO_NEWLINE)*? NO_LBRACES ASTERIKS DOUBLEQUOTE ;
matcher_wildcard_after_quoted_single                : SINGLEQUOTE ( NO_SINGLEQUOTE | NO_NEWLINE)*? NO_LBRACES ASTERIKS SINGLEQUOTE ;
path_part_matcher_wildcard
    : matcher_wildcard
    | matcher_wildcard_before_nonquoted
    | matcher_wildcard_before_quoted_double
    | matcher_wildcard_before_quoted_single
    | matcher_wildcard_after_nonquoted
    | matcher_wildcard_after_quoted_double
    | matcher_wildcard_after_quoted_single
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

