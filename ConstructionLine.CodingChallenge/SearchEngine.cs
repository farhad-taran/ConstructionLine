using System;
using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    public class SearchEngine
    {
        private readonly List<Shirt> _shirts;

        public SearchEngine(List<Shirt> shirts)
        {
            _shirts = shirts ?? throw new ArgumentNullException(nameof(shirts));
        }

        public SearchResults Search(SearchOptions options)
        {
            var matchingShirts = _shirts.Where(shirt =>
                    (!options.Colors.Any() || options.Colors.Contains(shirt.Color)) &&
                    (!options.Sizes.Any() || options.Sizes.Contains(shirt.Size)))
                .ToList();

            var colorCounts = Color.All.Select(color => new ColorCount
            {
                Color = color,
                Count = matchingShirts.Count(shirt => shirt.Color.Id == color.Id)
            }).ToList();

            var sizeCounts = Size.All.Select(size => new SizeCount
            {
                Size = size,
                Count = matchingShirts.Count(shirt => shirt.Size.Id == size.Id)
            }).ToList();

            return new SearchResults
            {
                Shirts = matchingShirts,
                ColorCounts = colorCounts,
                SizeCounts = sizeCounts
            };
        }
    }
}