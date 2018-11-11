using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButton : MonoBehaviour
{
    private bool isClicked = false;

    public void Click()
    {
        isClicked = true;
    }

    public IEnumerator WaitForClose()
    {
        while (!isClicked)
        {
            yield return new WaitForSeconds(0.1f);
        }
        isClicked = false;
        yield return null;
    }
}
