using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Paint : MonoBehaviour
{
    public MeshRenderer meshRenderer;// Boyayacağımız obje
    public Texture2D brush;// Fırça texture'ı
    public Vector2Int textureArea;//x:1024,y:1024
    Texture2D texture;
    public TextMeshProUGUI percentText;

    private int percent;
    private int paintedPixelCount;

    public Camera cam;

    private void Start()
    {
        
        percent = 0;
        paintedPixelCount = 0;

        percentText.text = "%0";
        percentText.enabled = false;

        texture = new Texture2D(textureArea.x,textureArea.y,TextureFormat.ARGB32,false);
        meshRenderer.material.mainTexture = texture;
    }
    // Update is called once per frame
    void Update()
    {
        if (Run.isFinished == true && percentText.enabled == false)
        {
            percentText.enabled = true;
        }
        if (Input.GetMouseButton(0) && Run.isFinished == true)// Sol tıka basılı tuttukça boyayacak
        {
            RaycastHit hitInfo;

            // cam,kullandığımız kamera(Camera classi)
            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hitInfo))
            {
                //Debug.Log(hitInfo.textureCoord);// 0-1 arasında UV koordinati
                Painting(hitInfo.textureCoord);
                skorCalculater();
            }
        }
    }

    private void skorCalculater()
    {
        //toplam pixel       boyalı ön yüz        boyalı tüm alan    
        //  1048576      -      390000          =     620000    >>>   %1 = 6200
        //    %0                 %100
        Color32[] textureC32 = texture.GetPixels32();
        Color32[] brushC32 = brush.GetPixels32();

        for(int i = 0; i<1024*1024; i++)// texture'un boyutu kadar dön
        {
            if(brushC32[brush.width/2].r > textureC32[i].r)// fırçanın kırmızı rengi texture pixelinin kırmızı renginden 
            {                                              // büyükse boyalıPikselSayısı değişkenini 1 arttır
                paintedPixelCount++;
            }
        }
        for(int i = 1; i<=100; i++)
        {
            if(1048577-paintedPixelCount >= 6200*i)//yüzde = (toplam pixel - boyalı pixel) büyükse %1 değerinden(6200)  ve bunu her yüzde için ayırı ayrı yapar
            {
                percent = i;
            }
        }
        percentText.text = "%"+percent.ToString();
        //Debug.Log(percent);
        paintedPixelCount = 0;
    }
    
    private void Painting(Vector2 coordinate)
    {
        coordinate.x *= texture.width;// 0-1 değerini tam nokta piksellere çevirdik
        coordinate.y *= texture.height;// Yani 0-1024 yaptık

        Color32[] textureC32 = texture.GetPixels32();
        Color32[] brushC32 = brush.GetPixels32();

        // Fırçanın ortasının koordinatları
        Vector2Int halfBrush = new Vector2Int(brush.width / 2,brush.height / 2);

        for(int x = 0; x < brush.width; x++)
        {
            int xPos = x - halfBrush.x + (int)coordinate.x;
            if(xPos < 0 || xPos >= texture.width)
                continue;

            for(int y = 0; y < brush.height; y++)
            {
                int yPos = y - halfBrush.y + (int)coordinate.y;
                if(yPos < 0 || yPos >= texture.height)
                    continue;

                if(brushC32[x+(y*brush.width)].a>0f)
                {
                    
                    int tPos=
                        xPos+//X(U)posizyonu
                        (texture.width*yPos);//Y(V)Posizyonu

                    if(brushC32[x+(y*brush.width)].r>textureC32[tPos].r)
                        textureC32[tPos]=brushC32[x+(y*brush.width)];
                }
                
            }
        }
        texture.SetPixels32(textureC32);
        texture.Apply();// Değişikliklerin uygulanması için
    }        
}