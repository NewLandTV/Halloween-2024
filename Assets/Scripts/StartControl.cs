using System.Collections;
using UnityEngine;

public class StartControl : MonoBehaviour
{
    [SerializeField]
    private GameObject director;

    private IEnumerator Start()
    {
        while (!Input.GetKeyDown(KeyCode.Space))
        {
            yield return null;
        }

        director.SetActive(true);
    }
}
