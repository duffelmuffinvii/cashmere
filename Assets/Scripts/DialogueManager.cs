using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    /*Flags & Flag Checker
     
        Flags help determine important stuff like who's the current speaker or what are the previous important choices
        that a player has made.

        Flag Checker is a method that needs to run before any other method runs. Based on the currently active/inactive flags,
        the dialogue will switch to a different game object.

        isCurrentlyActive: displays the current active flag. Set by Flag Checker
        etc. flags: All the other flags for options or dialogue order. Each flag has a unique number (tag id) assoc. with them

                             
    */

    [Header("Character Dialogue Flags")]

    private bool char1;          //Tag id 1
    private bool char2;          //Tag id 2       

    [Header("isCurrentlyActive")]

    private int isCurrentlyActive;      //Takes Tag ids to indicate the current active speaker

    [Header("nextLine")]

    public GameObject reader_object;
    private Reader reader;

    public string line;

    [SerializeField] private float typingSpeed = 0.02f;

    [SerializeField] private bool player1Speaking;

    [Header("Dialogue TMP")]
    [SerializeField] private TextMeshProUGUI player1dialoguetext;
    [SerializeField] private TextMeshProUGUI player2dialoguetext;

    [Header("Option bar")]
    [SerializeField] private GameObject Option1;
    [SerializeField] private GameObject Option2;
    [SerializeField] private GameObject Option3;
    [SerializeField] private GameObject Option4;

    private string player1DialogueSentences = " ";
    //[SerializeField] private string[] player1DialogueSentences;
    [Header("Dialogue Sentences")]
    [TextArea]
    [SerializeField] private string[] player2DialogueSentences;

    
  

    private bool dialogueStart = false;
    private char flagCheck;

    private bool option1flag = false;
    private bool option2flag = false;
    private bool option3flag = false;
    private bool option4flag = false;

    private int option1branch = 0;
    private int option2branch = 1;
    private int option3branch = 2;
    private int option4branch = 3;



    private void Start()
    {
        reader = reader_object.GetComponent<Reader>();

        FlagChecker();
        StartDialogue();
    }

    private IEnumerator TypePlayer1Dialogue()
    {

        foreach (char letter in player1DialogueSentences.ToString().ToCharArray())
        {
            Debug.Log(player1DialogueSentences);


            player1dialoguetext.text += letter;
           
            yield return new WaitForSeconds(typingSpeed);
            
        }

        flagCheck = System.Convert.ToChar(player1DialogueSentences.Substring(0, player1DialogueSentences.Length - 1));
        if(flagCheck == '&')
        {
            TypePlayer1Dialogue();

        }



    }

    private IEnumerator TypePlayer2Dialogue()
    {
        
        
        if (option1flag == true && option2flag == false && option3flag == false && option4flag == false)
        {

            foreach (char letter in player2DialogueSentences[option1branch].ToCharArray())
            {

                player2dialoguetext.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }

        }

        if (option1flag == false && option2flag == true && option3flag == false && option4flag == false)
        {

            foreach (char letter in player2DialogueSentences[option2branch].ToCharArray())
            {

                player2dialoguetext.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }

        }

        if (option1flag == false && option2flag == false && option3flag == true && option4flag == false)
        {

            foreach (char letter in player2DialogueSentences[option3branch].ToCharArray())
            {

                player2dialoguetext.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }

        }

        if (option1flag == false && option2flag == false && option3flag == false && option4flag == true)
        {

            foreach (char letter in player2DialogueSentences[option4branch].ToCharArray())
            {

                player2dialoguetext.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }

        }
        
    }

    public void StartDialogue()
    {
        if (player1Speaking)
        {
            StartCoroutine(TypePlayer1Dialogue());

        }

        else 
        {
            StartCoroutine(TypePlayer2Dialogue());

        }

    }

   

    public void player2ContinueButton()
    {

       

    }

    public void Option1ButtonPress()
    {

        option1flag = true;
        option2flag = false;
        option3flag = false;
        option4flag = false;

        player2dialoguetext.text = string.Empty;

        StartCoroutine(TypePlayer2Dialogue());

    }

    public void Option2ButtonPress()
    {

        option1flag = false;
        option2flag = true;
        option3flag = false;
        option4flag = false;

        player2dialoguetext.text = string.Empty;

        StartCoroutine(TypePlayer2Dialogue());

    }

    public void Option3ButtonPress()
    {

        option1flag = false;
        option2flag = false;
        option3flag = true;
        option4flag = false;

        player2dialoguetext.text = string.Empty;

        StartCoroutine(TypePlayer2Dialogue());

    }


    public void Option4ButtonPress()
    {

        option1flag = false;
        option2flag = false;
        option3flag = false;
        option4flag = true;

        player2dialoguetext.text = string.Empty;

        StartCoroutine(TypePlayer2Dialogue());

    }


    public void FlagChecker()
    {

        line = reader.lineReader;
        if (line != null)
        {
            player1DialogueSentences = line;

        }
        //player1DialogueSentences.Add();

    }

    private void Update()
    {
        FlagChecker();

    }

}
