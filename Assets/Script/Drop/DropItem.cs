using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour {

	// Use this for initialization
	public string Type;

    public string id;
	public string level;
	public string describe;
	public new string name;

	//public string artifactID;
	//public string artifactLevel;
    public DropItem(ArtifactData ad)
    {
        this.id = ad.id;
        this.name = ad.name;
        this.describe = ad.describe;
        this.level = ad.level;
    }

}
