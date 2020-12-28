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

start                   : script ;

script                  : (sequence EOL | sequence)+;

sequence
    : operand                       # OperandOnlySequence
    | operand operator operand      # OperandOperatorOperandSequence
    | operator operand              # OperatorOperandSequence
    | comment                       # CommentSequence
    ;

operand
    : operand_non_rooted_path
    | operand_rooted_path
    ;

operand_non_rooted_path
    :
    (
        ((traverser string)+ traverser) |
        ((traverser string)+)
    )
    ;
operand_rooted_path
    :
        (string ROOT_SEPARATOR string (traverser string)* traverser) |
        (string ROOT_SEPARATOR string (traverser string)*)
    |
        (string ROOT_SEPARATOR string traverser) |
        (string ROOT_SEPARATOR string)
    ;


comment : COMMENT_PREFIX string ;

string
    : STRING_NONQUOTED
    | STRING_QUOTED_SINGLE
    | STRING_QUOTED_DOUBLE
    ;

operator
    : OPERATOR_ASSIGN
    | OPERATOR_ADD
    | OPERATOR_REMOVE
    ;

traverser
    : TRAVERSER_PARENT
    | TRAVERSER_PARENTS_ALL
    | TRAVERSER_CHILDREN
    | TRAVERSER_CHILDREN_ALL
    ;
