//namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
//{
//    using System.Collections.Generic;
//    using System.Linq;

//    public class ResultSet 
//    {
//        public string Id { get { return _id; } }
//        private readonly string _id;

//        public int Count { get { return _count; } }
//        private readonly int _count;

//        public bool ShowCount { get { return _showCount; } }
//        private readonly bool _showCount;

//        public IEnumerable<Result> Results { get { return _results; } }
//        private readonly IEnumerable<Result> _results;

//        public ResultSet(
//            string id, 
//            IEnumerable<Result> results)
//        {
//            _id = id;
//            _results = results;
//            _count = results.Count();
//            _showCount = _count > 1;
//        }
//    }
//}
