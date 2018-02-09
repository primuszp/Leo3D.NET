/*
 *
 * LeoAPI header file
 * Copyright 2010 of 3D for All Ltd. (www.Leonar3Do.com). 
 *
 * Warning! This document is an integral and inseparable part of the 
 * Leonar3Do Software Development Kit version: 1.0., protected by the 
 * Copyright 2010 of 3D for All Ltd. (www.Leonar3Do.com). 
 * All rights are reserved. Unauthorized reproduction or distribution 
 * of Leonar3Do Software Development Kit version: 1.0., or any portion 
 * of it, may result in severe civil and criminal penalties, and will 
 * be prosecuted to the maximum extent possible under the governing law 
 * specified it in the „License Terms for 3D for All Ltd.’s Leonar3Do 
 * Software Development Kit version: 1.0”. 
 *
 */

#ifndef LEO_API_H
#define LEO_API_H

#ifdef LEOAPI_EXPORTS
#   define LEOAPI __declspec(dllexport)
#else
#   define LEOAPI
#endif

#define LEOAPI_CALL __cdecl

#ifdef __cplusplus 
extern "C" 
{
#endif

typedef struct _LGLContext *LGLContext;

/**
    Contains parameters for a projection. 

    These parameters hold the left bottom, right bottom and left top corners 
    of the projection plane and the position of the camera's focus point.
*/
struct LGLProjection
{
    double lbX, lbY, lbZ;
    double rbX, rbY, rbZ;
    double ltX, ltY, ltZ;
    double focusX, focusY, focusZ;
};


/**
 * \defgroup LeoAPI LeoAPI functions
 */
/*@{*/

/** Initializes and connects the application to Leonar3Do System Software.

    leoInitialize() should be called before any other LeoAPI function calls. 
    The function may succeed even if Leonar3Do doesn't run, however, then 
    the connection status is false. The success of the connection could be 
    checked with calling the leoIsConnected() function. 
*/
LEOAPI bool LEOAPI_CALL leoInitialize();

/** Returns the version of LeoAPI. */
LEOAPI double LEOAPI_CALL leoGetAPIVersion();

/** Returns if the application is connected to the Leonar3Do System Software. */
LEOAPI bool LEOAPI_CALL leoIsConnected();


/** Returns the version of the Leonar3Do System Software the application was connected to. */
LEOAPI double LEOAPI_CALL leoGetLeonar3DoVersion();

/** Returns the number of applications connected to Leonar3Do. */
LEOAPI unsigned LEOAPI_CALL leoGetApplicationsCount();

/** Sets the name of the application for Leonar3Do. 

    Leonar3Do System Software registers the applications connected to it. 
    The name and version as registered by Leonar3Do can be set by LeoAPI 
    with calls leoSetApplicationName() and leoSetApplicationVersion(). 
    By default, the application name is the name of the executable file. 
*/
LEOAPI void LEOAPI_CALL leoSetApplicationName(const char* name);

/** Sets the version of the application for Leonar3Do. 

    Leonar3Do System Software registers the applications connected to it. 
    The name and version as registered by Leonar3Do can be set by LeoAPI with 
    calls leoSetApplicationName() and leoSetApplicationVersion(). 
    By default, the application version is 0. 
*/
LEOAPI void LEOAPI_CALL leoSetApplicationVersion(double version);


/** Returns the width of the display used by Leonar3Do. 

    The width is measured in millimetres. 
*/
LEOAPI double LEOAPI_CALL leoGetMonitorWidth();

/** Returns the height of the display used by Leonar3Do. 

    The height is measured in millimetres. 
*/
LEOAPI double LEOAPI_CALL leoGetMonitorHeight();

/** Returns the index of the display used by Leonar3Do. 

    This is the same value as the enumerant corresponding to the display 
    returned by the operating system. 
*/
LEOAPI unsigned LEOAPI_CALL leoGetMonitorIndex();

/** Returns if the application is in stereo mode. */
LEOAPI bool LEOAPI_CALL leoIsStereoSwitchedOn();


/** Returns if bird is visible by the sensors. */
LEOAPI bool LEOAPI_CALL leoIsBirdVisible();

/** Returns the position and orientation of the bird in LeoSpace. */
LEOAPI void LEOAPI_CALL leoGetBirdPosition(double *posX, double *posY, double *posZ,
                                           double *rotX, double *rotY, double *rotZ);

/** Returns if the small button on the bird is pressed. */
LEOAPI bool LEOAPI_CALL leoGetSmallButtonState();

/** Returns the press state of the big button. 

    The returned number is the ratio of the press state in the range [0,1].
*/
LEOAPI double LEOAPI_CALL leoGetBigButtonState();

/** Returns if glasses are visible by the sensors. */
LEOAPI bool LEOAPI_CALL leoAreGlassesVisible();

/** Returns the position of the glasses in LeoSpace. */
LEOAPI void LEOAPI_CALL leoGetGlassesPosition(double *posX, double *posY, double *posZ,
                                              double *rotX, double *rotY, double *rotZ);

/** Returns the position of left and right eyes in LeoSpace */
LEOAPI void LEOAPI_CALL leoGetEyesPosition(double *leftX, double *leftY, double *leftZ,
                                           double *rightX, double *rightY, double *rightZ);

/** Switches stereo on. */
LEOAPI void LEOAPI_CALL leoSetStereo(bool stereo);

/** Sets vibration level of the bird. 

    The vibration level is in the range [0,1].
*/
LEOAPI void LEOAPI_CALL leoSetBirdVibration(double vibration);

/** Sets a callback function for Leonar3DO's receive event.

    When Leonar3Do updates the state of the bird and glasses, a receive event 
    is generated. With SetAfterReceive the application registers a callback 
    function that will be called each time the event happens.

    The callback function has no attributes and has no return value, thus its form is:
    
    typedef void (* cbfunc)(void);
*/
LEOAPI void LEOAPI_CALL leoSetAfterRecieve(void* receiveCB);


/**
    LGL error codes returned by lglGetLastError()
*/
enum LGLErrors
{
    LGL_NO_ERROR,
    LGL_INVALID_OPENGL_CONTEXT,
    LGL_GL_VERSION_2_0_NOT_SUPPORTED,
    LGL_FBO_NOT_SUPPORTED,
    LGL_CONTEXT_NOT_CREATED,
    LGL_STEREO_MODE_NOT_SUPPORTED,
    LGL_CONTEXT_COUNT_OVERFLOW,
    LGL_CANT_RENDER_CONTEXT,
    LGL_INVALID_RENDER_INDEX,
    LGL_INVALID_CONTEXT,
};

/** Initializes LGL. 

    Before using LGL, it should be initialized by using lglInitialize(). 
    After this the application can create LGL Contexts and call any other
    LGL functions.
*/
LEOAPI void LEOAPI_CALL lglInitialize();

/** Creates an LGL Context.

    For creating an LGL context, an active OpenGL Rendering Context is 
    needed. Furthermore, the OpenGL Context must has a double-buffered 
    RGBA pixel-format with a depth-buffer. 

    @param hDC          handle to a device context
    @param hGLRC        handle to an active OpenGL Rendering Context which
                        has a double-buffered RGBA pixel-format with a 
                        depth-buffer.

    @returns On success, a valid LGLContext handle is returned. Otherwise a
             null-pointer is returned. To get extended error information then 
             call lglGetLastError(). The generated error codes could be:
             LGL_INVALID_OPENGL_CONTEXT         when hGLRC is not valid
             LGL_GL_VERSION_2_0_NOT_SUPPORTED   when OpenGL 2.0 is not supported
             LGL_FBO_NOT_SUPPORTED              when the extension GL_ARB_framebuffer_object is not supported
             LGL_CONTEXT_COUNT_OVERFLOW         when the maximum number of contexts are already created
             LGL_CONTEXT_NOT_CREATED            for other unknown reason
*/
LEOAPI LGLContext LEOAPI_CALL lglCreateContext(void *hDC, void *hGLRC);

/** Destroys an LGL Context */
LEOAPI void LEOAPI_CALL lglDeleteContext(LGLContext context);

/** Returns the last error code caused by an LGL function. */
LEOAPI unsigned LEOAPI_CALL lglGetLastError();

/** Sets the last error code */
LEOAPI void LEOAPI_CALL lglSetLastError(unsigned lglError);

/** Returns the number of views to render. */
LEOAPI unsigned LEOAPI_CALL lglGetRenderCount(LGLContext context);

/** Issues starting of rendering to the destination framebuffer of the view
    associated with index.

    @param index    the index of the view to be rendered
    @param matrix   pointer to 16 consecutive double values, which are set
                    by the function as the elements of a 4x4 column-major 
                    matrix

    Subsequent OpenGL drawing commands will be targeted to the destination 
    framebuffer registered for the view associated with index until another 
    lglStartRender() with a different index or an lglFinishRender() is called. 
    For a given view, lglStartRender() should be called only once until 
    lglFinishRender() is called. 
    
    The function fills a projection matrix. This projection matrix is recommended 
    to be used during the rendering. The projection matrix could also be manually 
    created with retrieving the projection parameters for the view by 
    lglGetProjection() and using a near and far clipping distance. The projection 
    matrix constructed by lglStartRender() uses near and far distances that was 
    previously set by lglSetClippingPlane().

    After lglStartRender() the associated framebuffer is cleared automatically. 
    This is done because of compatibility with augmented reality renderings, when 
    after lglStartRender(), some information may be resides in the framebuffer that 
    is important. It can happen when System Software asks us to render the scene for 
    a web-camera and compositing our rendering with the camera's image. Therefore, 
    you shouldn't clear the screen.
*/
LEOAPI bool LEOAPI_CALL lglStartRender(LGLContext context, unsigned index, double *matrix);

/** Creates the proper output that to be derived from all rendered views. 

    This will refresh the contents of the default framebuffers associated 
    with the LGLContext, that is it calls SwapBuffers() for each device. 
    Therefore, you shouldn't call SwapBuffers() at any time.
*/
LEOAPI void LEOAPI_CALL lglFinishRender(LGLContext context);

/** Returns the projection parameters for a given view. 

    @param index    the index of the view
    @param matrix   pointer to an LGLProjection structure that
                    will be filled by the function
*/
LEOAPI void LEOAPI_CALL lglGetProjection(LGLContext context, unsigned index, LGLProjection *matrix);

/** Returns the dimensions of the framebuffer associated with index. */
LEOAPI void LEOAPI_CALL lglGetRenderSize(LGLContext context, unsigned index, unsigned *width, unsigned *height);

/** Returns the name of the view associated with index.

    @returns For views associated with the user-created LGL context, the returned string is "Self".
             For views associated with LeoCapture, the returned string is "LeoCapture".
             For views associated with LeoConf, the returned string is "LeoConf".
*/
LEOAPI const char* LEOAPI_CALL lglGetRenderName(LGLContext context, unsigned index);

/** Returns the eye index for the view associated with index. */
LEOAPI unsigned lglGetRenderEyeIndex(LGLContext context, unsigned index);

/** Returns the total number of eyes (viewpoints) needed to entirely render the display device associated with index. */
LEOAPI unsigned lglGetRenderEyeCount(LGLContext context, unsigned index);

/** Sets the near and far clipping distances. 

    lglStartRender() creates a projection matrix. That matrix is constructed by
    the projection parameters returned with lglGetProjection() and the near and
    far clipping distances set by lglSetClippingPlane. The default values for
    the clipping distances are 0.1 and 100.0
*/
LEOAPI void LEOAPI_CALL lglSetClippingPlane(LGLContext context, double neard, double fard);

/** Returns the near and far clipping distances */
LEOAPI void LEOAPI_CALL lglGetClippingPlane(LGLContext context, double *neard, double *fard); 

/*@}*/

#ifdef __cplusplus
}
#endif

#endif