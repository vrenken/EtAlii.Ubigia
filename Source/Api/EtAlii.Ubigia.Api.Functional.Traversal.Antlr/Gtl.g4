
grammar Gtl;

@header {
    //using System.Collections.Generic;
    //using System.Diagnostics.CodeAnalysis;

    #pragma warning disable CS0115 // CS0115: no suitable method found to override
    #pragma warning disable CS3021 // CS3021: The CLSCompliant attribute is not needed because the assembly does not have a CLSCompliant attribute
    // ReSharper disable all
}

options {
     language = CSharp;
     //namespace = EtAlii.Ubigia.Api.Functional.Traversal.Antlr;
}

/*
 * Parser Rules
 */

chat                : line line EOF ;
line                : name SAYS opinion NEWLINE;
name                : WORD ;
opinion             : TEXT ;

/*
 * Lexer Rules
 */

fragment A          : ('A'|'a') ;
fragment S          : ('S'|'s') ;
fragment Y          : ('Y'|'y') ;

fragment LOWERCASE  : [a-z] ;
fragment UPPERCASE  : [A-Z] ;

SAYS                : S A Y S ;
WORD                : (LOWERCASE | UPPERCASE)+ ;
TEXT                : '"' .*? '"' ;
WHITESPACE          : (' '|'t')+ -> skip ;
NEWLINE             : ('r'? 'n' | 'r')+ ;
