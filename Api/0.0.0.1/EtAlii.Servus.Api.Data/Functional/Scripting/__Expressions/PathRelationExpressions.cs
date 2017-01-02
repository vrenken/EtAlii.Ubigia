namespace EtAlii.Servus.Api.Data
{
    using Moppet.Lapa;
    using System;

    public class PathRelationExpressions
    {
        public readonly LpsParser IsChildOf;
        public readonly LpsParser IsParentOf;
        public readonly LpsParser IsNextFor;
        public readonly LpsParser IsPreviousFor;
        public readonly LpsParser IsUpdateFor;
        public readonly LpsParser IsDowndateFor;

        //public readonly LpsParser IsFirstFor;
        //public readonly LpsParser IsLastFor;
        //public readonly LpsParser IsOriginalFor;
        //public readonly LpsParser IsFinalFor;

        public readonly LpsParser LeftSource;
        public readonly LpsParser RightTarget;

        public readonly LpsParser LeftTarget;
        public readonly LpsParser RightSource;

        internal PathRelationExpressions(
            TerminalExpressions terminalExpressions,
            OperatorExpressions operatorExpressions)
        {

            LeftSource = Lp.Term("-");
            RightTarget = Lp.Term("->");

            LeftTarget = Lp.Term("<-");
            RightSource = Lp.Term("-");

            IsParentOf = CreateRelation("IS_PARENT_OF", @"/");
            IsChildOf = CreateRelation("IS_CHILD_OF", @"\");
            IsNextFor = CreateRelation("IS_NEXT_FOR", @"<");
            IsPreviousFor = CreateRelation("IS_PREVIOUS_FOR", @">");
            IsUpdateFor = CreateRelation("IS_UPDATE_FOR", @"{");
            IsDowndateFor = CreateRelation("IS_DOWNDATE_FOR", @"}");

            //IsFirstFor = CreateRelation("IS_FIRST_FOR]", @"<<");
            //IsLastFor = CreateRelation("IS_LAST_FOR]", @">>");
            //IsOriginalFor = CreateRelation("IS_ORIGINAL_FOR]", @"{{");
            //IsFinalFor = CreateRelation("IS_FINAL_FOR]", @"}}");
        }

        private LpsParser CreateRelation(string name, string symbol)
        {
            var labeledName = String.Format(@"[:{0}]", name);
            var labeledSymbol = String.Format(@"[{0}]", symbol);

            return new LpsParser
            {
                Parser = LeftSource + Lp.Term(labeledName) + RightTarget |
                         LeftSource + Lp.Term(labeledSymbol) + RightTarget |
                         Lp.Term(symbol)
            };
        }
    }
}
