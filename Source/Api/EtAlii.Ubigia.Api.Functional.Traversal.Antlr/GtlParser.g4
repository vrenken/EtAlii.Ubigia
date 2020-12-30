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
subject_rooted_path                                 : IDENTITY COLON path_part* ;
subject_constant_string                             : STRING_QUOTED ;
subject_constant_object                             : object ;
subject_root                                        : ROOT_SUBJECT_PREFIX COLON IDENTITY ;
subject_variable                                    : DOLLAR IDENTITY ;


// Functions.
subject_function
    : IDENTITY LPAREN RPAREN
    | IDENTITY LPAREN IDENTITY RPAREN
    | IDENTITY LPAREN (IDENTITY COMMA)+ RPAREN
    ;

path_part : (path_part_match | path_part_traverser) ;

path_part_match
    : path_part_matcher_constant
    | PATH_PART_MATCHER_REGEX
    | path_part_matcher_variable
    | path_part_matcher_identifier
    | path_part_matcher_wildcard
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
path_part_traverser_downdates_all                   : LBRACE AST ;                      // {*
path_part_traverser_downdates_oldest                : LBRACE LBRACE ;                   // {{
path_part_traverser_updates                         : RBRACE ;                          // }
path_part_traverser_updates_multiple                : RBRACE INTEGER_LITERAL_UNSIGNED ; // }12
path_part_traverser_updates_all                     : RBRACE AST ;                      // }*
path_part_traverser_updates_newest                  : RBRACE RBRACE ;                   // }}

// Wildcards.
path_part_matcher_wildcard
    : MATCHER_WILDCARD_BEFORE_NONQUOTED
    | MATCHER_WILDCARD_BEFORE_QUOTED_DOUBLE
    | MATCHER_WILDCARD_BEFORE_QUOTED_SINGLE
    | MATCHER_WILDCARD_AFTER_NONQUOTED
    | MATCHER_WILDCARD_AFTER_QUOTED_DOUBLE
    | MATCHER_WILDCARD_AFTER_QUOTED_SINGLE
    ;


// Identifier
path_part_matcher_identifier
    :
    AMP
    GUID DOT
    GUID DOT
    GUID DOT
    INTEGER_LITERAL_UNSIGNED DOT
    INTEGER_LITERAL_UNSIGNED DOT
    INTEGER_LITERAL_UNSIGNED
    ;

// Constant
path_part_matcher_constant
    : STRING_QUOTED
    | STRING_UNQUOTED
    // This is a weird rule. It is needed to get the sample*.txt files operational but might become obsolete later.
    | IDENTITY
    ;
// Variable
path_part_matcher_variable                          : DOLLAR IDENTITY ;

