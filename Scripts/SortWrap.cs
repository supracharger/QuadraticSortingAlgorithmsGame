using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SortWrap : MonoBehaviour
{
    public GameObject _input;
    ASort sort;
    public GameObject _sortBtn;
    public GameObject _erMsg;
    public GameObject[] _texts;
    public SortType _sortType;
    public enum SortType
    {BubbleSort, InsertionSort, SelectionSort};

    // Start is called before the first frame update
    void Start()
    {
        _sortBtn.GetComponent<Button>().onClick.AddListener(
            () => {
                // Dispose Previous
                if (sort != null) sort.Dispose();
                // Get the sort desired
                if (_sortType == SortType.BubbleSort)
                    sort = new BubbleSort(_texts);
                else if (_sortType == SortType.InsertionSort)
                    sort = new InsertionSort(_texts);
                else if (_sortType == SortType.SelectionSort)
                    sort = new SelectionSort(_texts);
                // Plan out the sort to display
                if (sort.Init(_input, _erMsg))
                    sort.Plan();
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (sort == null) return;
        var nextAct = sort.NextExecute();
        if (nextAct != null)
            StartCoroutine(nextAct());
    }
}
