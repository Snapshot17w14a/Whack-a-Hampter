using GXPEngine.SceneManagement;
using GXPEngine.Scenes;
using GXPEngine;

class MyGame : Game {

	private Sprite _mouse;

	public MyGame() : base(960, 640, false, false, 1280, 720,true) 
	{
		GameData.Initialize();
		AddChild(_mouse = new Sprite("mouse.png"));
		_mouse.SetOrigin(_mouse.width / 2, _mouse.height / 2);
		ShowMouse(GameData.ShowMouse);
		SceneManager.LoadScene(GameData.SceneToLoad);
        PlayBGM();
	}

	public static void Main (string[] args)
	{
		new MyGame().Start();
    }

	private void Update()
	{
		SetChildIndex(_mouse, GetChildCount() - 1);
		_mouse.SetXY(Input.mouseX, Input.mouseY);
	}

    private SoundChannel background_music;
    private SoundChannel Bush;
    private SoundChannel Fire;
    private SoundChannel HitHampter;
    private SoundChannel HitWall;
    private SoundChannel LevelPass;
    private SoundChannel MenuSelect;
    private SoundChannel Mud;
    // private SoundChannel Putt;
    private SoundChannel Water;

    public void PlayBGM()
    {
        background_music = new Sound("Sound/BGM.wav", true, true).Play();
    }

    public void BushSFX()
    {
        Bush = new Sound("Sound/Bush.wav", false, false).Play();
    }

    public void FireSFX()
    {
        Fire = new Sound("Sound/Fire.wav", false, false).Play();
    }

    public void HitHampterSFX()
    {
        HitHampter = new Sound("Sound/HitHampter.wav", false, false).Play();
    }

    public void HitWallSFX()
    {
        HitWall = new Sound("Sound/HitWall.wav", false, false).Play();
    }

    public void LevelPassSFX()
    {
        LevelPass = new Sound("Sound/LevelPass.wav", false, false).Play();
    }

    public void MenuSelectSFX()
    {
        MenuSelect = new Sound("Sound/MenuSelect.wav", false, false).Play();
    }

    public void MudSFX()
    {
        Mud = new Sound("Sound/Mud.wav", false, false).Play();
    }

    public void WaterSFX()
    {
        Water = new Sound("Sound/Water.wav", false, false).Play();
    }
}