namespace Common.Core.Generic.DynamicQueryFilter.DynamicExpressions
{
    /// <summary>
    /// This class defines the dynamic field prefix names for QueryFilters, such as those used in the ClientDynamicFieldsQueryFilter class.
    /// </summary>
    public class DynamicFiltersConfiguration
    {
        /// <summary>
        /// The name of the assembly containing the model definitions.
        /// </summary>
        public string AssemblyModelName { get; set; }

        /// <summary>
        /// Array of prefixes used in dynamic field filtering.
        /// For example, the ClientDynamicFieldsQueryFilter class may have properties prefixed with Max, Min, From, To, Contains, and List.
        /// </summary>
        public string[] PREFIXES { get => [Max, Min, From, To, Contains, List]; }

        /// <summary>
        /// Prefix for maximum value filters.
        /// </summary>
        public string Max { get; set; }

        /// <summary>
        /// Prefix for minimum value filters.
        /// </summary>
        public string Min { get; set; }

        /// <summary>
        /// Prefix for start date or range filters.
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// Prefix for end date or range filters.
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// Prefix for contains or substring filters.
        /// </summary>
        public string Contains { get; set; }

        /// <summary>
        /// Prefix for list or collection filters.
        /// </summary>
        public string List { get; set; }
    }
}
