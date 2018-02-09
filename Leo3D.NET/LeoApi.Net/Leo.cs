using System.Security;
using System.Security.Permissions;
using System.Runtime.InteropServices;

namespace LeoApi.Net
{
    [SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
    public static class Leo
    {
        #region Public Constants

        public const double Version = 1.1;

        #endregion

        #region LeoAPI Native Library

        private const string LeoNativeLibrary = "LeoAPI.dll";

        #endregion

        #region Calling Convention

        /// <summary>
        /// Specifies the calling convention.
        /// </summary>
        /// <remarks>
        /// Specifies <see cref="CallingConvention.StdCall" /> for Windows and Linux.
        /// </remarks>
        private const CallingConvention Convention = CallingConvention.StdCall;

        #endregion

        #region Delegates

        /// <summary>
        /// Sets a callback function for Leonar3DO's receive event.
        /// <para>When Leonar3Do updates the state of the bird and glasses, a receive event</para>
        /// <para>is generated. With SetAfterReceive the application registers a callback</para>
        /// <para>function that will be called each time the event happens.</para>
        /// </summary>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void SetAfterRecieve();

        #endregion Delegates

        #region Functions

        #region Leonar3Do basic functions

        /// <summary>
        /// Initializes and connects the application to Leonar3Do System Software.
        /// <para>Initialize() should be called before any other LeoAPI function calls.</para>
        /// <para>The function may succeed even if Leonar3Do doesn't run, however, then the connection status is false. The success of the connection could be checked with calling the IsConnected() function.</para>
        /// </summary>
        [DllImport(LeoNativeLibrary, EntryPoint = "leoInitialize", CallingConvention = Convention), SuppressUnmanagedCodeSecurity]
        public static extern bool Initialize();

        /// <summary>
        /// Returns the version of LeoAPI.
        /// </summary>
        /// <returns>The LeoAPI version number</returns>
        [DllImport(LeoNativeLibrary, EntryPoint = "leoGetAPIVersion", CallingConvention = Convention), SuppressUnmanagedCodeSecurity]
        public static extern double GetApiVersion();

        /// <summary>
        /// Returns if the application is connected to the Leonar3Do System Software.
        /// </summary>
        /// <returns>Returns if the application is connected to the Leonar3Do System Software.</returns>
        [DllImport(LeoNativeLibrary, EntryPoint = "leoIsConnected", CallingConvention = Convention), SuppressUnmanagedCodeSecurity]
        public static extern bool IsConnected();

        /// <summary>
        /// Returns the version of the Leonar3Do System Software the application was connected to.
        /// </summary>
        [DllImport(LeoNativeLibrary, EntryPoint = "leoGetLeonar3DoVersion", CallingConvention = Convention), SuppressUnmanagedCodeSecurity]
        public static extern double GetLeonar3DoVersion();

        /// <summary>
        /// Returns the number of applications connected to Leonar3Do.
        /// </summary>
        [DllImport(LeoNativeLibrary, EntryPoint = "leoGetApplicationsCount", CallingConvention = Convention), SuppressUnmanagedCodeSecurity]
        public static extern uint GetApplicationsCount();

        /// <summary>
        /// Leonar3Do System Software registers the applications connected to it.
        /// <para>The name and version as registered by Leonar3Do can be set by LeoAPI with calls SetApplicationName() and SetApplicationVersion().</para>
        /// <para>By default, the application name is the name of the executable file.</para>
        /// </summary>
        /// <param name="name">Sets the name of the application for Leonar3Do.</param>
        [DllImport(LeoNativeLibrary, EntryPoint = "leoSetApplicationName", CallingConvention = Convention), SuppressUnmanagedCodeSecurity]
        public static extern void SetApplicationName(string name);

        /// <summary>
        /// Leonar3Do System Software registers the applications connected to it.
        /// <para>The name and version as registered by Leonar3Do can be set by LeoAPI with calls SetApplicationName() and SetApplicationVersion().</para>
        /// <para>By default, the application version is 0.</para>
        /// </summary>
        /// <param name="version">Sets the version of the application for Leonar3Do. </param>
        [DllImport(LeoNativeLibrary, EntryPoint = "leoSetApplicationVersion", CallingConvention = Convention), SuppressUnmanagedCodeSecurity]
        public static extern void SetApplicationVersion(double version);

        /// <summary>
        /// Returns the width of the display used by Leonar3Do. The width is measured in millimetres.
        /// </summary>
        /// <returns>Display width in millimeters.</returns>
        [DllImport(LeoNativeLibrary, EntryPoint = "leoGetMonitorWidth", CallingConvention = Convention), SuppressUnmanagedCodeSecurity]
        public static extern double GetMonitorWidth();

        /// <summary>
        /// Returns the height of the display used by Leonar3Do. The height is measured in millimetres.
        /// </summary>
        /// <returns>Display height in millimeters.</returns>
        [DllImport(LeoNativeLibrary, EntryPoint = "leoGetMonitorHeight", CallingConvention = Convention), SuppressUnmanagedCodeSecurity]
        public static extern double GetMonitorHeight();

        /// <summary>
        /// Returns the index of the display used by Leonar3Do.
        /// <para>This is the same value as the enumerant corresponding to the display returned by the operating system.</para>
        /// </summary>
        /// <returns>Index of the display</returns>
        [DllImport(LeoNativeLibrary, EntryPoint = "leoGetMonitorIndex", CallingConvention = Convention), SuppressUnmanagedCodeSecurity]
        public static extern uint GetMonitorIndex();

        /// <summary>
        /// Returns if the application is in stereo mode.
        /// </summary>
        /// <returns>Return true if the stereo mode is active.</returns>
        [DllImport(LeoNativeLibrary, EntryPoint = "leoIsStereoSwitchedOn", CallingConvention = Convention), SuppressUnmanagedCodeSecurity]
        public static extern bool IsStereoSwitchedOn();

        /// <summary>
        /// Returns if bird is visible by the sensors.
        /// </summary>
        /// <returns>Return true if bird is visible by the sensors</returns>
        [DllImport(LeoNativeLibrary, EntryPoint = "leoIsBirdVisible", CallingConvention = Convention), SuppressUnmanagedCodeSecurity]
        public static extern bool IsBirdVisible();

        /// <summary>
        /// Returns the position and orientation of the bird in LeoSpace.
        /// </summary>
        [DllImport(LeoNativeLibrary, EntryPoint = "leoGetBirdPosition", CallingConvention = Convention), SuppressUnmanagedCodeSecurity]
        public static extern void GetBirdPosition(out double posX, out double posY, out double posZ, out double rotX, out double rotY, out double rotZ);

        /// <summary>
        /// Returns if the small button on the bird is pressed.
        /// </summary>
        [DllImport(LeoNativeLibrary, EntryPoint = "leoGetSmallButtonState", CallingConvention = Convention), SuppressUnmanagedCodeSecurity]
        public static extern bool GetSmallButtonState();

        /// <summary>
        /// Returns the press state of the big button. 
        /// </summary>
        /// <returns>The returned number is the ratio of the press state in the range [0,1].</returns>
        [DllImport(LeoNativeLibrary, EntryPoint = "leoGetBigButtonState", CallingConvention = Convention), SuppressUnmanagedCodeSecurity]
        public static extern double GetBigButtonState();

        /// <summary>
        /// Returns if glasses are visible by the sensors.
        /// </summary>
        [DllImport(LeoNativeLibrary, EntryPoint = "leoAreGlassesVisible", CallingConvention = Convention), SuppressUnmanagedCodeSecurity]
        public static extern bool AreGlassesVisible();

        /// <summary>
        /// Returns the position of the glasses in LeoSpace.
        /// </summary>
        [DllImport(LeoNativeLibrary, EntryPoint = "leoGetGlassesPosition", CallingConvention = Convention), SuppressUnmanagedCodeSecurity]
        public static extern void GetGlassesPosition(out double posX, out double posY, out double posZ, out double rotX, out double rotY, out double rotZ);

        /// <summary>
        /// Returns the position of left and right eyes in LeoSpace.
        /// </summary>
        [DllImport(LeoNativeLibrary, EntryPoint = "leoGetEyesPosition", CallingConvention = Convention), SuppressUnmanagedCodeSecurity]
        public static extern void GetEyesPosition(out double leftX, out double leftY, out double leftZ, out double rightX, out double rightY, out double rightZ);

        /// <summary>
        /// Switches stereo on.
        /// </summary>
        [DllImport(LeoNativeLibrary, EntryPoint = "leoSetStereo", CallingConvention = Convention), SuppressUnmanagedCodeSecurity]
        public static extern void SetStereo(bool stereo);

        /// <summary>
        /// Sets vibration level of the bird.
        /// </summary>
        /// <param name="vibration">The vibration level is in the range [0,1].</param>
        [DllImport(LeoNativeLibrary, EntryPoint = "leoSetBirdVibration", CallingConvention = Convention), SuppressUnmanagedCodeSecurity]
        public static extern void SetBirdVibration(double vibration);

        #endregion

        #endregion
    }
}