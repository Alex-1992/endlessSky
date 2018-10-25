using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTest : MonoBehaviour {

    public Image imageFilled;//填充背景，及灰色读条    
    private float coldTime = 2.0f;//技能的冷却时间    
    private float timer = 0;//当前冷却时间    
    private bool isCold = false;//是否进入冷却    
    private KeyCode skillKey = KeyCode.Alpha1;//技能键
    
    void Start () {        
    	//imageFilled=gameObject.GetComponent<Image>();        
    	imageFilled.fillAmount = 0;    
    }       
         
    void Update () {        
    	isCold=SkillKeyDown();        
    	if(isCold == true)        
    	{            
    		timer += Time.deltaTime;            
    		if(timer > coldTime)            
    		{ 
    			//冷却完毕，回归默认值               
    			isCold = false;                
    			timer = 0;                
    			imageFilled.fillAmount = 0;            
    		}            
    		else            
    		{                
    			imageFilled.fillAmount = (coldTime - timer)/coldTime;   //冷却比例         
    		}        
    	}            
    }
    
    //当鼠标点击时释放技能    
    public void OnClick()    
    {        
    	// isCold = true;        
    	// FreeSkill();    
    }
    
    //当按到1时释放技能    
    private bool SkillKeyDown()    
    {        
    	imageFilled.fillAmount = 0;        
    	if(Input.GetKey(skillKey))        
    	{            
    		isCold = true;        
    	}        
    	FreeSkill();        
    	return isCold;    
    }   
     
    //技能释放
    private void FreeSkill()    
    {
            //释放技能方法（未实现）    
    }
}

