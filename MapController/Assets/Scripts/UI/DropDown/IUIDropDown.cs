using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUIDropDown {

    string[] GetOptions();

    void SetValue(int value);

}
