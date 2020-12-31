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

start
    : script
    ;

script: sequence+ ;

comment : COMMENT ;

sequence
    : subject operator subject comment EOL
    | subject operator subject comment
    | subject operator subject EOL
    | subject operator subject
    | operator subject comment EOL
    | operator subject comment
    | operator subject EOL
    | operator subject
    | subject comment EOL
    | subject comment
    | subject EOL
    | subject
    | comment EOL
    | comment
    ;

operator
    : LCHEVR EQUALS #OperatorAssign
    | PLUS EQUALS #OperatorAdd
    | MINUS EQUALS #OperatorRemove
    ;

subject
    : subject_non_rooted_path
    | subject_rooted_path
    | subject_constant_string
    | subject_constant_object
    | subject_variable
    | subject_function
    | subject_root
    ;

subject_non_rooted_path                             : path_part+ ;
subject_rooted_path                                 : IDENTIFIER COLON path_part* ;
subject_constant_string                             : STRING_QUOTED ;
subject_constant_object                             : object ;
subject_root                                        : ROOT_SUBJECT_PREFIX COLON IDENTIFIER ;
subject_variable                                    : DOLLAR IDENTIFIER ;


// Functions.
subject_function
    : IDENTIFIER LPAREN RPAREN
    | IDENTIFIER LPAREN IDENTIFIER RPAREN
    | IDENTIFIER LPAREN (IDENTIFIER COMMA)+ RPAREN
    ;

path_part : (path_part_traverser | path_part_match) ;

path_part_match
    : path_part_matcher_identifier
    | path_part_matcher_constant
    | PATH_PART_MATCHER_REGEX
    | path_part_matcher_variable
    | path_part_matcher_wildcard
    | path_part_matcher_tag
    ;

path_part_traverser
    : path_part_traverser_parent      // Hierarchical
    | path_part_traverser_parents_all
    | path_part_traverser_children
    | path_part_traverser_children_all
    | path_part_traverser_previous_single    // Sequential
    | path_part_traverser_previous_multiple
    | path_part_traverser_previous_first
    | path_part_traverser_next_single
    | path_part_traverser_next_multiple
    | path_part_traverser_next_last
    | path_part_traverser_downdate    // Temporal
    | path_part_traverser_downdates_multiple
    | path_part_traverser_downdates_all
    | path_part_traverser_downdates_oldest
    | path_part_traverser_updates
    | path_part_traverser_updates_multiple
    | path_part_traverser_updates_all
    | path_part_traverser_updates_newest
    ;

// Hierarchical
path_part_traverser_parent                          : FSLASH ;                          // /
path_part_traverser_parents_all                     : FSLASH FSLASH;                    // //
path_part_traverser_children                        : BSLASH ;                          // \
path_part_traverser_children_all                    : BSLASH BSLASH ;                   // \\

// Sequential
path_part_traverser_previous_single                 : LCHEVR ;                          // <
path_part_traverser_previous_multiple               : LCHEVR INTEGER_LITERAL_UNSIGNED ; // <12
path_part_traverser_previous_first                  : LCHEVR LCHEVR ;                   // <<
path_part_traverser_next_single                     : RCHEVR ;                          // >
path_part_traverser_next_multiple                   : RCHEVR INTEGER_LITERAL_UNSIGNED ; // >12
path_part_traverser_next_last                       : RCHEVR RCHEVR ;                   // >>

// Temporal
path_part_traverser_downdate                        : LBRACE ;                          // {
path_part_traverser_downdates_multiple              : LBRACE INTEGER_LITERAL_UNSIGNED ; // {12
path_part_traverser_downdates_all                   : LBRACE ASTERIKS ;                 // {*
path_part_traverser_downdates_oldest                : LBRACE LBRACE ;                   // {{
path_part_traverser_updates                         : RBRACE ;                          // }
path_part_traverser_updates_multiple                : RBRACE INTEGER_LITERAL_UNSIGNED ; // }12
path_part_traverser_updates_all                     : RBRACE ASTERIKS ;                 // }*
path_part_traverser_updates_newest                  : RBRACE RBRACE ;                   // }}

// Wildcards.
path_part_matcher_wildcard
    : matcher_wildcard
    | matcher_wildcard_before_nonquoted
    | matcher_wildcard_before_quoted_double
    | matcher_wildcard_before_quoted_single
    | matcher_wildcard_after_nonquoted
    | matcher_wildcard_after_quoted_double
    | matcher_wildcard_after_quoted_single
    ;

matcher_wildcard                                    : ASTERIKS ;
matcher_wildcard_before_nonquoted                   : ASTERIKS NO_NEWLINE+? ;
matcher_wildcard_before_quoted_double               : DOUBLEQUOTE ASTERIKS (  ESCAPED_DOUBLEQUOTE | NO_DOUBLEQUOTE | NO_NEWLINE)*? DOUBLEQUOTE ;
matcher_wildcard_before_quoted_single               : SINGLEQUOTE ASTERIKS (  ESCAPED_SINGLEQUOTE | NO_SINGLEQUOTE | NO_NEWLINE)*? SINGLEQUOTE ;
matcher_wildcard_after_nonquoted                    : NO_NEWLINE+? NO_LBRACES ASTERIKS ;
matcher_wildcard_after_quoted_double                : DOUBLEQUOTE (  ESCAPED_DOUBLEQUOTE | NO_DOUBLEQUOTE | NO_NEWLINE)*? NO_LBRACES ASTERIKS DOUBLEQUOTE ;
matcher_wildcard_after_quoted_single                : SINGLEQUOTE (  ESCAPED_SINGLEQUOTE | NO_SINGLEQUOTE | NO_NEWLINE)*? NO_LBRACES ASTERIKS SINGLEQUOTE ;


// Identifier
path_part_matcher_identifier                        : UBIGIA_IDENTIFIER ;

path_part_matcher_tag_name_only                     : IDENTIFIER HASHTAG ;
path_part_matcher_tag_tag_only                      : HASHTAG IDENTIFIER ;
path_part_matcher_tag_and_name                      : IDENTIFIER HASHTAG IDENTIFIER ;
path_part_matcher_tag
    : path_part_matcher_tag_name_only
    | path_part_matcher_tag_tag_only
    | path_part_matcher_tag_and_name
    ;

// Constant
path_part_matcher_constant_quoted                   : STRING_QUOTED ;
path_part_matcher_constant_unquoted                 : STRING_UNQUOTED ;
path_part_matcher_constant_identifier               : IDENTIFIER ;
path_part_matcher_constant
    : path_part_matcher_constant_quoted
    | path_part_matcher_constant_unquoted
    | path_part_matcher_constant_identifier
    ;

// Variable
path_part_matcher_variable                          : DOLLAR IDENTIFIER ;

