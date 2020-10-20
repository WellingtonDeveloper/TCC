using System.Collections;
using UnityEngine;

public class SoundCenas : MonoBehaviour {

	public AudioSource efSource; //efeitos sons
	public AudioSource musicSource; // som fundo
	public static SoundCenas instance = null; //
	public float lowPitchRange = .95f; // para o som não ficar monotomo, este é o pitch do som
	public float highPitchRange = 1.05f; // pitch mais alto

	void Awake()
	{
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}

		DontDestroyOnLoad (gameObject);
	}

	public void PlaySingls (AudioClip clip)
	{
		efSource.clip = clip;
		efSource.Play ();
	}

	public void RondomizeSfx (params AudioClip[] clips)
	{
		int randomIndex = Random.Range (0,clips.Length);

		float randomPitch = Random.Range (lowPitchRange, highPitchRange);

		efSource.pitch = randomPitch;

		efSource.clip = clips [randomIndex];

		efSource.Play ();

	}
}
