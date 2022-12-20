using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private DualogueNode[] _node;
    [SerializeField] private int _currentNode;
    [SerializeField] private bool _showDialogue = true;
    [SerializeField] GUIStyle style = new GUIStyle();

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private void OnGUI()
    {
        if(_showDialogue)
        {
            
            
            GUI.Box(new Rect(Screen.width / 2 - 300, Screen.height - 300, 600, 250), "");
            GUI.Label(new Rect(Screen.width / 2 - 250,  Screen.height - 250, 500, 60), _node[_currentNode].NpcText);
            for (int i = 0; i < _node[_currentNode].PlayerAnswer.Length; i++)
            {
                if(GUI.Button(new Rect(Screen.width / 2 - 250, Screen.height - 200 + 25 * i, 500, 25), _node[_currentNode].PlayerAnswer[i].Text, style))
                {
                    if(_node[_currentNode].PlayerAnswer[i].SpeekEnd)
                    {
                        _showDialogue = false;
                    }
                    _currentNode = _node[_currentNode].PlayerAnswer[i].ToNode;
                }
            }
        }        
    }
}

[System.Serializable]
public class DualogueNode
{
    public string NpcText;
    public Answer[] PlayerAnswer;
}

[System.Serializable]
public class Answer
{
    public string Text;
    public int ToNode;
    public bool SpeekEnd;
}

