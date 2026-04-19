using System.Collections.Generic;
using UnityEngine;

public static class Names
{
    public static List<string> firstNames = new List<string>()
    {
        "Glargina",
        "Ziffany",
        "Blorpina",
        "Glurgy",
        "Blibsa",
        "Gooette",
        "Alienetta",
        "Glorb",
        "Flurb",
        "Zarn",
        "Kree",
        "Yerp",
        "Yee",
        "Blip",
        "Gnorts",
        "Zorp",
        "Snurp",
        "John",
        "Jane",
        "Spliffany",
        "Glibberina",
        "Zorbletta",
        "Dante",
        "Zara",
    };

    public static List<string> lastNames = new List<string>()
    {
        "Knorpelstein",
        "Zorbleglorp",
        "Blorpington",
        "Glooblesnort",
        "Wobbleflorp",
        "Snorfleblorp",
        "Zibblezorp",
        "Zorbelstein",
        "von Blorpenberg",
        "Glorpenstein",
        "von Zorble",
        "Smith",
        "Doe",
        "Blorp",
        "Goop",
        "Zoopenheim",
        "Zmith",

    };

    public static List <string> alienLikes = new List<string>() // or dislikes
    {
        "Space Pizza",
        "Mr. Fresh Carbonated Milk",
        "Glorpberries",
        "Stargazing",
        "Interdimensional Travel",
        "Cosmic Karaoke",
        "Zero-G Dance Parties",
        "Asteroid Mining",
        "Alien Soap Operas",
        "Galactic Gardening",
        "Nebula Noodles",
        "AI Slop",
        "Spaceball",
        "Quantum Quiche",
        "Knock Knock Jokes",
        "The Ocean",
        "The Forest",
        "Singularities",
        "Black Holes",
        "Wormholes",
        "Computer Virusses",
        "Space Cats",
        "Space Dogs",
        "Space Hamsters",
        "Chill Guy Float",
        "DRILL, the Video Game",
        "Slimey Dungeon",
        "Potion Panic",
        "Ludum Dare",
        "Dancing",
        "Singing",
        "Cooking",
        "Painting",
        "Sculpting",
        "Naked Gardening",
        "Alien Stand-Up Comedy",
        "Writing",
        "Knitting",
        "Origami",
        "Hiking",
        "Swimming",
        "Skydiving",
        "Naked Skydiving",
        "Bungee Jumping",
        "Traveling",
        "Collecting Rocks",
        "Collecting Stamps",
        "Collecting Alien Artifacts",
        "Shiny Hunting",
        "Speedrunning",
        "Cosmic Bowling",
        "Space Golf",
        "Alien Yoga",
        "Capitalism",
        "Communism",
        "Free Education",
        "Universal Healthcare",
        "First Person Shooters",
        "Real-Time Strategy Games",
        "MMORPGs",
        "Puzzle Games",
        "Platformers",
        "Roguelikes",
        "Visual Novels",
        "Alien Dating Simulators",
        "Cooking Sims",
        "Doing Drugs",
        "Doing Space Drugs",
        "Not Doing Drugs"
    };

    public static string GetRandomFirstName()
    {
        return firstNames[Random.Range(0, firstNames.Count)];
    }

    public static string GetRandomLastName()
    {
        return lastNames[Random.Range(0, lastNames.Count)];
    }

    internal static string GetRandomDislike()
    {
        return alienLikes[Random.Range(0, alienLikes.Count)];
    }

    internal static string GetRandomLike()
    {
        return alienLikes[Random.Range(0, alienLikes.Count)];
    }
}
