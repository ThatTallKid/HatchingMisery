using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credit : MonoBehaviour
{

    public GameObject[] Ui;
    public int UIint;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(End());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator End()
    {
        for (int i = 0; i < 4; i++)
        {
            Ui[UIint].SetActive(true);
            yield return new WaitForSeconds(5);
            Ui[UIint].SetActive(false);
            UIint++;
        }

        SceneManager.LoadScene(0);

    }
}
