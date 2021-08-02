using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character: MonoBehaviour
{
    //name of character
    public string name;
    //public bool availableForEvent; //whether or not they are available for a 'heart' event

    /*
    //this method sets each event dialogue 2D array, the current event status, max status, and whether or not they are available for a 'heart' event
    void SetupCharacter(string name)
    {
        //switch statement loads based on name supplied
        switch (name)
        {
            case "Chiaki Nanami":
                //event one names (who is saying which line)
                this.eventOneNames = new string[] { "Hajime Hinata", "Chiaki Nanami", "Chiaki Nanami", "Hajime Hinata", "Chiaki Nanami", "Chiaki Nanami", "Hajime Hinata", "Hajime Hinata",
                "Chiaki Nanami", "Hajime Hinata", "Hajime Hinata"};
                //event one dialogue
                this.eventOneDialogue = new string[] { "Hey Chiaki, g'morning.", "...", "Oh hey Hajime.","Why aren't you gaming like you always do in the mornings?",
                "Hmmmmm...", "Well this place is totally lacking games and stuff, don't you think?", "Well yeah, now that you mention it, there isn't really much to do here...",
                "Unless I can find an arcade machine in the back or something, it really seems boring here.", "Did you say there were games in the back?!", "...",
                "...Nevermind." };

                //event two names (who is saying which line)
                this.eventTwoNames = new string[] { "Hamjime Hinata", "Chiaki Nanami", "Hajime Hinata", "Chiaki Nanami", "Chiaki Nanami", "Hajime Hinata", "Hajime Hinata",
                "Chiaki Nanami", "Hajime Hinata", "Hajime Hinata"};
                //event two dialogue
                this.eventTwoDialogue = new string[] { "Hey Chiaki, g'morning.", "Oh hey Hajime.","Why aren't you gaming like you always do in the mornings?",
                "Hmmmmm...", "Well this place is totally lacking games and stuff, don't you think?", "Well yeah, now that you mention it, there isn't really much to do here...",
                "Unless I can find an arcade machine in the back or something, it really seems boring here.", "Did you say there were games in the back?!", "...",
                "...Nevermind." };

                //event three names (who is saying which line)
                this.eventThreeNames = new string[] { "Hamjime Hinata", "Chiaki Nanami", "Hajime Hinata", "Chiaki Nanami", "Chiaki Nanami", "Hajime Hinata", "Hajime Hinata",
                "Chiaki Nanami", "Hajime Hinata", "Hajime Hinata"};
                //event three dialogue
                this.eventThreeDialogue = new string[] { "Hey Chiaki, g'morning.", "Oh hey Hajime.","Why aren't you gaming like you always do in the mornings?",
                "Hmmmmm...", "Well this place is totally lacking games and stuff, don't you think?", "Well yeah, now that you mention it, there isn't really much to do here...",
                "Unless I can find an arcade machine in the back or something, it really seems boring here.", "Did you say there were games in the back?!", "...",
                "...Nevermind." };

                //event four names (who is saying which line)
                this.eventFourNames = new string[] { "Hamjime Hinata", "Chiaki Nanami", "Hajime Hinata", "Chiaki Nanami", "Chiaki Nanami", "Hajime Hinata", "Hajime Hinata",
                "Chiaki Nanami", "Hajime Hinata", "Hajime Hinata"};
                //event four dialogue
                this.eventFourDialogue = new string[] { "Hey Chiaki, g'morning.", "Oh hey Hajime.","Why aren't you gaming like you always do in the mornings?",
                "Hmmmmm...", "Well this place is totally lacking games and stuff, don't you think?", "Well yeah, now that you mention it, there isn't really much to do here...",
                "Unless I can find an arcade machine in the back or something, it really seems boring here.", "Did you say there were games in the back?!", "...",
                "...Nevermind." };

                //event five names (who is saying which line)
                this.eventFiveNames = new string[] { "Hamjime Hinata", "Chiaki Nanami", "Hajime Hinata", "Chiaki Nanami", "Chiaki Nanami", "Hajime Hinata", "Hajime Hinata",
                "Chiaki Nanami", "Hajime Hinata", "Hajime Hinata"};
                //event five dialogue
                this.eventFiveDialogue = new string[] { "Hey Chiaki, g'morning.", "Oh hey Hajime.","Why aren't you gaming like you always do in the mornings?",
                "Hmmmmm...", "Well this place is totally lacking games and stuff, don't you think?", "Well yeah, now that you mention it, there isn't really much to do here...",
                "Unless I can find an arcade machine in the back or something, it really seems boring here.", "Did you say there were games in the back?!", "...",
                "...Nevermind." };

                this.eventStatus = 1;
                this.maxEventStatus = 5;
                this.availableForEvent = false;
                break;
            case "Sonia Nevermind":
                //event one names (who is saying which line)
                this.eventOneNames = new string[] { "Hajime Hinata", "Chiaki Nanami", "Chiaki Nanami", "Hajime Hinata", "Chiaki Nanami", "Chiaki Nanami", "Hajime Hinata", "Hajime Hinata",
                "Chiaki Nanami", "Hajime Hinata", "Hajime Hinata"};
                //event one dialogue
                this.eventOneDialogue = new string[] { "Hey Chiaki, g'morning.", "...", "Oh hey Hajime.","Why aren't you gaming like you always do in the mornings?",
                "Hmmmmm...", "Well this place is totally lacking games and stuff, don't you think?", "Well yeah, now that you mention it, there isn't really much to do here...",
                "Unless I can find an arcade machine in the back or something, it really seems boring here.", "Did you say there were games in the back?!", "...",
                "...Nevermind." };

                //event two names (who is saying which line)
                this.eventTwoNames = new string[] { "Hamjime Hinata", "Chiaki Nanami", "Hajime Hinata", "Chiaki Nanami", "Chiaki Nanami", "Hajime Hinata", "Hajime Hinata",
                "Chiaki Nanami", "Hajime Hinata", "Hajime Hinata"};
                //event two dialogue
                this.eventTwoDialogue = new string[] { "Hey Chiaki, g'morning.", "Oh hey Hajime.","Why aren't you gaming like you always do in the mornings?",
                "Hmmmmm...", "Well this place is totally lacking games and stuff, don't you think?", "Well yeah, now that you mention it, there isn't really much to do here...",
                "Unless I can find an arcade machine in the back or something, it really seems boring here.", "Did you say there were games in the back?!", "...",
                "...Nevermind." };

                //event three names (who is saying which line)
                this.eventThreeNames = new string[] { "Hamjime Hinata", "Chiaki Nanami", "Hajime Hinata", "Chiaki Nanami", "Chiaki Nanami", "Hajime Hinata", "Hajime Hinata",
                "Chiaki Nanami", "Hajime Hinata", "Hajime Hinata"};
                //event three dialogue
                this.eventThreeDialogue = new string[] { "Hey Chiaki, g'morning.", "Oh hey Hajime.","Why aren't you gaming like you always do in the mornings?",
                "Hmmmmm...", "Well this place is totally lacking games and stuff, don't you think?", "Well yeah, now that you mention it, there isn't really much to do here...",
                "Unless I can find an arcade machine in the back or something, it really seems boring here.", "Did you say there were games in the back?!", "...",
                "...Nevermind." };

                //event four names (who is saying which line)
                this.eventFourNames = new string[] { "Hamjime Hinata", "Chiaki Nanami", "Hajime Hinata", "Chiaki Nanami", "Chiaki Nanami", "Hajime Hinata", "Hajime Hinata",
                "Chiaki Nanami", "Hajime Hinata", "Hajime Hinata"};
                //event four dialogue
                this.eventFourDialogue = new string[] { "Hey Chiaki, g'morning.", "Oh hey Hajime.","Why aren't you gaming like you always do in the mornings?",
                "Hmmmmm...", "Well this place is totally lacking games and stuff, don't you think?", "Well yeah, now that you mention it, there isn't really much to do here...",
                "Unless I can find an arcade machine in the back or something, it really seems boring here.", "Did you say there were games in the back?!", "...",
                "...Nevermind." };

                //event five names (who is saying which line)
                this.eventFiveNames = new string[] { "Hamjime Hinata", "Chiaki Nanami", "Hajime Hinata", "Chiaki Nanami", "Chiaki Nanami", "Hajime Hinata", "Hajime Hinata",
                "Chiaki Nanami", "Hajime Hinata", "Hajime Hinata"};
                //event five dialogue
                this.eventFiveDialogue = new string[] { "Hey Chiaki, g'morning.", "Oh hey Hajime.","Why aren't you gaming like you always do in the mornings?",
                "Hmmmmm...", "Well this place is totally lacking games and stuff, don't you think?", "Well yeah, now that you mention it, there isn't really much to do here...",
                "Unless I can find an arcade machine in the back or something, it really seems boring here.", "Did you say there were games in the back?!", "...",
                "...Nevermind." };
                this.eventStatus = 1;
                this.maxEventStatus = 5;
                this.availableForEvent = true;
                break;
            default:
                break;
        }   
    }

    //this method sets the current dialogue to the current event the player is on when called
    public void SetEvent()
    {
        switch (eventStatus)
        {
            case 1:
                currentDialogueNames = eventOneNames;
                currentDialogue = eventOneDialogue;
                break;
            case 2:
                currentDialogueNames = eventTwoNames;
                currentDialogue = eventTwoDialogue;
                break;
            case 3:
                currentDialogueNames = eventThreeNames;
                currentDialogue = eventThreeDialogue;
                break;
            case 4:
                currentDialogueNames = eventFourNames;
                currentDialogue = eventFourDialogue;
                break;
            case 5:
                currentDialogueNames = eventFiveNames;
                currentDialogue = eventFiveDialogue;
                break;
            default:
                break;
        }
    }

    public bool getAvailableForEvent()
    {
        return availableForEvent;
    }

    public string[] getCurrentDialogueNames()
    {
        return currentDialogueNames;
    }

    public string[] getCurrentDialogue()
    {
        return currentDialogue;
    }

    public string getName()
    {
        return name;
    }

    public int getEventStatus()
    {
        return eventStatus;
    }

    //increases event status by one when called
    public void increaseEventLevel()
    {
        this.eventStatus = (short)(this.eventStatus + 1);
    }
    */
}

