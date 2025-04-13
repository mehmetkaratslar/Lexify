namespace Lexify.Models
{
    public class TestQuestion
    {
        public int WordID { get; set; }
        public required string QuestionText { get; set; }
        public required string CorrectAnswer { get; set; }
        public required List<string> Options { get; set; }
        public string SelectedAnswer { get; set; } = string.Empty;
    }
}
