using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class materialScript : MonoBehaviour
{
    public SpriteAtlas spriteAtlas; // Ссылка на спрайт-атлас
    public string spriteName = "ааа_0"; // Имя спрайта в атласе

    void Start()
    {
        // Получить ссылку на спрайт из атласа
        Sprite sprite = spriteAtlas.GetSprite(spriteName);

        // Создать новый материал
        Material material = new Material(Shader.Find("Standard"));

        // Установить текстуру спрайта в материал
        material.mainTexture = sprite.texture;

        // Применить материал к вашему объекту
        GetComponent<Renderer>().material = material;
    }
}
