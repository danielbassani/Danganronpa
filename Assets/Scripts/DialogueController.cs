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

    //having to do with character and dialogue
    public GameObject character;
    string[] whoIsTalking;
    string[] dialogue;
    Sprite[] dialogueSprites;
    AudioClip[] audioClips;

    //having to do with dialogue UI
    Text name;
    Text textfield;
    Image sprite;
    Image sprite2;

    int maxIndex;
    int index;
    bool frozen = false;
    bool audioPlayed = false;
    bool spriteOneActive = true;
    bool textUpdated = false;
    bool SKIPwasHit = false;
	bool hitLast = false;
    float Duration = 0.2f;
    float targetTime = 0.6f; //time before you can skip dialogue

    // Start is called before the first frame update
    void Start()
    {
        name = GameObject.Find("Name").GetComponent<Text>();
        textfield = GameObject.Find("Dialogue").GetComponent<Text>();
        sprite = GameObject.Find("Sprite").GetComponent<Image>();
        sprite2 = GameObject.Find("Sprite 2").GetComponent<Image>();
        /*
        whoIsTalking = character.GetComponent<CharacterProperties>().whoIsTalking;
        dialogue = character.GetComponent<CharacterProperties>().dialogue;
        dialogueSprites = character.GetComponent<CharacterProperties>().dialogueSprites;
        audioClips = character.GetComponent<CharacterProperties>().soundClips;
        maxIndex = dialogue.Length;
        */
        index = 0;
    }

    

    // Update is called once per frame
    void Update()
    {
        if (dialogueEnabled)
        {
            whoIsTalking = character.GetComponent<CurrentDialogue>().currentDialogueNames;
            dialogue = character.GetComponent<CurrentDialogue>().currentDialogue;
            dialogueSprites = character.GetComponent<CurrentDialogue>().currentDialogueSprites;
            audioClips = character.GetComponent<CurrentDialogue>().currentAudioClips;
            maxIndex = dialogue.Length;

            if (index == 0)
            {
                sprite.sprite = dialogueSprites[index];
            }

            //to avoid out of bounds errors, we do nothing if index exceeds bounds
            if(index > maxIndex)
            {
                //do nothing
				print("here");
            }
            else
            {
                //timer decreased until < 0 when the player is allowed to scroll dialogue again
                targetTime -= Time.deltaTime;

                //freeze the player
                if (!frozen)
                {
                    FreezePlayer();
                    HideCharacter();
                }

                if (!textUpdated)
                {
                    string personSpeaking = whoIsTalking[index];
                    string speakingDialogue = dialogue[index];

                    //check if special SKIP tag encountered
                    if (speakingDialogue == "SKIP")
                    {
                        //SKIPwasHit set to true and reset dialogue
                        SKIPwasHit = true;
                        speakingDialogue = "";
                    }
                    else
                    {
                        name.text = personSpeaking;
                        this.GetComponent<TypeWriterEffect>().fullText = speakingDialogue;
                        this.GetComponent<TypeWriterEffect>().updateText = true;
                        textUpdated = true;
                    }
                }


                if (dialogueSprites[index] == null)
                {
                    //do nothing
					// print("nothing");
                }
                //display sprite if current index is not null, sprite has not faded, and if the current index is NOT supposed to be skipped
                else if (dialogueSprites[index] != null && !spriteFaded && !SKIPwasHit)
                {	
                    //if sprite one is currently on screen
                    if (spriteOneActive)
                    {
						print("fading sprite one");
                        //fade out sprite one
                        StartCoroutine(FadeOut(sprite));

                        sprite2.sprite = dialogueSprites[index];

                        //fade in sprite two
						if (!hitLast) {
							StartCoroutine(FadeIn(sprite2));
						}
                        
                        spriteOneActive = false; //sprite two is now on screen
                        spriteFaded = true; //sprite has faded, used to not scroll sprites too fast
                    }
                    //if sprite two is currently on screen
                    else
                    {	
						print("fading sprite two");
                        //fade out sprite 2
                        StartCoroutine(FadeOut(sprite2));

                        sprite.sprite = dialogueSprites[index];

                        //fade in sprite one
						if (!hitLast) {
							StartCoroutine(FadeIn(sprite));
						}
                        
                        spriteOneActive = true; //sprite one is now on screen
                        spriteFaded = true; //sprite has faded, used to not scroll sprites too fast
                    }
                }
				
				

                //play soundclip if there is one, if audio was not played, and we are NOT supposed to skip current index
                if (audioClips[index] != null && !audioPlayed && !SKIPwasHit)
                {
                    audioSource.PlayOneShot(audioClips[index]);
                    audioPlayed = true;
                }

                //if user presses left mouse, index < max, the time delay between clicks has decreased to 0 and lower or we hit a SKIP tag
                if (Input.GetMouseButtonDown(0) && targetTime <= 0f && index < maxIndex && !SKIPwasHit)
                {
                    UIObjForMenuSounds.GetComponent<MenuSounds>().PlaySound("textConfirm");
					if (index + 1 != dialogue.Length && dialogue[index + 1] != "SKIP") {
						index++;
						audioSource.Stop();
						audioPlayed = false;
						spriteFaded = false;
						textUpdated = false;
						this.GetComponent<TypeWriterEffect>().updateText = false;
						StopCoroutine(this.GetComponent<TypeWriterEffect>().ShowText());
						targetTime = 0.6f; //reset delay between scrolling through dialogue
					} else if (index < dialogue.Length - 1) {
						SKIPwasHit = false;
						dialoguePanel.GetComponent<PanelFader>().Fade();
						StartCoroutine("Wait"); //wait to set panel inactive after 0.5f seconds
						Invoke("SetToFalse", 0.7f); //wait 0.7f seconds to set various variables (so that they dont get stuck not moving)
						audioPlayed = false;
						HUD.SetActive(true);
						HUD.GetComponent<PanelFader>().FadeIfInitiallyShowing();
						ShowCharacter();
						index += 2;

						if (eventStarted)
						{
							character.GetComponent<Events>().increaseEventLevel();

							//TOGGLE AS NEEDED!
							character.GetComponent<Events>().availableForEvent = false;
						}
					} 
					// means we hit the end of the dialogue 
					else {
						hitLast = true;
						SKIPwasHit = false;
						dialoguePanel.GetComponent<PanelFader>().Fade();
						StartCoroutine("Wait"); // wait to set panel inactive after 0.5f seconds
						Invoke("SetToFalse2", 0.7f); // wait 0.7f seconds to set various variables (so that they dont get stuck not moving)
						audioPlayed = false;
						HUD.SetActive(true);
						HUD.GetComponent<PanelFader>().FadeIfInitiallyShowing();
						ShowCharacter();
						// index = 0; // uncomment to make dialogue repeat (but its bugged a bit)

						if (eventStarted)
						{
							character.GetComponent<Events>().increaseEventLevel();

							//TOGGLE AS NEEDED!
							character.GetComponent<Events>().availableForEvent = false;
						}
					}
                }
            }
        } else {
			print("nothing at all");
		}
    }

    /*
    waits to set dialogueEnable to false so that the player does not talk to the character while the panel is fading out
    this helps to avoid the player getting stuck/not able to move as they may click to talk to the character while the panel is fading
    and then they would be stuck on a blank screen
    waits to unfreeze player too so that they don't automatically get re-frozen by update()
    set index to 0 after too so that the dialogue does not begin to repeat
    */
    void SetToFalse()
    {
        dialogueEnabled = false;
        UnfreezePlayer();
        
        textUpdated = false;
        this.GetComponent<TypeWriterEffect>().updateText = false;
        StopCoroutine(this.GetComponent<TypeWriterEffect>().ShowText());

		if (spriteOneActive) {
			print("if asdasdsa");
			// set sprite one to be not active, reset alpha to 0
			spriteOneActive = false;
			Color curColor = sprite.color;
			curColor.a = 0f;
			sprite.color = curColor;

			//set sprite 2 alpha to 1
			curColor = sprite2.color;
			curColor.a = 1f;
			sprite2.color = curColor;
			sprite2.sprite = dialogueSprites[index]; // set to the next sprite
		} else {
			print("else afdasdasd");
			//set sprite one to be active, reset alpha to one
			spriteOneActive = true;
			Color curColor = sprite.color;
			curColor.a = 1f;
			sprite.color = curColor;
			sprite.sprite = dialogueSprites[index]; // set to the next sprite

			//set sprite 2 alpha to 0
			curColor = sprite2.color;
			curColor.a = 0f;
			sprite2.color = curColor;

		}
    }

	void SetToFalse2()
    {
		print("set to false 2");
        dialogueEnabled = false;
        UnfreezePlayer();
        
        textUpdated = false;
        this.GetComponent<TypeWriterEffect>().updateText = false;
        StopCoroutine(this.GetComponent<TypeWriterEffect>().ShowText());

		// reset settings for when it is index 0
		spriteOneActive = true;
		Color curColor  = sprite.color;
		curColor.a = 1f;
		sprite.color = curColor;

		curColor = sprite2.color;
		curColor.a = 0f;
		sprite2.color = curColor;
    }

    //freezes player
    public void FreezePlayer()
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponentInChildren<MouseLook>().enabled = false;
        frozen = true;
    }

    //unfreezes player
    public void UnfreezePlayer()
    {
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponentInChildren<MouseLook>().enabled = true;
        frozen = false;
    }

    public void HideCharacter()
    {
        character.SetActive(false);
    }

    public void ShowCharacter()
    {
        character.gameObject.SetActive(true);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        dialoguePanel.SetActive(false);
    }

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
