using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GM_Secret : MonoBehaviour
{
    private float timeToGlitch;
    private float timeToGlitchSkybox;
    private float timeToQuit;
    private bool glitching;
    private AudioSource audioSource;
    public Material dark;
    public Material hallofchaos;
    public Material night;
    public Material unused;
    public Material crash;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        timeToGlitch = audioSource.clip.length + 2f;
    }

    void Update()
    {
        if (timeToGlitch > 0f & glitching == false)
        {
            timeToGlitch -= Time.deltaTime;
        }

        if (timeToGlitch < 0f & glitching == false)
        {
            glitching = true;
            StartCoroutine("SkyboxChange");
            timeToQuit = 25f;
        }

        if (glitching == true & audioSource.pitch < 3f)
        {
            audioSource.pitch += Time.deltaTime * 0.1f;
        }

        if (audioSource.pitch > 3f)
        {
            audioSource.pitch = 3f;
        }

        if (timeToGlitchSkybox > 0f)
        {
            timeToGlitchSkybox -= Time.deltaTime;
        }

        if (timeToGlitchSkybox < 0f)
        {
            StartCoroutine("SkyboxChange");
        }

        if (timeToQuit > 0f)
        {
            timeToQuit -= Time.deltaTime;
        }

        if (timeToQuit < 0f)
        {
            {
                SceneManager.LoadScene("Credits");
            }
        }
    }

    IEnumerator SkyboxChange()
    {
        timeToGlitchSkybox = 5f;
        yield return new WaitForSeconds(1);
        RenderSettings.skybox = dark;
        RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Skybox;
        yield return new WaitForSeconds(1);
        RenderSettings.skybox = hallofchaos;
        yield return new WaitForSeconds(1);
        RenderSettings.skybox = night;
        yield return new WaitForSeconds(1);
        RenderSettings.skybox = unused;
        yield return new WaitForSeconds(1);
        RenderSettings.skybox = crash;
    }
}