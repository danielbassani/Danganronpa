using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseClick : MonoBehaviour
{
    public Camera camera;
    public GameObject choicePanel;
    public GameObject HUD;
    public GameObject dialoguePanel;

    GameObject objHit;
    Character character;
    DialogueController dialogueController;
    GameObject characterHit;

    private void Update()
    {
        CheckIfCharacterHit();
    }

    /*
     * This method checks to see if a character is hit each frame.
     * It also deals with setting up/executing character dialogue (either events, or the default dialogue for the scene.
     */
    void CheckIfCharacterHit()
    {
        //on left mouse click
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //whatever was hit is now in here
                objHit = hit.transform.gameObject;

                //if the tag of obj is Character and they ARE available for an event
                if(objHit.tag == "Character" && objHit.GetComponent<Events>().availableForEvent)
                {
                    characterHit = objHit;
                    

                    //if dialogue is NOT enabled, we prompt the user if they want to talk with the character they clicked
                    if (!dialoguePanel.GetComponentInChildren<DialogueController>().dialogueEnabled)
                    {                        
                        //character script
                        character = objHit.GetComponent<Character>();

                        //dialogue controller script attached to panel
                        dialogueController = dialoguePanel.GetComponentInChildren<DialogueController>();
                        dialogueController.character = characterHit;

                        //prompt to hang out
                        HUD.GetComponent<PanelFader>().FadeIfInitiallyShowing();
                        HUD.SetActive(false);
                        TypeWriterEffect t = choicePanel.GetComponentInChildren<TypeWriterEffect>();
                        choicePanel.GetComponentInChildren<TypeWriterEffect>().fullText = "Would you like to hang out with " + characterHit.name + "?";
                        choicePanel.GetComponentInChildren<TypeWriterEffect>().updateText = true;
                        choicePanel.SetActive(true);
                        choicePanel.GetComponent<PanelFader>().Fade();
                        dialogueController.FreezePlayer();
                        characterHit.GetComponent<Collider>().enabled = false;
                        Cursor.lockState = CursorLockMode.None;
                    }
                }
                //this would display whatever default dialogue the character has for this scene
                else if (objHit.tag == "Character")
                {
                    if (!dialoguePanel.GetComponentInChildren<DialogueController>().dialogueEnabled)
                    {
                        characterHit = objHit;

                        //dialogue controller script attached to panel
                        dialogueController = dialoguePanel.GetComponentInChildren<DialogueController>();
                        dialogueController.character = characterHit;

                        //we know it is a character so we should enable their dialogue
                        dialoguePanel.SetActive(true);

                        //set the current names, dialogue, sprites, clips to the scene-specific character dialogue
                        characterHit.GetComponent<CurrentDialogue>().currentDialogueNames = characterHit.GetComponent<SceneSpecificDialogue>().names;
                        characterHit.GetComponent<CurrentDialogue>().currentDialogue = characterHit.GetComponent<SceneSpecificDialogue>().dialogue;
                        characterHit.GetComponent<CurrentDialogue>().currentDialogueSprites = characterHit.GetComponent<SceneSpecificDialogue>().sprites;
                        characterHit.GetComponent<CurrentDialogue>().currentAudioClips = characterHit.GetComponent<SceneSpecificDialogue>().clips;

                        //fade out HUD
                        HUD.GetComponent<PanelFader>().FadeIfInitiallyShowing();
                        HUD.SetActive(false);

                        //fade in panel and enable dialogue boolean
                        dialoguePanel.GetComponent<PanelFader>().Fade();
                        dialoguePanel.GetComponentInChildren<DialogueController>().dialogueEnabled = true;
                    }                 
                }
            }
        }

    }

    //choose to hangout
    public void OnChoiceYes() { 
    
        dialogueController.UIObjForMenuSounds.GetComponent<MenuSounds>().PlaySound("menuSelect");
        StopCoroutine(choicePanel.GetComponentInChildren<TypeWriterEffect>().ShowText());
        choicePanel.GetComponentInChildren<TypeWriterEffect>().updateText = false;
        dialogueController.eventStarted = true;
        characterHit.GetComponent<Collider>().enabled = true;
        dialogueController.HideCharacter();
        Cursor.lockState = CursorLockMode.Locked;
        choicePanel.GetComponent<PanelFader>().Fade();

        Events thisEvent = characterHit.GetComponent<Events>();
        string[] eventNames = thisEvent.getCurrentEventNames();
        string[] eventDialogue = thisEvent.getCurrentEventDialogue();
        Sprite[] eventSprites = thisEvent.getCurrentEventSprites();
        AudioClip[] eventClips = thisEvent.getCurrentEventClips();

        CurrentDialogue currentDialogue = characterHit.GetComponent<CurrentDialogue>();
        currentDialogue.currentDialogueNames = eventNames;
        currentDialogue.currentDialogue = eventDialogue;
        currentDialogue.currentDialogueSprites = eventSprites;
        currentDialogue.currentAudioClips = eventClips;

        //we know it is a character so we should enable their dialogue
        dialoguePanel.SetActive(true);
        dialoguePanel.GetComponent<PanelFader>().Fade();
        dialoguePanel.GetComponentInChildren<DialogueController>().dialogueEnabled = true;
    }

    public void OnChoiceNo()
    {
        dialogueController.UIObjForMenuSounds.GetComponent<MenuSounds>().PlaySound("menuBack");
        StopCoroutine(choicePanel.GetComponentInChildren<TypeWriterEffect>().ShowText());
        choicePanel.GetComponentInChildren<TypeWriterEffect>().updateText = false;
        characterHit.GetComponent<Collider>().enabled = true;
        choicePanel.GetComponent<PanelFader>().Fade();
        HUD.SetActive(true);
        HUD.GetComponent<PanelFader>().FadeIfInitiallyShowing();
        Cursor.lockState = CursorLockMode.Locked;
        dialogueController.UnfreezePlayer();
    }
}
