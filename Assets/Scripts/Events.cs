using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Events : MonoBehaviour
{
    public bool availableForEvent; //whether or not they are available for a 'heart' event

    public string[] eventOneNames;
    public string[] eventTwoNames;
    public string[] eventThreeNames;
    public string[] eventFourNames;
    public string[] eventFiveNames;

    public string[] eventOneDialogue;
    public string[] eventTwoDialogue;
    public string[] eventThreeDialogue;
    public string[] eventFourDialogue;
    public string[] eventFiveDialogue;

    public Sprite[] eventOneSprites;
    public Sprite[] eventTwoSprites;
    public Sprite[] eventThreeSprites;
    public Sprite[] eventFourSprites;
    public Sprite[] eventFiveSprites;

    public AudioClip[] eventOneClips;
    public AudioClip[] eventTwoClips;
    public AudioClip[] eventThreeClips;
    public AudioClip[] eventFourClips;
    public AudioClip[] eventFiveClips;

    //status of the event they are on, #1-5
    public int eventStatus;
    public int maxEventStatus; //max event number, which is 5 for now

    public string[] getCurrentEventNames()
    {
        switch (eventStatus)
        {
            case 1:
                return eventOneNames;
            case 2:
                return eventTwoNames;
            case 3:
                return eventThreeNames;
            case 4:
                return eventFourNames;
            default:
                return eventFiveNames;
        }
    }

    public string[] getCurrentEventDialogue()
    {
        switch (eventStatus)
        {
            case 1:
                return eventOneDialogue;
            case 2:
                return eventTwoDialogue;
            case 3:
                return eventThreeDialogue;
            case 4:
                return eventFourDialogue;
            default:
                return eventFiveDialogue;
        }
    }

    public Sprite[] getCurrentEventSprites()
    {
        switch (eventStatus)
        {
            case 1:
                return eventOneSprites;
            case 2:
                return eventTwoSprites;
            case 3:
                return eventThreeSprites;
            case 4:
                return eventFourSprites;
            default:
                return eventFiveSprites;
        }
    }

    public AudioClip[] getCurrentEventClips()
    {
        switch (eventStatus)
        {
            case 1:
                return eventOneClips;
            case 2:
                return eventTwoClips;
            case 3:
                return eventThreeClips;
            case 4:
                return eventFourClips;
            default:
                return eventFiveClips;
        }
    }

    public void increaseEventLevel()
    {
        eventStatus++;
    }
}
