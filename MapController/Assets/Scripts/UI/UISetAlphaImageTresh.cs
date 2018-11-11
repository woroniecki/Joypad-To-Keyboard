using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UISetAlphaImageTresh : MonoBehaviour {

	void Awake () {
        GetComponent<Image>().alphaHitTestMinimumThreshold = 1f;
	}

}
