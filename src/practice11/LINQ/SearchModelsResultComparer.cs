namespace LINQ
{
    using System.Collections.Generic;

    public sealed class SearchModelsResultComparer : IComparer<SearchModelsResult>
    {
        public int Compare(SearchModelsResult x, SearchModelsResult y)
        {
            var minx = GetMinYear(x.Years);
            var miny = GetMinYear(y.Years);

            return minx.CompareTo(miny);
        }

        private int GetMinYear(int[] years)
        {
            var min = years[0];
            for (int i = 1; i < years.Length; i++)
            {
                if (years[i] < min)
                {
                    min = years[i];
                }
            }

            return min;
        }
    }
}
