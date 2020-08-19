using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Terminal : MonoBehaviour
{

    [SerializeField]
    InputField terminalInput = null;

    [SerializeField]
    GameObject awaitingInputSignal = null;

    [SerializeField]
    GameObject terminalOutputLinePrefab = null;

    [SerializeField]
    RectTransform terminalOutputLineParent = null;

    public Command[] initialCommandDatabase;

    static InputField sTerminalInput;
    static RectTransform sTerminalOutputLineParent;
    static GameObject sTerminalOutputLinePrefab;

    public static Dictionary<string, Command> sCommandDatabase = new Dictionary<string, Command>();

    // Start is called before the first frame update
    void Start()
    {
        sTerminalInput = terminalInput;
        sTerminalOutputLineParent = terminalOutputLineParent;
        sTerminalOutputLinePrefab = terminalOutputLinePrefab;

        if(initialCommandDatabase != null || initialCommandDatabase.Length == 0){
            for(int i = 0; i < initialCommandDatabase.Length; i++){
                sCommandDatabase.Add(initialCommandDatabase[i].GetCommandName(), initialCommandDatabase[i]);
            }
        }else{
            Debug.LogError("ERROR: Terminal has no initial commands!");
        }

        terminalInput.ActivateInputField();
        awaitingInputSignal.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

        if(terminalInput.isFocused == false){
            terminalInput.ActivateInputField();
        }

        //move text in terminal input area, clear terminal input area, if enter is pressed
        if(Input.GetKeyDown(KeyCode.Return)){
            awaitingInputSignal.SetActive(false);
            PrintToTerminal("> " + terminalInput.text);
            
            if(terminalInput.text == "")
                return;

            //seperate out text segments of input
            List<string> parsedInput = ParseInput(terminalInput.text);
            List<string> parsedArgs = new List<string>();
            for(int i = 1; i < parsedInput.Count; i++){
                parsedArgs.Add(parsedInput[i]);
            }

            //search for, and run if found, the appropiate command
            if(sCommandDatabase.ContainsKey(parsedInput[0])){
                sCommandDatabase[parsedInput[0]].Excecute(parsedArgs.ToArray());
            }else{
                //print if command was not found
                PrintToTerminal("ERROR: Command " + parsedInput[0] + " not found");
            }
            
            
            
            terminalInput.text = string.Empty;          
            awaitingInputSignal.SetActive(true);
        }

    }

    List<string> ParseInput(string input){
        List<string> result = new List<string>();

        for(int i = 0; i < input.Length; i++){
            if(input[i] != ' '){
                //parse out the word starting from lastWordEndPoint
                string parsedWord = ParseWord(input, i);
                i += parsedWord.Length;
                result.Add(parsedWord);
            }
        }

        return result;
    }

    string ParseWord(string input, int startIndex){
        string result = "";

        for(int i = startIndex; i < input.Length; i++){
            if(input[i] == ' ')
                break;

            result += input[i];
        }

        return result;
    }
    
    public static void PrintToTerminal(string message){
        //instantate the terminal output line
        GameObject obj = Instantiate(sTerminalOutputLinePrefab, new Vector3(0,0,0), Quaternion.identity, sTerminalOutputLineParent.transform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(sTerminalOutputLineParent);
        obj.GetComponentInChildren<TextMeshProUGUI>().text = message + "\n";
    }

}


