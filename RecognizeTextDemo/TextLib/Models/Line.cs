using System;
using System.Collections.Generic;
using System.Text;

namespace TextLib.Models
{
    public class Line
    {
        public int[] BoundingBox { get; set; }
        public string Text { get; set; }
        public Word[] Words { get; set; }

    }
}
