using UnityEngine;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI currentPowerText;
    public TextMeshProUGUI invokedOrderText;

    private WordSlotManager wordSlotManager;

    private void Start()
    {
        // Get the WordSlotManager component from the scene
        wordSlotManager = FindObjectOfType<WordSlotManager>();
    }

    // Called when a new word is added
    public void OnNewWordAdded()
    {
        Debug.Log("UI Manager: New word added event invoked.");

        // Update invokedOrderText with words from WordSlotManager
        UpdateInvokedOrderTextWithWords();
    }

    // Called when an order is invoked
    public void OnOrderInvoked()
    {
        Debug.Log("UI Manager: Order invoked event invoked.");
        UpdateInvokedOrderTextWithOrder();
    }

    private void UpdateInvokedOrderTextWithOrder()
    {
        if (wordSlotManager == null)
        {
            Debug.LogWarning("UI Manager: WordSlotManager not found.");
            return;
        }

        // Clear the text first
        invokedOrderText.text = "";

        //set text to invoked order
        invokedOrderText.text = wordSlotManager.currentInvokedOrder.name;
        invokedOrderText.color = Color.red;
    }

    // Update the invokedOrderText to feature all words from WordSlots
    // Omit the word "Empty"
    private void UpdateInvokedOrderTextWithWords()
    {
        if (wordSlotManager == null)
        {
            Debug.LogWarning("UI Manager: WordSlotManager not found.");
            return;
        }

        // Clear the text first
        invokedOrderText.text = "";

        // Loop through all WordSlots
        foreach (Word slot in wordSlotManager.wordSlots)
        {
            // Skip empty slots
            if (slot.Equals(Word.Empty))
                continue;

            // Add the word to the text, with a '+' between words
            invokedOrderText.text += slot.ToString() + " + ";
        }

        // Remove the last " + " from the text
        if (!string.IsNullOrEmpty(invokedOrderText.text))
            invokedOrderText.text = invokedOrderText.text.Substring(0, invokedOrderText.text.Length - 3);
        else
            invokedOrderText.text = "Empty";

        invokedOrderText.color = Color.yellow;
    }
}
