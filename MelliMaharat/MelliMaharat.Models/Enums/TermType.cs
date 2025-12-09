using System.ComponentModel;

namespace MelliMaharat.Models.Enums;

public enum TermType
{
    [Description("ترم پاییز")]
    Fall,
    [Description("ترم زمستان")]
    Spring,
    [Description("ترم تابستان")]
    Summer
}
