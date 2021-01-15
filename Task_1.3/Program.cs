using System;
using System.IO;
using Library;

namespace Task_1._3
{
    class Program
    {
        static void Main(string[] args)
        {            
            Menu();
        }

        private static void Menu()
        {
            Console.WriteLine("Created by Kirill");
            Console.WriteLine("This is a simple Notation Tool app");
            Console.WriteLine();

            if (File.Exists("notes.json")) Note.ReadNotesFromFile();

            while (true)
            {                                

                Console.WriteLine("=================================");
                Console.WriteLine("\tMENU");
                Console.WriteLine("=================================");

                Console.WriteLine("Please, choose option:\n");

                Console.WriteLine("1. Search Notes");
                Console.WriteLine("2. View Note");
                Console.WriteLine("3. Create Note");
                Console.WriteLine("4. Delete Note");
                Console.WriteLine("5. Exit");

                int option;
                bool exit = false;

                if(!int.TryParse(Console.ReadLine(),out option) || option > 5 || option < 1)
                {
                    Console.WriteLine("Wrong option!");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    continue;
                }
                Console.Clear();
                switch (option)
                {
                    case 1: SearchNotes();
                        break;
                    case 2: ViewNote(false);
                        break;
                    case 3: CreateNote();
                        break;
                    case 4: DeleteNote();
                        break;
                    case 5: exit = true;
                        break;
                }
                Console.Clear();

                if (exit) break;
            }

            Note.SaveNotesToFile();
        }

        private static void SearchNotes()
        {
            Console.WriteLine("=================================");
            Console.WriteLine("\tSEARCH NOTES");
            Console.WriteLine("=================================");
            Console.WriteLine("Please, enter search phrase(search filter): ");

            var filter = Console.ReadLine();

            Console.WriteLine("----------------------------------");
            var notesList = Note.GetNotes(filter);

            if(notesList.Count == 0)
            {
                Console.WriteLine("No matches found!");
                Console.WriteLine("----------------------------------");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }
            Console.WriteLine(" ID\t\t\tTITLE\t\t\t\tDATE");
            foreach (var note in notesList)
            {
                Console.WriteLine(note);
            }

            Console.WriteLine("----------------------------------");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        private static int ViewNote(bool isCalledFromDeleteNoteMethod)
        {
            if (!isCalledFromDeleteNoteMethod)
            {
                Console.WriteLine("==================================");
                Console.WriteLine("\tVIEW NOTE");
                Console.WriteLine("==================================");               
            }

            Console.WriteLine("Please, enter note Id: ");
            int id;

            while (!int.TryParse(Console.ReadLine().Trim(), out id) || id < 1)
            {
                Console.WriteLine("Wrong Id! Must be a positive integer!");
                Console.WriteLine();
                Console.WriteLine("Please, enter note Id: ");
                continue;
            }

            Note note = Note.FindNote(id);

            if (note == default(Note))
            {
                Console.WriteLine("Note with Id {0}, doesn't exist!", id);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return -1;
            }

            Console.WriteLine("----------------------------------");
            Console.WriteLine($"ID: {note.Id}");
            Console.WriteLine($"Title: {note.Title}");
            Console.WriteLine($"Text: {note.Text}");
            Console.WriteLine($"Creation time: {note.CreatedOn}");
            Console.WriteLine("----------------------------------");

            if (isCalledFromDeleteNoteMethod) return id;

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            return 0;
        }
        private static void CreateNote()
        {
            string text = "";

            Console.WriteLine("==================================");
            Console.WriteLine("\tCREATE NOTE");
            Console.WriteLine("==================================");
            Console.WriteLine("Your text(enter \'q\' when you are done):");
            Console.WriteLine("----------------------------------");

            while (true)
            {
                var input = Console.ReadLine().Trim();
                if (input == "q") break;

                text = string.IsNullOrEmpty(input)? text + "" : text + Environment.NewLine + input;
            }

            Console.WriteLine("----------------------------------");
            if (string.IsNullOrEmpty(text))
            {
                Console.WriteLine("Your note is empty and won't be added!");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            new Note(text);
            Console.WriteLine("Your note was successfully added!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        private static void DeleteNote()
        {
            Console.WriteLine("==================================");
            Console.WriteLine("\tDELETE NOTE");
            Console.WriteLine("==================================");

            int id = ViewNote(true);

            if (id == -1) return;

            Console.WriteLine("Please, enter \"Confirm\" to confirm deletion of note.");
            Console.WriteLine("Or, enter anything else to cancel deletion of note");

            if (Console.ReadLine().Trim().ToUpper() == "CONFIRM")
            {
                Note.DeleteNote(id);
                Console.WriteLine("Note was succesfully deleted!");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
    }
}
