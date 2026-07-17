namespace MelliMaharat.Models.Enums;

public enum Departments : byte
{
    //None
    /// <summary>You Can Simply Use `default` instead of Departments.None</summary>
    [Description("You Can Simply Use `default` instead of Departments.None")]
    None,

    // Engineering & Technology
    ///<summary>Civil Engineering (مهندسی عمران)</summary>
    [Description("Civil Engineering (مهندسی عمران)")]
    CE,      
    ///<summary>Mechanical Engineering (مهندسی مکانیک)</summary>
    [Description("Mechanical Engineering (مهندسی مکانیک)")]
    ME,
    ///<summary>Electrical Engineering (مهندسی برق)</summary>
    [Description("Electrical Engineering (مهندسی برق)")]
    EE,
    ///<summary>Chemical Engineering (مهندسی شیمی)</summary>
    [Description("Chemical Engineering (مهندسی شیمی)")]
    ChE,     
    ///<summary>Industrial Engineering (مهندسی صنایع)</summary>
    [Description("Industrial Engineering (مهندسی صنایع)")]
    IE,
    ///<summary>Computer Science & Engineering (مهندسی کامپیوتر)</summary>
    [Description("Computer Science & Engineering (مهندسی کامپیوتر)")]
    CSE,     
    ///<summary>Information Technology (فناوری اطلاعات)</summary>
    [Description("Information Technology (فناوری اطلاعات)")]
    IT,
    ///<summary>Aerospace Engineering (مهندسی هوافضا)</summary>
    [Description("Aerospace Engineering (مهندسی هوافضا)")]
    AE,
    ///<summary>Materials Science and Engineering (مهندسی مواد)</summary>
    [Description("Materials Science and Engineering (مهندسی مواد)")]
    MSE,     

    // Science
    ///<summary>Physics (فیزیک)</summary>
    [Description("Physics (فیزیک)")]
    PHYS,    
    ///<summary>Chemistry (شیمی)</summary>
    [Description("Chemistry (شیمی)")]
    CHEM,    
    ///<summary>Biology (زیست‌شناسی)</summary>
    [Description("Biology (زیست‌شناسی)")]
    BIO,     
    ///<summary>Mathematics (ریاضیات)</summary>
    [Description("Mathematics (ریاضیات)")]
    MATH,    
    ///<summary>Statistics (آمار)</summary>
    [Description("Statistics (آمار)")]
    STAT,    
    ///<summary>Geology (زمین‌شناسی)</summary>
    [Description("Geology (زمین‌شناسی)")]
    GEO,     

    //Humanities & Social Sciences
    ///<summary>Psychology (روان‌شناسی)</summary>
    [Description("Psychology (روان‌شناسی)")]
    PSY,     
    ///<summary>Sociology (جامعه‌شناسی)</summary>
    [Description("Sociology (جامعه‌شناسی)")]
    SOC,     
    ///<summary>Philosophy (فلسفه)</summary>
    [Description("Philosophy (فلسفه)")]
    PHIL,    
    ///<summary>History (تاریخ)</summary>
    [Description("History (تاریخ)")]
    HIS,     
    ///<summary>Literature (ادبیات)</summary>
    [Description("Literature (ادبیات)")]
    LIT,     
    ///<summary>Law (حقوق)</summary>
    [Description("Law (حقوق)")]
    LAW,     
    ///<summary>Political Science (علوم سیاسی)</summary>
    [Description("Political Science (علوم سیاسی)")]
    POLSCI,  
    ///<summary>International Relations (روابط بین‌الملل)</summary>
    [Description("International Relations (روابط بین‌الملل)")]
    IR,

    //Business & Management
    ///<summary>Master of Business Administration (مدیریت اجرایی)</summary>
    [Description("Master of Business Administration (مدیریت اجرایی)")]
    MBA,    
    ///<summary>Management (مدیریت)</summary>
    [Description("Management (مدیریت)")]
    MGT,     
    ///<summary>Accounting (حسابداری)</summary>
    [Description("Accounting (حسابداری)")]
    ACC,   
    ///<summary>Finance (مالی)</summary>
    [Description("Finance (مالی)")]
    FIN,    
    ///<summary>Economics (اقتصاد)</summary>
    [Description("Economics (اقتصاد)")]
    ECO,    

    //Education & Arts
    /// <summary>Education (علوم تربیتی)</summary>
    [Description("Education (علوم تربیتی)")]
    EDU,   
    /// <summary>Fine Arts (هنرهای زیبا)</summary>
    [Description("Fine Arts (هنرهای زیبا)")]
    FA,
    /// <summary>Music (موسیقی)</summary>
    [Description("Music (موسیقی)")]
    MUS,    
    /// <summary>Architecture (معماری)</summary>
    [Description("Architecture (معماری)")]
    ARCH,
    /// <summary>Urban and Regional Planning (برنامه‌ریزی شهری و منطقه‌ای)</summary>
    [Description("Urban and Regional Planning (برنامه‌ریزی شهری و منطقه‌ای)")]
    URP
}
