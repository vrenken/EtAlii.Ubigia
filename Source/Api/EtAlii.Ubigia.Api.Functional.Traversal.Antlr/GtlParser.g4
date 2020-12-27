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

script                  : WHITESPACE? (NEWLINE? sequence)+ WHITESPACE? EOF;

sequence
    : operand                       # OperandOnlySequence
    | operand operator operand      # OperandOperatorOperandSequence
    | operator operand              # OperatorOperandSequence
    | comment                       # CommentSequence
    | WHITESPACE                    # WhitespaceSequence
    ;

//    : subject                   # SubjectSequencePart
//    | subject operator subject  # OperationSequencePart
//    | subject operator          # OperationSequencePart
//    | COMMENT                   # CommentSequencePart
//    ;
operand_non_rooted_path  : (traverser string)+ traverser? ;
operand_rooted_path
    : string ROOT_SEPARATOR string (traverser string)* traverser?
    | string ROOT_SEPARATOR string traverser?
    ;


operand
    : operand_non_rooted_path
    | operand_rooted_path
//    | string
    ;

comment : COMMENT_PREFIX SPACE? string ;

string
    : (SPACE | WORD | ANY)+
    | STRING_QUOTED_SINGLE
    | STRING_QUOTED_DOUBLE
    ;

operator
    : OPERATOR_ASSIGN
    | OPERATOR_ADD
    | OPERATOR_REMOVE
    ;
//
//operator                : OPERATOR;
//subject
//    : subject_root
//    | subject_function
//    | subject_constant
//    | subject_root_definition
//    | subject_rooted_path
//    | subject_nonrooted_path
//    | subject_variable
//    ;
//
//subject_root            : ROOT (QUOTED_TEXT | NAME);
//subject_function        : ;
//subject_constant
//    : subject_constant_string
//    | subject_constant_object
//    ;
//subject_root_definition : ;
//subject_rooted_path     : TRAVERSER_PARENT NAME (TRAVERSER_PARENT NAME)+ ; // TRAVERSER_PARENT NAME traverser_step+;
//subject_nonrooted_path  : ;
//subject_variable        : ;
//
//
//subject_constant_string : ;
//subject_constant_object : ;
//
////traverser_step          : traversal_action NAME;
//
traverser
    : TRAVERSER_PARENT
    | TRAVERSER_PARENTS_ALL
    | TRAVERSER_CHILDREN
    | TRAVERSER_CHILDREN_ALL
    ;

// OLD HELLO WORLD
//chat                    : line line EOF ;
//line                    : name SAYS opinion NEWLINE;
//name                    : WORD ;
//opinion                 : TEXT_ESCAPED ;
