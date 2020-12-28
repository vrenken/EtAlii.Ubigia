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

start
    : script
    ;

script
    :
    (
        sequence EOL |
        sequence comment EOL |
        comment EOL |
        sequence |
        sequence comment |
        comment
    )+
    ;

comment : COMMENT ;

sequence
    : operand                       # OperandOnlySequence
    | operand OPERATOR operand      # OperandOperatorOperandSequence
    | OPERATOR operand              # OperatorOperandSequence
    ;

operand
    : operand_non_rooted_path
    | operand_rooted_path
    ;

operand_non_rooted_path : path_part+ ;

operand_rooted_path : root path_part* ;

root: ROOT;

path_part : (path_part_match | path_part_traverser) ;

path_part_match
    : PATH_PART_MATCHER_CONSTANT
    | PATH_PART_MATCHER_REGEX
    | PATH_PART_MATCHER_VARIABLE
    | PATH_PART_MATCHER_IDENTIFIER
    | PATH_PART_MATCHER_WILDCARD
    ;

path_part_traverser
    : PATH_PART_TRAVERSER_PARENT      // Hierarchical
    | PATH_PART_TRAVERSER_CHILDREN
    | PATH_PART_TRAVERSER_PREVIOUS_SINGLE    // Sequential
    | PATH_PART_TRAVERSER_PREVIOUS_MULTIPLE
    | PATH_PART_TRAVERSER_PREVIOUS_FIRST
    | PATH_PART_TRAVERSER_NEXT_SINGLE
    | PATH_PART_TRAVERSER_NEXT_MULTIPLE
    | PATH_PART_TRAVERSER_NEXT_LAST
    | PATH_PART_TRAVERSER_DOWNDATE    // Temporal
    | PATH_PART_TRAVERSER_DOWNDATES
    | PATH_PART_TRAVERSER_DOWNDATES_ALL
    | PATH_PART_TRAVERSER_DOWNDATES_OLDEST
    | PATH_PART_TRAVERSER_UPDATE
    | PATH_PART_TRAVERSER_UPDATES
    | PATH_PART_TRAVERSER_UPDATES_ALL
    | PATH_PART_TRAVERSER_UPDATES_NEWEST
    ;
