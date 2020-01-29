using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShadowBaker : MonoBehaviour
{    [Header("Textures")]

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


       /*Texture tex_temp = new Texture2D(a.width,a.height);
        Graphics.CopyTexture(a,tex_temp);
        backgroundTexture = (Texture2D) tex_temp;
*/
        int ppu = pixelsPerUnit;

        MiniImage[] mis = FindObjectsOfType<MiniImage>();
        Transform[] masks = new Transform[mis.Length];
        int widest=0; float ww=0.0f; 
        int lowest=0; float ll=9999f;
        int highest=0; float hh=-9999f;
        for(int i = 0; i < mis.Length; i++)
        {
            masks[i] = mis[i].transform;
            if(ww < mis[i].transform.localScale.x) {ww=mis[i].transform.localScale.x; widest = i;}
            if(ll> mis[i].transform.position.y) {ll=mis[i].transform.position.y; lowest = i;}
            if(hh < mis[i].transform.position.y) {hh=mis[i].transform.position.y; highest = i;}

        }


        int w=(int) (ww*(float)ppu*2.0f);
        int h = (int) ((((hh+masks[highest].localScale.y)-ll+masks[lowest].localScale.y))*(float)ppu);

        w+=padding*2;
        h+=padding*2;


        Texture2D tex = new Texture2D(w,h);

        for(int i = 0; i < w; i++)
        {
            for(int j = 0; j < h; j++)
            {

                tex.SetPixel(i,j,new Color32(0,0,0,0));

            }

        }


        float x,y;

        x =  (masks[lowest].position.x - masks[lowest].lossyScale.x) ;
        y =  (masks[lowest].position.y - masks[lowest].lossyScale.y) ;


        int i_max = (int) ((float)shadowTexture.width/(float)shadowScaler);
        int j_max = (int) ((float)shadowTexture.height/(float)shadowScaler);


        for(int m = 0; m < masks.Length; m++)
        {

        

            

            int k_max;
            k_max= (int)((masks[m].lossyScale.x*(float)ppu*2f)/(float)i_max);
            int k_max2 = (int) ((float) k_max * tiling);
            
            for(int k = 0; k < ((float)k_max/tiling)+1; k++)
            {

            for(int i = 0; i < i_max; i++)
            {

                for(int j = 0; j < j_max; j++)
                {
                    //BOTTOM
                    float x1,y1;
                    x1 = masks[m].position.x-masks[m].lossyScale.x;
                    y1 = masks[m].position.y-masks[m].lossyScale.y;
                    int x2 = (int)((x1 - x)*ppu)+0+padding;
                    int y2= (int)((y1 - y)*ppu)+j+padding;
                    int x3 = (int)((x1-x+masks[m].lossyScale.x*2f)*(float)ppu)+padding;
                    x3-=(int) ((float)k*(float)i_max*tiling);
                    x3-=i;
                   // x2 = x3-x2;
                    //if(x2 < (int)((x1 - x)*ppu)+0+padding) continue;

                    //if(false) {}
                    if(x3<x2) continue;
                    else{
                    Color32 a = tex.GetPixel(x3,y2);
                    Color32 b = shadowTexture.GetPixel((int)((float)i*(float)shadowScaler),(int)((float)j*(float)shadowScaler));
                    Color32 final =shadowColor;
                    double aa = (double)a.a/(double)255.0;
                    double ba = (double)b.a/(double)255.0;
                    double f =  (aa + ba*(1-aa));
                    final.a = (byte) (f*255);
                    
                    tex.SetPixel(x3,y2,final);

                    
                    
                    //TOP
                    
                    y1 = masks[m].position.y+masks[m].lossyScale.y;
                    y2= (int)((y1 - y)*ppu)-j+padding;
                    a = tex.GetPixel(x3,y2);
                    b = shadowTexture.GetPixel((int)((float)i*(float)shadowScaler),(int)((float)j*(float)shadowScaler));
                    final = shadowColor;
                    aa = (double)a.a/(double)255.0;
                    ba = (double)b.a/(double)255.0;
                    f =  (aa + ba*(1-aa));
                    final.a = (byte) (f*255);
                    tex.SetPixel(x3,y2,final);


                    }
                }

            }
            }

            
            int kk_max;
            kk_max= (int)((masks[m].lossyScale.y*(float)ppu*2f)/(float)i_max);
            int kk_max2 = (int) ((float) kk_max * tiling);
            


            for(int k = 0; k < ((float)kk_max/tiling); k++)
            {

            for(int i = 0; i < i_max; i++)
            {

                for(int j = 0; j < j_max; j++)
                {
                    float x1,y1;
                    x1 = masks[m].position.x-masks[m].lossyScale.x;
                    y1 = masks[m].position.y-masks[m].lossyScale.y;
                    int x2 = (int)((x1 - x)*ppu)+j+padding;
                    int y2= (int)((y1 - y)*ppu)+i+padding;;
                    y2+=(int) ((float)k*(float)i_max*tiling);
                    int y3 = (int)((y1-y+masks[m].lossyScale.y*2f)*(float)ppu)+padding;
                    if(y2>y3) continue;
                    else{
                  
                    Color32 a = tex.GetPixel(x2,y2);
                    Color32 b = shadowTexture.GetPixel((int)((float)i*(float)shadowScaler),(int)((float)j*(float)shadowScaler));
                    Color32 final = shadowColor;
                    double aa = (double)a.a/(double)255.0;
                    double ba = (double)b.a/(double)255.0;
                    double f =  (aa + ba*(1-aa));
                    final.a = (byte) (f*255);

                   
                    tex.SetPixel(x2,y2,final);    


                    //RIGHT
                    x1 = masks[m].position.x+masks[m].lossyScale.x;
                    y1 = masks[m].position.y-masks[m].lossyScale.y;
                    x2 = (int)((x1 - x)*ppu)-i+padding;
                    y2= (int)((y1 - y)*ppu)+j+padding;
                    
                    
                    y2+=(int) ((float)k*(float)i_max*tiling);

                    a = tex.GetPixel(x2,y2);
                    b = shadowTexture.GetPixel((int)((float)i*(float)shadowScaler),(int)((float)j*(float)shadowScaler));
                    final = shadowColor;
                    aa = (double)a.a/(double)255.0;
                    ba = (double)b.a/(double)255.0;
                    f =  (aa + ba*(1-aa));
                    final.a = (byte) (f*255);
                    tex.SetPixel(x2,y2,final);
                        
                    }


                }
            }
            }

/*
            
            i_max = (int) ((float)cornerShadow.width/(float)shadowScaler);
            j_max = (int) ((float)cornerShadow.height/(float)shadowScaler);



              for(int i = 0; i < i_max; i++)
            {

                for(int j = 0; j < j_max; j++)
                {
                    //CORNERf
                    float x1,y1;
                    x1 = masks[m].position.x-masks[m].lossyScale.x;
                    y1 = masks[m].position.y-masks[m].lossyScale.y;
                    int x2 = (int)((x1 - x)*ppu)+i+padding;
                    int y2= (int)((y1 - y)*ppu)+j+padding;
                    x2+=shadowOffset.x;
                    y2+=shadowOffset.y;
                    Color32 a = tex.GetPixel(x2,y2);
                    Color32 b = cornerShadow.GetPixel((int)((float)i*(float)shadowScaler),(int)((float)j*(float)shadowScaler));
                    Color32 final = new Color32();
                    double aa = (double)a.a/(double)255.0;
                    double ba = (double)b.a/(double)255.0;
                    double f =  (aa + ba*(1-aa));
                    final.a = (byte) (f*255);
               //     tex.SetPixel(x2,y2,final);
                }

            }
*/



        }



        tex.Apply();
        Sprite s = Sprite.Create(tex,new Rect(0,0,w,h),new Vector2(0,0),ppu);
        backgroundPanel.GetComponent<SpriteRenderer>().sprite = s;
        Vector3 tpos = backgroundPanel.transform.position;
        tpos.x = x -(float)padding/(float)ppu;;
        tpos.y = y - (float)padding/(float)ppu;
        backgroundPanel.transform.position = tpos + new Vector3(shadowOffset.x,shadowOffset.y,0);



    }



}


/*
                    COLOR CORRECTION
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
