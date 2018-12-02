using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Windows.Forms;

public class VersionController : MonoBehaviour
{

    const string currentAvailableVersionURL = "http://raw.githubusercontent.com/woroniecki/Joypad-To-Keyboard/master/VERSION";

    [SerializeField] Text versionText;
    
    /// <summary>
    /// Data of url:
    /// Current version
    /// Patch note
    /// Link to download new version
    /// </summary>
    IEnumerator Start()
    {
        versionText.text = "v" + UnityEngine.Application.version;

        using (WWW www = new WWW(currentAvailableVersionURL))
        {
            yield return www;

            if (www.error != null && www.error != "")
                yield break;

            string text = www.text;

            string newVersion = text.Substring(0, text.IndexOf('\n'));

            int firstIndex = text.IndexOf('\n');
            int lastIndex = text.Substring(0, text.Length - 1).LastIndexOf('\n');

            string patchNote = text.Substring(firstIndex + System.Environment.NewLine.Length).Substring(0, lastIndex - firstIndex - 1);
            string newVersionDownloadLink = text.Substring(lastIndex, text.Length - lastIndex - 1);

            if (versionText.text.CompareTo(newVersion) != 0)
            {
                DialogResult result = MessageBox.Show(
                "New version available (" + newVersion + ")\n\n"
                + patchNote + "\n"
                + "Do you want to download new version?",
                name,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information
                );

                if (result == DialogResult.Yes)
                    UnityEngine.Application.OpenURL(newVersionDownloadLink);
            }
        }
    }
}
