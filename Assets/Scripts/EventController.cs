using System.Collections;
using UnityEngine;

public class EventController : MonoBehaviour
{
    public void TextCaller(string text) => print(text);

    public void RunTim()
    {
        StartCoroutine(Tim());
    }
    private IEnumerator Tim()
    {
        yield return new WaitForSeconds(5);
        print("Im still here");
    }
}