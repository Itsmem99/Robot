namespace Robot.Models
{
    public class MoodEntry
    {
        public int Id { get; set; }          // Primärnyckel
        public string Text { get; set; }     // Beskrivning
        public string Emoji { get; set; }    // Emoji-symbol
    }
}
