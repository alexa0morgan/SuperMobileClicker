using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopLoadHandler : MonoBehaviour
{
    private List<Flower> flowers = new List<Flower>()
    {
        new Flower(){ name = "Test 1", cost = 1000 },
        new Flower(){ name = "Test 2", cost = 2000 },
    };


    class Flower
    {
        public string name { get; set; }
        public int cost { get; set; }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject viewPort = GameObject.FindGameObjectWithTag("Viewport");
    }
}
