using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

// Enum for the words
public enum Word
{
    Feed,
    Kill,
    Pray,
    Breed,
    Empty
}

public class WordSlotManager : MonoBehaviour
{
    public int maxWordSlots = 3; // Maximum number of word slots
    public Word[] wordSlots; // Array to store the words in each word slot
    public KeyCode[] keyBindings; // Key bindings for adding words
    public List<Order> allOrders; // List of all Order scriptable objects
    public Order currentInvokedOrder; // Current invoked order

    // Define events
    public UnityEvent newWordAddedEvent;
    public UnityEvent orderInvokedEvent;

    private void Start()
    {
        wordSlots = new Word[maxWordSlots]; // Initialize word slots array
        ClearWords();
    }

    private void Update()
    {
        // Check key inputs for adding words
        for (int i = 0; i < keyBindings.Length; i++)
        {
            if (Input.GetKeyDown(keyBindings[i]))
            {
                AddWord((Word)i);
            }
        }

        // Check if space key is pressed to invoke order
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeOrder();
        }
    }

    // Function to add a word to the word slots
    private void AddWord(Word newWord)
    {
        // If only 1 word slot is available, replace the old word with the new one
        if (maxWordSlots == 1)
        {
            wordSlots[0] = newWord;
        }
        else
        {
            // If more than 1 word slot is available, shift words to the right
            for (int i = maxWordSlots - 1; i > 0; i--)
            {
                wordSlots[i] = wordSlots[i - 1];
            }
            // Add the new word to the first word slot
            wordSlots[0] = newWord;
        }

        //invoke event
        newWordAddedEvent.Invoke();

        // Combine debug info into one message
        string debugInfo = "Word Slots: ";
        for (int i = 0; i < maxWordSlots; i++)
        {
            debugInfo += "[" + (i + 1) + ": " + wordSlots[i] + "] ";
        }
        Debug.Log(debugInfo);
    }

    // Function to invoke an order based on the current word combination
    private void InvokeOrder()
    {
        // Check if there is an order with the same word combination
        foreach (Order order in allOrders)
        {
            if (order.HasAllWords(GetCurrentOrder()))
            {
                currentInvokedOrder = order;
                orderInvokedEvent.Invoke();
                Debug.Log("Invoked order: " + currentInvokedOrder.name); // Debug print the invoked order             
                break;
            }
        }
        ClearWords();
    }

    // Function to get the current order from the word slots
    private Order GetCurrentOrder()
    {
        Order currentOrder = ScriptableObject.CreateInstance<Order>();
        currentOrder.name = "CurrentOrder"; // Assign a temporary name for comparison

        // Fill the current order with the words from the word slots
        currentOrder.wordCount = maxWordSlots;
        currentOrder.words = new Word[maxWordSlots];
        for (int i = 0; i < maxWordSlots; i++)
        {
            currentOrder.words[i] = wordSlots[i];
        }

        return currentOrder;
    }

    // Function to clear all word slots
    private void ClearWords()
    {
        for (int i = 0; i < maxWordSlots; i++)
        {
            wordSlots[i] = Word.Empty;
        }
      //  Debug.Log("Words cleared, all set to Empty");
    }

#if UNITY_EDITOR
    [ContextMenu("Load Orders")]
    public void LoadOrders()
    {
        allOrders.Clear();

        string folderPath = "Assets/Scripts/Orders";
        string[] guids = AssetDatabase.FindAssets("t:Order", new[] { folderPath });
        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            Order order = AssetDatabase.LoadAssetAtPath<Order>(assetPath);
            if (order != null)
            {
                allOrders.Add(order);
            }
        }
    }
#endif
}
