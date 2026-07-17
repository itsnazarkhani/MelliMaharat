namespace MelliMaharat.Web.ViewModels.Master
{
    public class SetScoreViewModel
    {
        public Guid SelectionId { get; set; }
        public string StudentName { get; set; }
        public decimal CurrentScore { get; set; }
        public decimal Score { get; set; }
        public decimal NewScore { get; set; }
    }
}
