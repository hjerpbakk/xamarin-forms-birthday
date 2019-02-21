using System;
using Xamarin.Forms.Internals;

namespace Birthdays.Models {
    [Preserve(AllMembers = true)]
    public class Location {
        public Location(string name) {
            Name = name;
        }

        public string Name { get; }

        public override string ToString() => Name;
    }
}
