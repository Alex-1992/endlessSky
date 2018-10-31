using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class Tools {
	//public enum SkillScript { Skill_jianzaihuopao, Skill_shanxiandaji, Skill_nengliangchang }

	// public static Skill_jianzaihuopao jianzaihuopao;
	// public static Skill_shanxiandaji shanxiandaji;
	// public static Skill_nengliangchang nengliangchang;

	public static bool SetSkillLevelByName (string name, int level) {
		Debug.Log ("GetScriptByName" + name);
		switch (name) {
			case "Skill_jianzaihuopao":
				//Skill_jianzaihuopao.SetSkillLevel (3);
				new Skill_jianzaihuopao().SetSkillLevel (level);
				Debug.Log (Skill_jianzaihuopao.SkillLevel);
				//return null;
				return true;
			case "Skill_shanxiandaji":
				new Skill_shanxiandaji().SetSkillLevel (level);
				Debug.Log (Skill_shanxiandaji.SkillLevel);
				return true;
			case "Skill_nengliangchang":
				new Skill_nengliangchang().SetSkillLevel (level);
				Debug.Log (Skill_nengliangchang.SkillLevel);
				return true;

		}
		return false;
	}

	// private static readonly string path = "GCForum";
	// public static SkillBase GetScriptByName (string name) {
	// 	return (SkillBase) Assembly.Load (path).CreateInstance (path + "." + name);
	// }

}