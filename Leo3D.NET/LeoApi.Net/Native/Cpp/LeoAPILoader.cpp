#include "LeoAPI.h"
#include <windows.h>

// ----------------------
//  LEOAPI.dll functions
// ----------------------
#define LEOAPI_DLL_CALL __stdcall

typedef void LEOAPI_DLL_CALL proc_leoInitialize();
typedef double LEOAPI_DLL_CALL proc_leoGetAPIVersion();
typedef bool LEOAPI_DLL_CALL proc_leoIsConnected();

typedef double LEOAPI_DLL_CALL proc_leoGetLeonar3DoVersion();
typedef unsigned LEOAPI_DLL_CALL proc_leoGetApplicationsCount();
typedef void LEOAPI_DLL_CALL proc_leoSetApplicationName(const char* name);
typedef void LEOAPI_DLL_CALL proc_leoSetApplicationVersion(double version);

typedef double LEOAPI_DLL_CALL proc_leoGetMonitorWidth();
typedef double LEOAPI_DLL_CALL proc_leoGetMonitorHeight();
typedef unsigned LEOAPI_DLL_CALL proc_leoGetMonitorIndex();
typedef bool LEOAPI_DLL_CALL proc_leoIsStereoSwitchedOn();

typedef bool LEOAPI_DLL_CALL proc_leoIsBirdVisible();
typedef void LEOAPI_DLL_CALL proc_leoGetBirdPosition(double *posX, double *posY, double *posZ,
                                                     double *rotX, double *rotY, double *rotZ);
typedef bool LEOAPI_DLL_CALL proc_leoGetSmallButtonState();
typedef double LEOAPI_DLL_CALL proc_leoGetBigButtonState();
typedef bool LEOAPI_DLL_CALL proc_leoAreGlassesVisible();
typedef void LEOAPI_DLL_CALL proc_leoGetGlassesPosition(double *posX, double *posY, double *posZ,
                                                        double *rotX, double *rotY, double *rotZ);
typedef void LEOAPI_DLL_CALL proc_leoGetEyesPosition(double *leftX, double *leftY, double *leftZ,
                                                     double *rightX, double *rightY, double *rightZ);

typedef void LEOAPI_DLL_CALL proc_leoSetStereo(bool stereo);
typedef void LEOAPI_DLL_CALL proc_leoSetBirdVibration(double vibration);
typedef void LEOAPI_DLL_CALL proc_leoSetAfterRecieve(void* receiveCB);

typedef void LEOAPI_DLL_CALL proc_lglInitialize();
typedef LGLContext LEOAPI_DLL_CALL proc_lglCreateContext(void *hDC, void *hGLRC);
typedef void LEOAPI_DLL_CALL proc_lglDeleteContext(LGLContext context);
typedef unsigned LEOAPI_DLL_CALL proc_lglGetLastError();
typedef void LEOAPI_DLL_CALL proc_lglSetLastError(unsigned lglError);
typedef unsigned LEOAPI_DLL_CALL proc_lglGetRenderCount(LGLContext context);
typedef bool LEOAPI_DLL_CALL proc_lglStartRender(LGLContext context, unsigned index, double *matrix);
typedef void LEOAPI_DLL_CALL proc_lglFinishRender(LGLContext context);
typedef void LEOAPI_DLL_CALL proc_lglGetProjection(LGLContext context, unsigned index, LGLProjection *proj);
typedef void LEOAPI_DLL_CALL proc_lglGetRenderSize(LGLContext context, unsigned index, unsigned *width, unsigned *height);
typedef const char* LEOAPI_DLL_CALL proc_lglGetRenderName(LGLContext context, unsigned index);
typedef unsigned LEOAPI_DLL_CALL proc_lglGetRenderEyeIndex(LGLContext context, unsigned index);
typedef unsigned LEOAPI_DLL_CALL proc_lglGetRenderEyeCount(LGLContext context, unsigned index);
typedef void LEOAPI_DLL_CALL proc_lglSetClippingPlane(LGLContext context, double neard, double fard);
typedef void LEOAPI_DLL_CALL proc_lglGetClippingPlane(LGLContext context, double *neard, double *fard);


proc_leoInitialize* _leoInitialize = 0;
proc_leoGetAPIVersion* _leoGetAPIVersion = 0;
proc_leoIsConnected* _leoIsConnected = 0;

proc_leoGetLeonar3DoVersion* _leoGetLeonar3DoVersion = 0;
proc_leoGetApplicationsCount* _leoGetApplicationsCount = 0;
proc_leoSetApplicationName* _leoSetApplicationName = 0;
proc_leoSetApplicationVersion* _leoSetApplicationVersion = 0;

