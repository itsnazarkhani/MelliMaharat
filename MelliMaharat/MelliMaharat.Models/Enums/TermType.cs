using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MelliMaharat.Models.Enums
{
    public enum TermType
    {
        [Description("ترم پاییز")]
        Fall,
        [Description("ترم زمستان")]
        Winter,
        [Description("ترم تابستان")]
        Summer
    }
}
