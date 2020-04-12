using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] float scrollSpeed = 0.5f;
    Material bgMaterial;
    void Start()
    {
        bgMaterial = GetComponent<Renderer>().material;
    }

    void Update()
    {
        bgMaterial.mainTextureOffset += new Vector2(0f, scrollSpeed) * Time.deltaTime;
    }
}
