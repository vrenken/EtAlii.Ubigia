namespace EtAlii.Servus.Api.Data
{
    using Remotion.Linq.Clauses;
    using Remotion.Linq.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class __QueryLinesAggregator
    {
        public __QueryLinesAggregator()
        {
            FromParts = new List<string>();
            WhereParts = new List<string>();
            OrderByParts = new List<string>();
        }

        public string SelectPart { get; set; }
        private List<string> FromParts { get; set; }
        private List<string> WhereParts { get; set; }
        private List<string> OrderByParts { get; set; }

        public void AddFromPart(IQuerySource querySource)
        {
            FromParts.Add(string.Format("{0} as {1}", GetEntityName(querySource), querySource.ItemName));
        }

        public void AddWherePart(string formatString, params object[] args)
        {
            WhereParts.Add(string.Format(formatString, args));
        }

        public void AddOrderByPart(IEnumerable<string> orderings)
        {
            OrderByParts.Insert(0, String.Join(", ", orderings));
        }

        public string BuildGqlString()
        {
            var stringBuilder = new StringBuilder();

            if (string.IsNullOrEmpty(SelectPart) || FromParts.Count == 0)
                throw new InvalidOperationException("A query must have a select part and at least one from part.");

            stringBuilder.AppendFormat("select {0}", SelectPart);
            stringBuilder.AppendFormat(" from {0}", String.Join(", ", FromParts));

            if (WhereParts.Count > 0)
                stringBuilder.AppendFormat(" where {0}", String.Join(" and ", WhereParts));

            if (OrderByParts.Count > 0)
                stringBuilder.AppendFormat(" order by {0}", String.Join(", ", OrderByParts));

            return stringBuilder.ToString();
        }

        private string GetEntityName(IQuerySource querySource)
        {
            throw new NotImplementedException();
            //return NHibernateUtil.Entity(querySource.ItemType).Name;
        }
    }
}