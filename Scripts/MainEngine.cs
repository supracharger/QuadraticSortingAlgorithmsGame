using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainEngine : MonoBehaviour
{
    public GameObject _bubbleSortBtn;
    public GameObject _insertionSortBtn;
    public GameObject _selectionSortBtn;

    void Awake(){
        LoadTheScene(_bubbleSortBtn, "BubbleSort");
        LoadTheScene(_insertionSortBtn, "InsertionSort");
        LoadTheScene(_selectionSortBtn, "SelectionSort");
    }

    void LoadTheScene(GameObject btnObj, string scene)
    {
        btnObj.GetComponent<Button>().onClick.AddListener(
            () => SceneManager.LoadScene(scene)
        );
    }
}
