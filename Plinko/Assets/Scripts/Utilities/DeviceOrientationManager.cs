using UnityEngine;

public class DeviceOrientationManager : MonoBehaviour
{
	private const float ORIENTATION_CHECK_INTERVAL = 0.5f;

	private float nextOrientationCheckTime;

	private static ScreenOrientation m_currentOrientation;
	public static ScreenOrientation CurrentOrientation
	{
		get
		{
			return m_currentOrientation;
		}
		private set
		{
			if( m_currentOrientation != value )
			{
				m_currentOrientation = value;
				Screen.orientation = value;

				if( OnScreenOrientationChanged != null )
					OnScreenOrientationChanged( value );
			}
		}
	}

	public static bool AutoRotateScreen = true;
	public static event System.Action<ScreenOrientation> OnScreenOrientationChanged = null;

	[RuntimeInitializeOnLoadMethod( RuntimeInitializeLoadType.AfterSceneLoad )]
	private static void Init()
	{
		DontDestroyOnLoad( new GameObject( "DeviceOrientationManager", typeof( DeviceOrientationManager ) ) );
	}

	void Awake()
	{
		m_currentOrientation = Screen.orientation;
		nextOrientationCheckTime = Time.realtimeSinceStartup + 1f;
	}

	void Update()
	{
		if( !AutoRotateScreen )
			return;
		
		if( Time.realtimeSinceStartup >= nextOrientationCheckTime )
		{
			DeviceOrientation orientation = Input.deviceOrientation;
			if( orientation == DeviceOrientation.Portrait || orientation == DeviceOrientation.PortraitUpsideDown ||
				orientation == DeviceOrientation.LandscapeLeft || orientation == DeviceOrientation.LandscapeRight )
			{
				if( orientation == DeviceOrientation.LandscapeLeft )
				{
					if( Screen.autorotateToLandscapeLeft )
						CurrentOrientation = ScreenOrientation.LandscapeLeft;
				}
				else if( orientation == DeviceOrientation.LandscapeRight )
				{
					if( Screen.autorotateToLandscapeRight )
						CurrentOrientation = ScreenOrientation.LandscapeRight;
				}
				else if( orientation == DeviceOrientation.PortraitUpsideDown )
				{
					if( Screen.autorotateToPortraitUpsideDown )
						CurrentOrientation = ScreenOrientation.PortraitUpsideDown;
				}
				else
				{
					if( Screen.autorotateToPortrait )
						CurrentOrientation = ScreenOrientation.Portrait;
				}
			}

			nextOrientationCheckTime = Time.realtimeSinceStartup + ORIENTATION_CHECK_INTERVAL;
		}
	}

	public static void ForceOrientation( ScreenOrientation orientation )
	{
		if( orientation == ScreenOrientation.AutoRotation )
			AutoRotateScreen = true;
		else if( orientation != ScreenOrientation.Unknown )
		{
			AutoRotateScreen = false;
			CurrentOrientation = orientation;
		}
	}
}