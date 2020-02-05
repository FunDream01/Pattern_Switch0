using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShadowBaker : MonoBehaviour
{
    [Header("Textures")]

    [Header("Shadow Baker!")]

    public Texture2D shadowTexture;

    public GameObject backgroundPanel;

    public Color32 shadowColor;

    [HideInInspector]
    public float tiling;

    [Header("Position and Resolution")]

    public int pixelsPerUnit;
    public int padding;
    public float shadowScaler;
    public Vector2 shadowOffset;



    public void bake()
    {
        Transform[] masks = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            masks[i] = transform.GetChild(i);
        }


        Bounds boundary = GetMaxBounds();
        int w = (int)(boundary.size.x * (float)pixelsPerUnit);
        int h = (int)(boundary.size.y * (float)pixelsPerUnit);
        w += padding * 2;
        h += padding * 2;

        float x, y;
        x = boundary.min.x;
        y = boundary.min.y;


        Texture2D tex = emptyTexture(w,h);


        int i_max = (int)((float)shadowTexture.width / (float)shadowScaler);
        int j_max = (int)((float)shadowTexture.height / (float)shadowScaler);


        for (int m = 0; m < masks.Length; m++)
        {
            int k_max = (int)((masks[m].lossyScale.x * (float)pixelsPerUnit * 2f) / (float)i_max);
            int k_max2 = (int)((float)k_max * tiling);

            for (int k = 0; k < ((float)k_max / tiling) + 1; k++)
            {

                for (int i = 0; i < i_max; i++)
                {

                    for (int j = 0; j < j_max; j++)
                    {
                        //BOTTOM
                        float x1, y1;
                        x1 = masks[m].position.x - masks[m].lossyScale.x;
                        y1 = masks[m].position.y - masks[m].lossyScale.y;
                        int x2 = (int)((x1 - x) * pixelsPerUnit) + 0 + padding;
                        int y2 = (int)((y1 - y) * pixelsPerUnit) + j + padding;
                        int x3 = (int)((x1 - x + masks[m].lossyScale.x * 2f) * (float)pixelsPerUnit) + padding;
                        x3 -= (int)((float)k * (float)i_max * tiling);
                        x3 -= i;
                        if (x3 < x2) continue;
                        else
                        {
                            Color32 a = tex.GetPixel(x3, y2);
                            Color32 b = shadowTexture.GetPixel((int)((float)i * (float)shadowScaler), (int)((float)j * (float)shadowScaler));
                            tex.SetPixel(x3, y2, LayerPixels(a, b));

                            //TOP
                            y1 = masks[m].position.y + masks[m].lossyScale.y;
                            y2 = (int)((y1 - y) * pixelsPerUnit) - j + padding;
                            a = tex.GetPixel(x3, y2);
                            b = shadowTexture.GetPixel((int)((float)i * (float)shadowScaler), (int)((float)j * (float)shadowScaler));
                            tex.SetPixel(x3, y2, LayerPixels(a, b));


                        }
                    }

                }
            }


            int kk_max;
            kk_max = (int)((masks[m].lossyScale.y * (float)pixelsPerUnit * 2f) / (float)i_max);
            int kk_max2 = (int)((float)kk_max * tiling);



            for (int k = 0; k < ((float)kk_max / tiling); k++)
            {

                for (int i = 0; i < i_max; i++)
                {

                    for (int j = 0; j < j_max; j++)
                    {
                        //LEFT
                        float x1, y1;
                        x1 = masks[m].position.x - masks[m].lossyScale.x;
                        y1 = masks[m].position.y - masks[m].lossyScale.y;
                        int x2 = (int)((x1 - x) * pixelsPerUnit) + j + padding;
                        int y2 = (int)((y1 - y) * pixelsPerUnit) + i + padding; ;
                        y2 += (int)((float)k * (float)i_max * tiling);
                        int y3 = (int)((y1 - y + masks[m].lossyScale.y * 2f) * (float)pixelsPerUnit) + padding;
                        if (y2 > y3) continue;
                        else
                        {

                            Color32 a = tex.GetPixel(x2, y2);
                            Color32 b = shadowTexture.GetPixel((int)((float)i * (float)shadowScaler), (int)((float)j * (float)shadowScaler));
                            tex.SetPixel(x2, y2, LayerPixels(a, b));


                            //RIGHT
                            x1 = masks[m].position.x + masks[m].lossyScale.x;
                            y1 = masks[m].position.y - masks[m].lossyScale.y;
                            x2 = (int)((x1 - x) * pixelsPerUnit) - i + padding;
                            y2 = (int)((y1 - y) * pixelsPerUnit) + j + padding;


                            y2 += (int)((float)k * (float)i_max * tiling);

                            a = tex.GetPixel(x2, y2);
                            b = shadowTexture.GetPixel((int)((float)i * (float)shadowScaler), (int)((float)j * (float)shadowScaler));
                            tex.SetPixel(x2, y2, LayerPixels(a, b));

                        }


                    }
                }
            }
        }
            tex.Apply();
            Sprite s = Sprite.Create(tex, new Rect(0, 0, w, h), new Vector2(0, 0), pixelsPerUnit);
            backgroundPanel.GetComponent<SpriteRenderer>().sprite = s;
            Vector3 tpos = backgroundPanel.transform.position;
            tpos.x = x - (float)padding / (float)pixelsPerUnit; ;
            tpos.y = y - (float)padding / (float)pixelsPerUnit;
            backgroundPanel.transform.position = tpos + new Vector3(shadowOffset.x, shadowOffset.y, 0);



        
    }

    Bounds GetMaxBounds()
    {
        var b = new Bounds(transform.position, Vector3.zero);
        for (int i = 0; i < transform.childCount; i++)
        {
            b.Encapsulate(transform.GetChild(i).GetComponent<SpriteMask>().bounds);
        }
        return b;
    }

    

    Color32 LayerPixels(Color32 a, Color32 b)
    {
        Color32 final = shadowColor;
        double aa = (double)a.a / (double)255.0;
        double ba = (double)b.a / (double)255.0;
        double f = (aa + ba * (1 - aa));
        final.a = (byte)(f * 255);
        return final;

        /* COLOR CORRECTION
        a.r = (byte) (a.r*a.a);
        a.g = (byte) (a.g*a.a);
        a.b = (byte) (a.b*a.a);

        b.r = (byte) (b.r*b.a);
        b.g = (byte) (b.g*b.a);
        b.b = (byte) (b.b*b.a);

        final.r =(byte) (a.r + b.r*(255-a.a));
        final.g =(byte) (a.g + b.g*(255-a.a));
        final.b =(byte) (a.b + b.b*(255-a.a));
        */


    }

    Texture2D emptyTexture(int w, int h)
    {
        Texture2D t = new Texture2D(w,h);
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {

                t.SetPixel(i, j, new Color32(0, 0, 0, 0));

            }

        }
        return t;

    }


}



