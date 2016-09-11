using System.Linq;
using System.Runtime.InteropServices;
using Foundation;

namespace AtomicEngine
{
	internal static class IOSPlatformInitializer
	{
		[DllImport(Constants.LIBNAME)]
		static extern void SDL_IOS_Init(string resDir, string docDir);

		internal static void OnInited()
		{
			string docsDir = NSSearchPath.GetDirectories(NSSearchPathDirectory.DocumentDirectory, NSSearchPathDomain.All, true).FirstOrDefault();
			string resourcesDir = NSBundle.MainBundle.ResourcePath;
			SDL_IOS_Init(resourcesDir, docsDir);
            SDLEvents.SetMainReady();
			NSFileManager.DefaultManager.ChangeCurrentDirectory(resourcesDir);
		}
	}
}
