using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogManager : MonoBehaviour
{
    public TextMeshProUGUI DialogText;
    public Text NameText;

    public Animator animator;
    public Animator animatorStart;

    private Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>(); 
    }

    public void StartDialog(Dialog dialog)
    {
        animator.SetBool("IsOpen", true);
        animatorStart.SetBool("IsClose", true);

        NameText.text = dialog.name;

        sentences.Clear();

        foreach (string sentence in dialog.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }    

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            SceneManager.LoadScene("Lv1");
            EndDialog();
            return; 
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }
    IEnumerator TypeSentence(string sentence)
    {
        DialogText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            DialogText.text += letter;
            yield return null;
        }
    }
    void EndDialog()
    {
        animator.SetBool("IsOpen", false);
    }    

    
}
