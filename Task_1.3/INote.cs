using System;
using System.Collections.Generic;
using System.Text;

namespace Task_1._3
{
    public interface INote
    {
        public int Id { get; }
        public string Title { get; }
        public string Text { get; }
        public DateTime CreatedOn { get; }
    }
}
