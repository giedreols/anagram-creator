// See https://aka.ms/new-console-template for more information
using BusinessLogic;
using Cli;



bool repeat = true;

var renderer = new Renderer();

while (repeat)
{
    // ask for word
    var myWord = renderer.GetWord();

    // generate anagrams
    var anagramSolver = new AnagramSolver();
    var anagrams = anagramSolver.GetAnagrams(myWord);

    // show anagrams
    renderer.ShowAnagrams(anagrams);

    // ask for repeating
    repeat = renderer.Repeat();
}