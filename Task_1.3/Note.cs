using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Library;
using System.IO;
using System.Linq;

namespace Task_1._3
{
    public class Note : INote
    {
        [NonSerialized]
        private static List<Note> _notes = new List<Note>(0);

        [JsonProperty("id")]
        public int Id { get; private set; }

        [JsonProperty("title")]
        public string Title { get; }

        [JsonProperty("text")]
        public string Text { get; }

        [JsonProperty("createdOn")]
        public DateTime CreatedOn { get; }

        public Note(string text)
        {
            if (File.Exists("notes.json") && _notes.Count != 0) Id = _notes.Select(n => n.Id).Max() + 1;
            else Id = 1;

            Title = text.Length >= 32 ? text.Substring(0, 32).Replace("\r\n", " ").Trim() : text.Substring(0, text.Length).Replace("\r\n", " ").Trim();
            Text = string.Concat(text,"");
            CreatedOn = DateTime.UtcNow;

            _notes.Add(this);
            SaveNotesToFile();
        }
        
        public static void DeleteNote(int id)
        {
            _notes.Remove(FindNote(id));
            foreach (var note in _notes)
            {
                if (note.Id > id) note.Id--;
            }
            SaveNotesToFile();
        }

        public static void SaveNotesToFile()
        {
            var json = JsonConvert.SerializeObject(_notes,Formatting.Indented);
            FileWorker.Write("notes.json", json);
        }

        public static void ReadNotesFromFile()
        {
            var json = FileWorker.Read("notes.json");
            _notes = JsonConvert.DeserializeObject<List<Note>>(json);
        }

        public static List<Note> GetNotes(string filter)
        {            
            var noteList = _notes.Where(
                n => (n.Id.ToString() + n.Text + n.Title + n.CreatedOn.ToString()).ToUpper().Contains(filter.ToUpper())
                ).ToList<Note>();
            return noteList;
        }

        public static Note FindNote(int id)
        {
            var note = _notes.Find(n => n.Id == id);
            return note;
        }

        public override string ToString()
        {
            return $" {Id.ToString()}\t \"{Title}\"\t [{CreatedOn.ToString()}]";
        }
      
    }
}
