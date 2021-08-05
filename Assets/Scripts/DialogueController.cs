using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    public GameObject player;
    public GameObject choicePanel;
    public GameObject dialoguePanel;
    public bool dialogueEnabled;
    public AudioSource audioSource;
    public bool spriteFaded = false;
    public bool eventStarted = false;
    public GameObject HUD;
    public GameObject UIObjForMenuSounds;

    // having to do with character and dialogue
    public GameObject character;
    string[] whoIsTalking;
    string[] dialogue;
    Sprite[] dialogueSprites;
    AudioClip[] audioClips;
    int index = 0; // index that keeps track of where in the array we are for name, dialogue, sprites, sounds
    float Duration = 0.2f; // time it takes to fade a sprite
    float targetTime = 0.6f; // time before you can scroll to next name, dialogue, sprite, sound

    // having to do with dialogue UI
    Text name;
    Text textfield;
    Image sprite;
    Image sprite2;

    // booleans
    bool frozen = false;
    bool textUpdated = false;
    bool spriteOneActive = true;
    bool audioPlayed = false;
	
	void Start() {
        /* initialize UI objects by finding them in scene, we will set these to display name of person who is speaking,
         * dialogue line, and the sprite to display next
         */
        name = GameObject.Find("Name").GetComponent<Text>();
        textfield = GameObject.Find("Dialogue").GetComponent<Text>();
        sprite = GameObject.Find("Sprite").GetComponent<Image>();
        sprite2 = GameObject.Find("Sprite 2").GetComponent<Image>();
    }

	void Update() {
        // if the player clicked on has dialogue enabled
        if (dialogueEnabled)
        {
            targetTime -= Time.deltaTime; // decrease time between switching to next dialogue line

            // if the player is not frozen
            if (!frozen)
            {
                FreezePlayer();
                HideCharacter();
            }

            // set globals to reference names, dialogue, sprites, and sounds
            whoIsTalking = character.GetComponent<CurrentDialogue>().getCurrentDialogueNames();
            dialogue = character.GetComponent<CurrentDialogue>().getCurrentDialogue();
            dialogueSprites = character.GetComponent<CurrentDialogue>().getCurrentSprites();
            audioClips = character.GetComponent<CurrentDialogue>().getCurrentAudioClips();

            // if we have reached the end of the dialogue
            if (index >= dialogue.Length)
            {
                dialoguePanel.GetComponent<PanelFader>().Fade(); // fade the character's dialogue panel
                StartCoroutine("Wait"); // set dialogue panel inactive after 0.5f
                Invoke("SetToFalse", 0.7f); // unfreeze player and set various things to false after dialogue panel has faded
                HUD.SetActive(true); // set HUD active
                HUD.GetComponent<PanelFader>().FadeIfInitiallyShowing(); // fade back in the HUD
                dialogueEnabled = false; // set false now before index so it doesn't show next name, dialogue, sprite, sound
                index = 0; // reset to 0 so that they can re-talk to character
            }
            else
            {
                // if the dialogue text has not been updated by TypeWriterEffect
                if (!textUpdated)
                {
                    // if we are at the beginning of the dialogue
                    if (index == 0)
                    {
                        name.text = whoIsTalking[0]; // name of who is talking
                        this.GetComponent<TypeWriterEffect>().fullText = dialogue[0]; // set text in TypeWriterEffect to display
                        this.GetComponent<TypeWriterEffect>().updateText = true; // tell TypeWriterEffect to update the text
                        textUpdated = true; // set to true so that it does not keep updating over and over

                        // if sprite one is active we want to set it since it is on screen
                        if (spriteOneActive)
                        {
                            sprite.sprite = dialogueSprites[0]; // set sprite 1 to the first sprite
                        } 
                        // otherwise that means sprite 2 is active and we want to set it since it is on screen
                        else
                        {
                            sprite2.sprite = dialogueSprites[0]; // set sprite 2 to the first sprite
                        }

                        // play soundclip if there is one, if audio was not played, and we are NOT supposed to skip current index
                        if (audioClips[index] != null && !audioPlayed)
                        {
                            audioSource.PlayOneShot(audioClips[index]); // play once
                            audioPlayed = true; // set to true to avoid audio spamming every frame
                        }
                    }
                    else
                    {
                        name.text = whoIsTalking[index]; // name of who is talking
                        this.GetComponent<TypeWriterEffect>().fullText = dialogue[index]; // set text in TypeWriterEffect to display
                        this.GetComponent<TypeWriterEffect>().updateText = true; // tell TypeWriterEffect to update the text
                        textUpdated = true; // set to true so that it does not keep updating over and over

                        // if sprite 1 is currently showing on screen
                        if (spriteOneActive)
                        {
                            // only set it if we have a sprite to set it to, otherwise it will stay the same on screen and not switch
                            if (dialogueSprites[index] != null)
                            {
                                StartCoroutine(FadeOut(sprite)); // fade out the current sprite
                                sprite2.sprite = dialogueSprites[index]; // set sprite2 to next sprite
                                StartCoroutine(FadeIn(sprite2)); // fade in new sprite
                                spriteOneActive = false; // spriteOne is no longer active
                            }

                        }
                        // otherwise sprite two is on screen
                        else
                        {
                            // only set it if we have a sprite to set it to, otherwise it will stay the same on screen and not switch
                            if (dialogueSprites[index] != null)
                            {
                                StartCoroutine(FadeOut(sprite2)); // fade out current sprite
                                sprite.sprite = dialogueSprites[index]; // set sprite1 to next sprite
                                StartCoroutine(FadeIn(sprite)); // fade in new sprite
                                spriteOneActive = true; // spriteOne is now active
                            }

                        }

                        // play soundclip if there is one, if audio was not played, and we are NOT supposed to skip current index
                        if (audioClips[index] != null && !audioPlayed)
                        {
                            audioSource.PlayOneShot(audioClips[index]); // play once
                            audioPlayed = true; // set to true to avoid audio spamming every frame
                        }
                    }
                }


                // on mouse click left and if the time between clicks has hit 0
                if (Input.GetMouseButtonDown(0) && targetTime <= 0f)
                {
                    // if the next dialogue line has not reached the end and if the next dialogue is special "SKIP"
                    if (index + 1 < dialogue.Length && dialogue[index + 1] == "SKIP")
                    {
                        dialoguePanel.GetComponent<PanelFader>().Fade(); // fade the character's dialogue panel
                        StartCoroutine("Wait"); // set dialogue panel inactive after 0.5f
                        Invoke("SetToFalse2", 0.7f); // USE SET TO FALSE 2 because it does what SetToFalse does but also updates next sprite
                        HUD.SetActive(true); // set HUD active
                        HUD.GetComponent<PanelFader>().FadeIfInitiallyShowing(); // fade back in the HUD
                        dialogueEnabled = false; // set false now before index so it doesn't show next name, dialogue, sprite, sound
                        index += 2; // skip over "SKIP"
                    } else
                    {
                        index++; // increase index to go to next name, dialogue, sprite, audio
                        textUpdated = false; // set to false so we can update name, dialogue, sprite, audio
                        audioSource.Stop(); // stop current audio clip to avoid overlap
                        audioPlayed = false; // reset audio played to false so next one can be played
                        this.GetComponent<TypeWriterEffect>().updateText = false; // set to false
                        StopCoroutine(this.GetComponent<TypeWriterEffect>().ShowText()); // stop current text scrolling
                        targetTime = 0.6f; // reset for next name, dialogue, sprite, sound
                    }
                    
                }
            } 
        } else
        {
            // print("no dialogue");
        }
	}

    /*
     * This method disables player movement and looking around
     */
	public void FreezePlayer() {
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponentInChildren<MouseLook>().enabled = false;
        frozen = true;
	}

    /*
     * This method enables player movement and looking around
     */
    public void UnfreezePlayer()
    {
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponentInChildren<MouseLook>().enabled = true;
        frozen = false;
    }

    /*
     * This method hides the character you are currently talking to on the field
     */
    public void HideCharacter()
    {
        character.SetActive(false);
    }

    /*
     * This method shows the character you are currently talking to on the field
     */
    public void ShowCharacter()
    {
        character.SetActive(true);
    }

    /*
     * This method waits 0.5 seconds before setting the dialogue panel inactive
     */
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        dialoguePanel.SetActive(false);
    }

    /*
     * This method unfreezes the player, shows the character you are talking to on the field, sets textUpdated to false for the next
     * time so it can update, and turns off TypeWriterEffect
     */
    void SetToFalse()
    {
        UnfreezePlayer();
        ShowCharacter(); // show character on field

        textUpdated = false;
        this.GetComponent<TypeWriterEffect>().updateText = false;
        StopCoroutine(this.GetComponent<TypeWriterEffect>().ShowText());
    }

    /*
     * This method unfreezes the player, shows the character you are talking to on the field, sets textUpdated to false for the next
     * time so it can update, and turns off TypeWriterEffect.
     * ADDITIONALLY since this is only called when we encounter the special "SKIP" dialogue, it also sets the next name, dialogue, 
     * and most importantly takes into account which sprite is going to show when we talk to the character again and sets the next sprite 
     * to THAT one.
     */
    void SetToFalse2()
    {
        UnfreezePlayer();
        ShowCharacter(); // show character on field

        textUpdated = false;
        this.GetComponent<TypeWriterEffect>().updateText = false;
        StopCoroutine(this.GetComponent<TypeWriterEffect>().ShowText());

        name.text = whoIsTalking[index]; // name of who is talking
        this.GetComponent<TypeWriterEffect>().fullText = dialogue[index]; // set text in TypeWriterEffect to display
        this.GetComponent<TypeWriterEffect>().updateText = true; // tell TypeWriterEffect to update the text
        textUpdated = true; // set to true so that it does not keep updating over and over
        
        if (spriteOneActive)
        {
            sprite.sprite = dialogueSprites[index]; // set sprite 1 to the next sprite 
        } else
        {
            sprite2.sprite = dialogueSprites[index]; // set sprite 1 to the first sprite
        }
        
    }

    /*
     * This method fades in a sprite
     */
    public IEnumerator FadeIn(Image sprite)
    {
        float start = 0f, end = 1f;
        float counter = 0f;
        Color curColor = sprite.color;

        while (counter < Duration)
        {
            counter += Time.deltaTime;
            curColor.a = Mathf.Lerp(start, end, counter / Duration);
            sprite.color = curColor;

            yield return null;
        }
    }

    /*
     * This method fades out a sprite
     */
    public IEnumerator FadeOut(Image sprite)
    {
        float start = 1f, end = 0f;
        float counter = 0f;
        Color curColor = sprite.color;

        while (counter < Duration)
        {
            counter += Time.deltaTime;
            curColor.a = Mathf.Lerp(start, end, counter / Duration);
            sprite.color = curColor;

            yield return null;
        }
    }
}
