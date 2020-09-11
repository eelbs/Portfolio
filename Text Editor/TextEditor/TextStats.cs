using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace TextEditor
{
    class TextStats
    {
        string text;
        public TextStats(string txt)
        {
            text = txt;
            
        }

        public int GetCharCount()
        {
            return text.Length - 1;
        }
        public int GetWordCount()
        {
            int wordCount = 0, index = 0;
            
            // skip whitespace until first word
            while (index < text.Length && char.IsWhiteSpace(text[index]))
                index++;

            while (index < text.Length)
            {
                // check if current char is part of a word
                while (index < text.Length && !char.IsWhiteSpace(text[index]))
                    index++;

                wordCount++;

                // skip whitespace until next word
                while (index < text.Length && char.IsWhiteSpace(text[index]))
                    index++;
            }
            return wordCount;
        }

        public int spaceCount()
        {
            int Count = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == ' ')
                {
                    Count += 1;
                }
            }
            return Count;
        }

        public string PoorAdjectivesString()
        {
            int goodCount = new Regex(Regex.Escape("good")).Matches(text).Count;
            int badCount = new Regex(Regex.Escape("bad")).Matches(text).Count;
            int thingsCount = new Regex(Regex.Escape("things")).Matches(text).Count;
            int niceCount = new Regex(Regex.Escape("nice")).Matches(text).Count;
            int stuffCount = new Regex(Regex.Escape("stuff")).Matches(text).Count;
            int coolCount = new Regex(Regex.Escape("cool")).Matches(text).Count;
            if ((goodCount+badCount+thingsCount+niceCount)>0)
            {
                string retString = "Times adjectives which are usually imporvable are used: \n";
                if(goodCount>0)
                {
                    retString += "Good: " + goodCount + '\n';
                }
                if(badCount>0)
                {
                    retString += "Bad: " + badCount + '\n';
                }
                if(thingsCount>0)
                {
                    retString += "Things: " + thingsCount + '\n';
                }
                if(niceCount>0)
                {
                    retString += "Nice: " + niceCount + '\n';
                }
                if (stuffCount > 0)
                {
                    retString += "Stuff: " + stuffCount + '\n';
                }
                if (coolCount > 0)
                {
                    retString += "Cool: " + coolCount + '\n';
                }
                return retString;
                
            }
            return string.Empty;
        }

        public string BadWordCountString()
        {
            int fCount = new Regex(Regex.Escape("fuck")).Matches(text).Count;
            int sCount = new Regex(Regex.Escape("shit")).Matches(text).Count;
            if(fCount+sCount>0)
            {
                return "Bad words are used: " + (fCount + sCount).ToString() + " times.";
            }
            return string.Empty;
        }

    }
}
