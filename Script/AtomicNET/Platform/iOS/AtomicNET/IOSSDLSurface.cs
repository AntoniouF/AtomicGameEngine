using System;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using CoreGraphics;
using UIKit;
namespace AtomicEngine
{
	public class IOSSDLSurface : UIKit.UIView
	{
        // This introduced some cruft into UrhoSharp cruft into SDL, need to evaluate it's use in MoveWindow
		//[DllImport("@rpath/AtomicNETNative.framework/AtomicNETNative", CallingConvention = CallingConvention.Cdecl)]
		//static extern void SDL_SetExternalViewPlaceholder(IntPtr viewPtr, IntPtr windowPtr);

		TaskCompletionSource<bool> initTaskSource = new TaskCompletionSource<bool>();

		public Task InitializeTask => initTaskSource.Task;

		public IOSSDLSurface()
		{
			IOSPlatformInitializer.OnInited();
			initTaskSource = new TaskCompletionSource<bool>();
			BackgroundColor = UIColor.Black;
		}

		public IOSSDLSurface(CGRect frame) : base(frame)
		{
			IOSPlatformInitializer.OnInited();
			BackgroundColor = UIColor.Black;
			initTaskSource = new TaskCompletionSource<bool>();
		}

		public void Pause()
		{
            var engine = AtomicNET.GetSubsystem<Engine>();

            if (engine == null)
                return;

			engine.PauseMinimized = true;

            SDLEvents.SendWindowEvent(SdlWindowEvent.SDL_WINDOWEVENT_FOCUS_LOST);
			SDLEvents.SendWindowEvent(SdlWindowEvent.SDL_WINDOWEVENT_MINIMIZED);
		}

		public void Resume()
		{
            var engine = AtomicNET.GetSubsystem<Engine>();

            if (engine == null)
                return;

            engine.PauseMinimized = false;

            SDLEvents.SendWindowEvent(SdlWindowEvent.SDL_WINDOWEVENT_FOCUS_GAINED);
			SDLEvents.SendWindowEvent(SdlWindowEvent.SDL_WINDOWEVENT_RESTORED);
		}

		public override void MovedToWindow()
		{
            
			base.MovedToWindow();

            /*
			var wndHandle = Window?.Handle;
			SDL_SetExternalViewPlaceholder(Handle, wndHandle ?? IntPtr.Zero);
			if (wndHandle != null) {
				initTaskSource.TrySetResult (true);
			} else {
				initTaskSource = new TaskCompletionSource<bool> ();
			}
			*/
		}
	}
}
