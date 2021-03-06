using System;
using System.Collections.Generic;
using System.Linq;
using Bio;
using FirstFloor.ModernUI.Presentation;

namespace BioinformaticsKKR.Core.Extensions
{
    public static class SequenceExtensions
    {
        public static LinkCollection CreateLinks(this IEnumerable<ISequence> sequences, string basePath, UriKind kind)
        {
            var linkCollection = new LinkCollection();
            var index = 0;

            foreach (var sequence in sequences)
            {
                linkCollection.Add(
                    new Link
                    {
                        DisplayName = sequence.ID,
                        Source = new Uri(string.Format("{0}#{1}", basePath, index), kind),
                    });

                index += 1;
            }

            return linkCollection;
        }

        public static char[] ToCharArray(this ISequence sequence)
        {
            if (sequence == null)
                return null;

            return sequence.Select(v => (char) v).ToArray();
        }
    }
}