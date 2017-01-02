namespace EtAlii.Servus.Api.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class StaticPathSubjectProcessor : ISubjectProcessor
    {
        public object Process(ProcessParameters<Subject, SequencePart> parameters)
        {
            var result = new List<string>();

            var path = (PathSubject)parameters.Target;
            var parts = path.Parts;
            foreach(var part in path.Parts)
            {
                if (part is IsParentOfPathSubjectPart)
                {
                    continue;
                }
                else if (part is ConstantPathSubjectPart)
                {
                    result.Add(((ConstantPathSubjectPart)part).Name);
                }
                else
                {
                    var message = String.Format("Unable to process static path '{0}' (part: {1}", path.ToString(), part.ToString());
                    throw new ScriptProcessingException(message);
                }
            }
            return result.ToArray();
        }
    }
}
