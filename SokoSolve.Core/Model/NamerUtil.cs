using System;
using System.Collections.Generic;
using System.Text;

namespace SokoSolve.Core.Model
{
    class NamerUtil
    {
        public string[] Colours = new string[] { "Red", "Green", "Blue", "Black", "White", "Yellow"};
        public string[] Advectives = new string[]
            {
                "Big", "Small", "Tense", "Tight", "Exacting", "Tricky", "Slimy", "Nefarious", "Ghastly", "Grim", "Funny",
                "Delighted", "Dastardly","Hot", "Cold", "Tepid", "Splendid", "Superb", "Grand", "Old", "Ghastly", "Dire", 
                "Edgy", "Uptight"
            };
        public string[] Nouns = new string[]
            {
                "Nemosis", "Tower", "Grave","Tunnel", "School", "Mountain", "Gem", "Spider", "Toad", "Estate",
                "Night", "Morning", "House", "Lagoon", "Blanket", "Hovel", "Hamlet", "Town", "Cottage", "Shower"
            };

        public string[] Names = new string[]
            {
                "Neville's", "Bob's", "Leigh's", "Susan's", "Wilma's", "David's", "Andrew's", "Lucy's", "Cat's", "Caroline's"
            };
        
        public string MakeName()
        {
            if (RandomHelper.RandomBool(10))
            {
                return RandomHelper.Select<string>(Names) + " " + RandomHelper.Select<string>(Nouns);
            }

            if (RandomHelper.RandomBool(5))
            {
                return RandomHelper.Select<string>(Advectives) + " " + RandomHelper.Select<string>(Colours) + " " + RandomHelper.Select<string>(Nouns);
            }
            return RandomHelper.Select<string>(Advectives) + " " + RandomHelper.Select<string>(Nouns);
        }
    }
}
