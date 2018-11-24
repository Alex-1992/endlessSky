using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///
public class SkillData
{
    public string id, name, describe, img, damage_factor, level, cd, mp_cost, script, bond_btn, keycode;
    //JsonMapper.ToObject 方法要求类不能有构造函数
}

public class ArtifactData
{
    public string id,name, describe, level;
    public ArtifactData(string v0,string v1, string v2, string v3)
    {
        this.id = v0;
        this.name = v1;
        this.describe = v2;
        this.level = v3;
    }
    //JsonMapper.ToObject 方法要求类不能有构造函数
}
