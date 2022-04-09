using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayRounds : MonoBehaviour
{
    public FloatSO ScoreSO;
    public string DisplayText;
    private TMPro.TextMeshProUGUI tmesh;
    // Start is called before the first frame update
    void Start()
    {
      tmesh=GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
      tmesh.text=DisplayText+" "+ScoreSO.Value;
    }
}