proc_leoGetMonitorWidth* _leoGetMonitorWidth = 0;
proc_leoGetMonitorHeight* _leoGetMonitorHeight = 0;
proc_leoGetMonitorIndex* _leoGetMonitorIndex = 0;
proc_leoIsStereoSwitchedOn* _leoIsStereoSwitchedOn = 0;

proc_leoIsBirdVisible* _leoIsBirdVisible = 0;
proc_leoGetBirdPosition* _leoGetBirdPosition = 0;
proc_leoGetSmallButtonState* _leoGetSmallButtonState = 0;
proc_leoGetBigButtonState* _leoGetBigButtonState = 0;
proc_leoAreGlassesVisible* _leoAreGlassesVisible = 0;
proc_leoGetGlassesPosition* _leoGetGlassesPosition = 0;
proc_leoGetEyesPosition* _leoGetEyesPosition = 0;

proc_leoSetStereo* _leoSetStereo = 0;
proc_leoSetBirdVibration* _leoSetBirdVibration = 0;
proc_leoSetAfterRecieve* _leoSetAfterRecieve = 0;

proc_lglInitialize* _lglInitialize = 0;
proc_lglCreateContext* _lglCreateContext = 0;
proc_lglDeleteContext* _lglDeleteContext = 0;
proc_lglGetLastError* _lglGetLastError = 0;
proc_lglSetLastError* _lglSetLastError = 0;
proc_lglGetRenderCount* _lglGetRenderCount = 0;
proc_lglStartRender* _lglStartRender = 0;
proc_lglFinishRender* _lglFinishRender = 0;
proc_lglGetProjection *_lglGetProjection = 0;
proc_lglGetRenderSize* _lglGetRenderSize = 0;
proc_lglGetRenderName* _lglGetRenderName = 0;
proc_lglGetRenderEyeIndex* _lglGetRenderEyeIndex = 0;
proc_lglGetRenderEyeCount* _lglGetRenderEyeCount = 0;
proc_lglSetClippingPlane *_lglSetClippingPlane = 0;
proc_lglGetClippingPlane *_lglGetClippingPlane = 0;


