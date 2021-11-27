namespace BackupsExtra.Models
{
    public enum HybridVariation
    {
        /// <summary>Delete RestorePoint if it doesn't fit at least one limit</summary>
        OneLimit = 1,

        /// <summary>Delete RestorePoint if it doesn't fit all limits</summary>
        AllLimits,
    }
}