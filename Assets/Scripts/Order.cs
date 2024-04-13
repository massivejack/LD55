using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Order", menuName = "Orders/Order")]
public class Order : ScriptableObject
{
    public string name; // Name of the order
    public int powerCost; // Power cost of the order
    public int wordCount; // Number of words in the order
    public Word[] words; // Array of words in the order

  
     // Override Equals method for comparison between two Order objects
    public override bool Equals(object obj)
    {
        if (!(obj is Order))
            return false;

        Order otherOrder = (Order)obj;

        return name == otherOrder.name;
    }

    // Override GetHashCode to match the custom Equals method
    public override int GetHashCode()
    {
        return name.GetHashCode();
    }

    public bool HasAllWords(Order otherOrder)
    {
        // If word counts are different, return false
        if (wordCount != otherOrder.wordCount-otherOrder.CountEmptyWords())
            return false;

        // Sort both arrays of words
        Word[] sortedWords1 = words.OrderBy(w => w).ToArray();
        Word[] sortedWords2 = otherOrder.words.OrderBy(w => w).ToArray();

        // Compare sorted arrays
        for (int i = 0; i < wordCount; i++)
        {
            if (sortedWords1[i] != sortedWords2[i])
                return false;
        }

        return true;
    }

    // Method to count occurrences of Word.Empty
    private int CountEmptyWords()
    {
        int count = 0;
        foreach (Word word in words)
        {
            if (word == Word.Empty)
                count++;
        }
        return count;
    }
}