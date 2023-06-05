using System.Collections;
using UnityEngine;
using UnityEngine.UI;
// Type Writer Script, Author : witnn .

[RequireComponent(typeof(AudioSource))]
public class TypeWrite : MonoBehaviour
{
    public float delay = 0.1f;
    public AudioSource TypeSound;
    [Multiline]
    public string yazi;

    AudioSource audSrc;
    Text thisText;

    private void Start()
    {
        audSrc = GetComponent<AudioSource>();
        thisText = GetComponent<Text>();

        StartCoroutine(TypeWriter());
    }

    IEnumerator TypeWriter()
    {
        foreach (char i in yazi)
        {
            thisText.text += i.ToString();
            audSrc.Play();
            if (i.ToString() == ".")
            {
                yield return new WaitForSeconds(1);
            }
            else 
            {
                yield return new WaitForSeconds(delay);
            }
        }
    }
}
