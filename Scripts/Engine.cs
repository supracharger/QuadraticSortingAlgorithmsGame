using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Engine : MonoBehaviour
{
    public static GameObject _tile;
    public GameObject _tileObj;
    public GameObject _btnBack;
    public GameObject _btnRandomize;
    public GameObject _input;
    // Start is called before the first frame update
    void Awake()
    {
        _tile = _tileObj;
        _tile.SetActive(false);
        _btnBack.GetComponent<Button>().onClick.AddListener(
            () => SceneManager.LoadScene("main2"));
        var inputTxt = _input.GetComponentInChildren<TMP_InputField>();
        _btnRandomize.GetComponent<Button>().onClick.AddListener(
            () => inputTxt.text = Randomize()
        );
    }

    static string Randomize(int size = 7){
        var rand = new System.Random();
        var nums = Enumerable.Range(0, size).Select(i => rand.Next(1, 99)).ToList();
        return string.Join(", ", nums);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
