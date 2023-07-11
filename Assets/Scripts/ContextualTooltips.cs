using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Shows an ordered list of messages via a text mesh
/// </summary>
public class ContextualTooltips : MonoBehaviour
{
    [Tooltip("The text mesh the message is output to")]
    public TextMeshProUGUI messageOutput = null;

    [Tooltip("What happens once the list is completed")] 
    public UnityEvent OnComplete = new UnityEvent();

    [Tooltip("The list of Tutorial tips that are shown. Id range 0 to n")]
    public List<TutorialTip> tutorialTips = new List<TutorialTip>();

    
    private int index = 0;
    private bool beenEnabled;

    [Serializable]
    public class TutorialTip
    {
        public int id;
        public string message;
        public bool completed;
    }
    
    private void Start()
    {
        ShowMessage();
    }
    

    public void NextMessage()
    {
        int newIndex = ++index % tutorialTips.Count;

        if (newIndex < index)
        {
            OnComplete.Invoke();
        }
        else
        {
            ShowMessage();
        }
    }

    public void PreviousMessage()
    {
        index = --index % tutorialTips.Count;
        ShowMessage();
    }

    private void ShowMessage()
    {
        //messageOutput.text = messages[Mathf.Abs(index)];
        messageOutput.text = tutorialTips[Mathf.Abs(index)].message;

    }

    public void ShowMessageAtIndex(int value)
    {
        if (tutorialTips[value].completed != false)
            return;
        
        index = value;
        ShowMessage();
    }

    public void CompleteTip(int value)
    {
        //if( messageIndex != 0 && tipDict[messageIndex-1] == true)
            //tipDict[messageIndex] = true;
            
            tutorialTips[value].completed = true;
            
        //if(messageIndex != tipDict.Count)
            ShowMessageAtIndex(value + 1);
        
        //Debug.Log("Dictionary: " + value);

        //Debug.Log("Dictionary: " + tipDict[value]);
    }
    
    public void EnableTipCanvas(GameObject tipGameObject)
    {
        if (!beenEnabled)
        {
            tipGameObject.SetActive(true);
            beenEnabled = true;
        }
    }
}
