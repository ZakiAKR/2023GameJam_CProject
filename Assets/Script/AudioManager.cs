using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] public AudioClip[] sound=new AudioClip[28];
    AudioSource audioSource;

    public float soundVolume;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) { audioSource.volume = soundVolume; audioSource.PlayOneShot(sound[0]);  }
        if (Input.GetKeyDown(KeyCode.B)) { audioSource.volume = soundVolume; audioSource.PlayOneShot(sound[1]);  }
        if (Input.GetKeyDown(KeyCode.C)) { audioSource.volume = soundVolume; audioSource.PlayOneShot(sound[2]);  }
        if (Input.GetKeyDown(KeyCode.D)) { audioSource.volume = soundVolume; audioSource.PlayOneShot(sound[3]);  }
        if (Input.GetKeyDown(KeyCode.E)) { audioSource.volume = soundVolume; audioSource.PlayOneShot(sound[4]);  }
        if (Input.GetKeyDown(KeyCode.F)) { audioSource.volume = soundVolume; audioSource.PlayOneShot(sound[5]);  }
        if (Input.GetKeyDown(KeyCode.G)) { audioSource.volume = soundVolume; audioSource.PlayOneShot(sound[6]);  }
        if (Input.GetKeyDown(KeyCode.H)) { audioSource.volume = soundVolume; audioSource.PlayOneShot(sound[7]);  }
        if (Input.GetKeyDown(KeyCode.I)) { audioSource.volume = soundVolume; audioSource.PlayOneShot(sound[8]);  }
        if (Input.GetKeyDown(KeyCode.J)) { audioSource.volume = soundVolume; audioSource.PlayOneShot(sound[9]);  }
        if (Input.GetKeyDown(KeyCode.K)) { audioSource.volume = soundVolume; audioSource.PlayOneShot(sound[10]);  }
        if (Input.GetKeyDown(KeyCode.L)) { audioSource.volume = soundVolume; audioSource.PlayOneShot(sound[11]);  }
        if (Input.GetKeyDown(KeyCode.M)) { audioSource.volume = soundVolume; audioSource.PlayOneShot(sound[12]);  }
        if (Input.GetKeyDown(KeyCode.N)) { audioSource.volume = soundVolume; audioSource.PlayOneShot(sound[13]);  }
        if (Input.GetKeyDown(KeyCode.O)) { audioSource.volume = soundVolume; audioSource.PlayOneShot(sound[14]);  }
        if (Input.GetKeyDown(KeyCode.P)) { audioSource.volume = soundVolume; audioSource.PlayOneShot(sound[15]);  }
        if (Input.GetKeyDown(KeyCode.Q)) { audioSource.volume = soundVolume; audioSource.PlayOneShot(sound[16]);  }
        if (Input.GetKeyDown(KeyCode.R)) { audioSource.volume = soundVolume; audioSource.PlayOneShot(sound[17]);  }
        if (Input.GetKeyDown(KeyCode.S)) { audioSource.volume = soundVolume; audioSource.PlayOneShot(sound[18]);  }
        if (Input.GetKeyDown(KeyCode.T)) { audioSource.volume = soundVolume; audioSource.PlayOneShot(sound[19]);  }
        if (Input.GetKeyDown(KeyCode.U)) { audioSource.volume = soundVolume; audioSource.PlayOneShot(sound[20]);  }
        if (Input.GetKeyDown(KeyCode.V)) { audioSource.volume = soundVolume; audioSource.PlayOneShot(sound[21]);  }
        if (Input.GetKeyDown(KeyCode.W)) { audioSource.volume = soundVolume; audioSource.PlayOneShot(sound[22]);  }
        if (Input.GetKeyDown(KeyCode.X)) { audioSource.volume = soundVolume; audioSource.PlayOneShot(sound[23]);  }
        if (Input.GetKeyDown(KeyCode.Y)) { audioSource.volume = soundVolume; audioSource.PlayOneShot(sound[24]);  }
        if (Input.GetKeyDown(KeyCode.Z)) { audioSource.volume = soundVolume; audioSource.PlayOneShot(sound[25]);  }
        if (Input.GetKeyDown(KeyCode.Minus)) { audioSource.volume = soundVolume; audioSource.PlayOneShot(sound[26]);  }
        if (Input.GetKeyDown(KeyCode.Space)) { audioSource.PlayOneShot(sound[27]);  }
    }

    public void On_ClickButtonSE()
    {
        audioSource.PlayOneShot(sound[27]);
    }
}