// -------------------------------------------
//  definition of LeoAPI static lib functions
// -------------------------------------------
#define GET_FUNCTION(handler, name) \
	_##name = (proc_##name *) GetProcAddress(handler, #name); \
	if(_##name == NULL) \
		return false;


bool leoInitialize()
{
    HMODULE leoapi_dll = LoadLibraryA("LeoAPI.dll");
    if (!leoapi_dll)
    {
        return false;
    }

    // loading functions
    GET_FUNCTION(leoapi_dll, leoInitialize);
    _leoInitialize();

    GET_FUNCTION(leoapi_dll, leoGetAPIVersion);
    double version = leoGetAPIVersion();

    GET_FUNCTION(leoapi_dll, leoIsConnected);

    GET_FUNCTION(leoapi_dll, leoGetLeonar3DoVersion);
    GET_FUNCTION(leoapi_dll, leoGetApplicationsCount);
    GET_FUNCTION(leoapi_dll, leoSetApplicationName);
    GET_FUNCTION(leoapi_dll, leoSetApplicationVersion);

    GET_FUNCTION(leoapi_dll, leoGetMonitorWidth);
    GET_FUNCTION(leoapi_dll, leoGetMonitorHeight);
    GET_FUNCTION(leoapi_dll, leoGetMonitorIndex);
    GET_FUNCTION(leoapi_dll, leoIsStereoSwitchedOn);

    GET_FUNCTION(leoapi_dll, leoIsBirdVisible);
    GET_FUNCTION(leoapi_dll, leoGetBirdPosition);
    GET_FUNCTION(leoapi_dll, leoGetSmallButtonState);
    GET_FUNCTION(leoapi_dll, leoGetBigButtonState);
    GET_FUNCTION(leoapi_dll, leoAreGlassesVisible);
    GET_FUNCTION(leoapi_dll, leoGetGlassesPosition);
    GET_FUNCTION(leoapi_dll, leoGetEyesPosition);
    
    GET_FUNCTION(leoapi_dll, leoSetStereo);
    GET_FUNCTION(leoapi_dll, leoSetBirdVibration);
    GET_FUNCTION(leoapi_dll, leoSetAfterRecieve);

    GET_FUNCTION(leoapi_dll, lglInitialize);
    GET_FUNCTION(leoapi_dll, lglCreateContext);
    GET_FUNCTION(leoapi_dll, lglDeleteContext);
    GET_FUNCTION(leoapi_dll, lglGetLastError);
    GET_FUNCTION(leoapi_dll, lglSetLastError);
    GET_FUNCTION(leoapi_dll, lglGetRenderCount);
    GET_FUNCTION(leoapi_dll, lglStartRender);
    GET_FUNCTION(leoapi_dll, lglFinishRender);
    GET_FUNCTION(leoapi_dll, lglGetProjection);
    GET_FUNCTION(leoapi_dll, lglGetRenderSize);
    GET_FUNCTION(leoapi_dll, lglSetClippingPlane);
    GET_FUNCTION(leoapi_dll, lglGetClippingPlane);

    if (version > 1.1 - 0.0001)
    {
        GET_FUNCTION(leoapi_dll, lglGetRenderName);
        GET_FUNCTION(leoapi_dll, lglGetRenderEyeIndex);
        GET_FUNCTION(leoapi_dll, lglGetRenderEyeCount);
    }
    
    return true;
}

double leoGetAPIVersion()
{
    return _leoGetAPIVersion();
}

bool leoIsConnected()
{
    return _leoIsConnected();
}


double leoGetLeonar3DoVersion()
{
    return _leoGetLeonar3DoVersion();
}

unsigned leoGetApplicationsCount()
{
    return _leoGetApplicationsCount();
}

void leoSetApplicationName(const char* name)
{
    _leoSetApplicationName(name);
}

void leoSetApplicationVersion(double version)
{
    _leoSetApplicationVersion(version);
}


double leoGetMonitorWidth()
{
    return _leoGetMonitorWidth();
}

double leoGetMonitorHeight()
{
    return _leoGetMonitorHeight();
}

unsigned leoGetMonitorIndex()
{
    return _leoGetMonitorIndex();
}

bool leoIsStereoSwitchedOn()
{
    return _leoIsStereoSwitchedOn();
}


bool leoIsBirdVisible()
{
    return _leoIsBirdVisible();
}

void leoGetBirdPosition(double *posX, double *posY, double *posZ,
                        double *rotX, double *rotY, double *rotZ)
{
    _leoGetBirdPosition(posX, posY, posZ, rotX, rotY, rotZ);
}

bool leoGetSmallButtonState()
{
    return _leoGetSmallButtonState();
}

double leoGetBigButtonState()
{
    return _leoGetBigButtonState();
}


bool leoAreGlassesVisible()
{
    return _leoAreGlassesVisible();
}

void leoGetGlassesPosition(double *posX, double *posY, double *posZ,
                           double *rotX, double *rotY, double *rotZ)
{
    _leoGetGlassesPosition(posX, posY, posZ, rotX, rotY, rotZ);
}

void leoGetEyesPosition(double *leftX, double *leftY, double *leftZ,
                        double *rightX, double *rightY, double *rightZ)
{
    _leoGetEyesPosition(leftX, leftY, leftZ, rightX, rightY, rightZ);
}


void leoSetStereo(bool stereo)
{
    _leoSetStereo(stereo);
}

void leoSetBirdVibration(double vibration)
{
    _leoSetBirdVibration(vibration);
}

void leoSetAfterRecieve(void* receiveCB)
{
    _leoSetAfterRecieve(receiveCB);
}


void lglInitialize()
{
    _lglInitialize();
}

LGLContext lglCreateContext(void *hDC, void *hGLRC)
{
    if (hDC == 0 || hGLRC == 0)
    {
        return _lglCreateContext(wglGetCurrentDC(), wglGetCurrentContext());
    }
    else
    {
        return _lglCreateContext(hDC, hGLRC);
    }
}

void lglDeleteContext(LGLContext context)
{
    _lglDeleteContext(context);
}

unsigned lglGetLastError()
{
    return _lglGetLastError();
}

void lglSetLastError(unsigned lglError)
{
    _lglSetLastError(lglError);
}

unsigned lglGetRenderCount(LGLContext context)
{
    return _lglGetRenderCount(context);
}

bool lglStartRender(LGLContext context, unsigned index, double *matrix)
{
    return _lglStartRender(context, index, matrix);
}

void lglFinishRender(LGLContext context)
{
    _lglFinishRender(context);
}

void lglGetProjection(LGLContext context, unsigned index, LGLProjection *proj)
{
   _lglGetProjection(context, index, proj);
}

void lglGetRenderSize(LGLContext context, unsigned index, unsigned *width, unsigned *height)
{
    _lglGetRenderSize(context, index, width, height);
}

const char* lglGetRenderName(LGLContext context, unsigned index)
{
    return _lglGetRenderName(context, index);
}

unsigned lglGetRenderEyeIndex(LGLContext context, unsigned index)
{
    return _lglGetRenderEyeIndex(context, index);
}

unsigned lglGetRenderEyeCount(LGLContext context, unsigned index)
{
    return _lglGetRenderEyeCount(context, index);
}

void lglSetClippingPlane(LGLContext context, double neard, double fard)
{
    _lglSetClippingPlane(context, neard, fard);
}

void lglGetClippingPlane(LGLContext context, double *neard, double *fard)
{
    _lglGetClippingPlane(context, neard, fard);
}
