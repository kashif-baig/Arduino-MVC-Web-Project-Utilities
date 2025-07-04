﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Arduino.MVC.NewProj.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Arduino.MVC.NewProj.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to $headingtext$
        ///@viewname $viewname$
        ///@mimetype ApplicationJson
        ///@modeltype $viewname$Model*
        ///@include &quot;$viewname$Model.h&quot;
        ///
        ///{
        ///    &quot;result&quot;:&quot;OK&quot;
        ///}.
        /// </summary>
        internal static string Api_asp {
            get {
                return ResourceManager.GetString("Api_asp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to #ifndef $viewname_upper$VIEW_H
        ///#define $viewname_upper$VIEW_H
        ///
        ///$headingtext$
        ///
        ///#include &quot;IView.h&quot;
        ///#include &quot;$viewname$Model.h&quot;
        ///
        ///class $viewname$View : public IView
        ///{
        ///public:
        ///	$viewname$View($viewname$Model *model, HttpRequest *request = getDefaultRequest()) : IView(ViewIdNotSpecified, NULL, request)
        ///	{
        ///		this-&gt;model = model;
        ///		this-&gt;viewMimeType = ApplicationJson;
        ///	}
        ///	$viewname$Model *getModel() const { return model; }
        ///
        ///protected:
        ///	$viewname$Model *model;
        ///
        ///	void onRender()
        ///	{
        ///		writeSt [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string Api_h {
            get {
                return ResourceManager.GetString("Api_h", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {
        ///    &quot;board&quot;: &quot;arduino:avr:uno&quot;,
        ///    &quot;port&quot;: &quot;COM6&quot;,
        ///    &quot;sketch&quot;: &quot;$projectname$.ino&quot;,
        ///    &quot;output&quot;: &quot;../.build/$projectname$&quot;,
        ///    &quot;programmer&quot;: &quot;Atmel STK500 development board&quot;
        ///}.
        /// </summary>
        internal static string arduino_json {
            get {
                return ResourceManager.GetString("arduino_json", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to #ifndef AUTHENTICATOR_H
        ///#define AUTHENTICATOR_H
        ///
        ///#define USER_MAX_COUNT           1
        ///#define USER_NAME_MAXLEN        9
        ///#define USER_PASS_MAXLEN        9
        ///#define AUTH_TICKET_MAXLEN      16
        ///#define AUTH_TICKET_EXPIRY_MS   300000
        ///
        ///class Authenticator;
        ///
        ///// Represents a single user profile.
        ///class UserInfo
        ///{
        ///public:
        ///    UserInfo()
        ///    {
        ///        clear();
        ///    };
        ///
        ///    void checkLastValidatedForExpiry()
        ///    {
        ///        if ((strlen(authTicket) &gt; 0) &amp;&amp; (millis() - lastValidated) &gt; (AUTH_TICKET_EXPIRY [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string Authenticator_h {
            get {
                return ResourceManager.GetString("Authenticator_h", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to @echo off
        ///REM Build views from asp files.
        ///
        ///CALL :NORMALIZEPATH &quot;%~p0..\asp_files&quot;
        ///SET source=%RETVAL%
        ///
        ///CALL :NORMALIZEPATH &quot;%~p0..&quot;
        ///SET dest=%RETVAL%
        ///
        ///O:\Projects\Arduino\Tools\MvcViewBuilder\Arduino.MVC.CodeGen.exe &quot;%source%&quot; &quot;%dest%&quot;
        ///
        ///:: ========== FUNCTIONS ==========
        ///EXIT /B
        ///
        ///:NORMALIZEPATH
        ///  SET RETVAL=%~f1
        ///  EXIT /B
        ///.
        /// </summary>
        internal static string BuildViews_script {
            get {
                return ResourceManager.GetString("BuildViews_script", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to #ifndef CONTENT_H
        ///#define CONTENT_H
        ///
        ///PROGMEM const char file_normal_min_css_gz[] = { 0x1F,0x8B,0x08,0x00,0x00,0x00,0x00,0x00,0x00,0x03,0x95,0x55,0xB9,0x8E,0xDB,0x30,0x10,0xFD,0x15,0xA5
        ///,0xD8,0x66,0x21,0xCA,0xF6,0x06,0x7B,0x80,0x42,0x9A,0x74,0x01,0x92,0x6E,0xBB,0x85,0x0B,0x4A,0x1C,0x49
        ///,0x8C,0x79,0x81,0xA4,0x7C,0xAC,0xA2,0x7F,0xCF,0xE8,0xB0,0x6C,0xF9,0x40,0x90,0xC2,0x86,0xC9,0xB9,0xDE
        ///,0xBC,0x79,0x43,0x2F,0x1E,0xBF,0x44,0xDA,0x38,0xC5,0xA4,0xF8,0x84,0x24,0xF7,0x3E,0xDA,0xBE,0x25,0xCB
        ///,0x64,0x15,0xFD,0 [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string Content_h {
            get {
                return ResourceManager.GetString("Content_h", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to         if (strcasecmp_P(argv[0], PSTR(&quot;api&quot;)) == 0)
        ///        {
        ///$serveapi$
        ///            return NULL;
        ///        }.
        /// </summary>
        internal static string Controller_ApiCheck {
            get {
                return ResourceManager.GetString("Controller_ApiCheck", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to #ifndef CONTROLLER_H
        ///#define CONTROLLER_H
        ///
        ///#include &quot;StdViews.h&quot;
        ///#include &quot;LayoutTemplateView.h&quot;
        ///
        ///$includes$
        ///#include &quot;IController.h&quot;
        ///#include &quot;IWebServer.h&quot;
        ///
        ///#include &quot;Content.h&quot;
        ///
        ///#include &lt;new&gt;
        ///
        ///// Determine amount of memory to allocate for views and models.
        ///static const size_t LARGEST_VIEW_SIZE =
        ///    STATIC_MAX(LARGEST_STD_VIEW_SIZE, /* always need this one. */
        ///$sizeofview$
        ///    0$staticmaxbrackets$);
        ///
        ///static const size_t LARGEST_MODEL_SIZE =
        ///$sizeofmodel$
        ///    0$staticmaxbrackets$;
        /// [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string Controller_h {
            get {
                return ResourceManager.GetString("Controller_h", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to         // Check if request is authenticated.
        ///        if (!request-&gt;isAuthenticated())
        ///        {
        ///            if (request-&gt;isAjax())
        ///            {
        ///                // Return &apos;Forbidden&apos; status code for unauthenticated AJAX requests.
        ///                return new (View) HttpStatusCodeView(403);
        ///            }
        ///            else if (strcasecmp_P(argv[0], PSTR(&quot;login&quot;)) != 0)
        ///            {
        ///                // Redirect to Login if not authenticated.
        ///                return new (View) HttpStatusCodeView(302, &quot;/ [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string Controller_isAuthenticated {
            get {
                return ResourceManager.GetString("Controller_isAuthenticated", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to     // Returns true if the supplied ticket successfully authenticates, false otherwise.
        ///    bool onAuthenticate(const char *ticket)
        ///    {
        ///        // Find user using authentication ticket from cookie.
        ///        // Updates their lastValidated time (if the ticket is valid).
        ///        const char *authenticatedUser = auth.getAuthenticatedUser(ticket);
        ///
        ///        if (authenticatedUser != NULL)
        ///        {
        ///            // Let web server know who the authenticated user is.
        ///            webServer-&gt;setAuthenticatedUs [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string Controller_onAuth {
            get {
                return ResourceManager.GetString("Controller_onAuth", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to             if (strcasecmp_P(argv[1], PSTR(&quot;$viewnamestripped$&quot;)) == 0)
        ///            {
        ///                $viewname$Model *model = new (Model) $viewname$Model();
        ///                $viewname$View *$viewname_camel$View = new (View) $viewname$View(model);
        ///                return $viewname_camel$View;
        ///            }.
        /// </summary>
        internal static string Controller_ServeApi {
            get {
                return ResourceManager.GetString("Controller_ServeApi", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to         if ($defaultcheck$strcasecmp_P(argv[0], PSTR(&quot;$viewname$&quot;)) == 0)
        ///        {
        ///            $viewname$Model *model = new (Model) $viewname$Model();
        ///            $viewname$View *$viewname_camel$View =  new (View) $viewname$View(model);
        ///            return new (Layout) LayoutTemplateView($viewname_camel$View);
        ///        }.
        /// </summary>
        internal static string Controller_ServeView {
            get {
                return ResourceManager.GetString("Controller_ServeView", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to /*
        ///
        ///	Generated using Arduino MVC view builder at https://mvc-view-builder.cohesivecomputing.co.uk/
        ///	For more information and help, visit https://www.cohesivecomputing.co.uk/code-storm/arduino-mvc-web-server/
        ///
        ///	All our hackatronics (and code storm) projects are free for personal use. If you find them helpful or useful,
        ///	please spread the word.
        ///
        ///*/.
        /// </summary>
        internal static string Heading_ViewClass {
            get {
                return ResourceManager.GetString("Heading_ViewClass", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to @rem Select all text then copy/paste in to panel at https://mvc-view-builder.cohesivecomputing.co.uk/.
        ///@rem Copy/paste resulting code back in to $viewname$View.h
        ///@rem Do not leave any blank lines at top or between directives..
        /// </summary>
        internal static string Heading_ViewSource {
            get {
                return ResourceManager.GetString("Heading_ViewSource", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to #ifndef ILAYOUTCONTENTVIEW_H
        ///#define ILAYOUTCONTENTVIEW_H
        ///
        ///#include &quot;IView.h&quot;
        ///
        ///// Layout view interface for use by LayoutTemplateView
        ///class ILayoutContentView : public IView
        ///{
        ///public:
        ///    // Renders page title to response stream (required).
        ///    virtual void renderTitle () = 0;
        ///    // Renders body markup to response stream (required).
        ///    virtual void renderBody() = 0;
        ///    // Render optional script block
        ///    virtual void renderScript() {}
        ///protected:
        ///
        ///   ILayoutContentView(uint8_t viewId, con [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string ILayoutContentView_h {
            get {
                return ResourceManager.GetString("ILayoutContentView_h", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to $headingtext$
        ///@viewname LayoutTemplate
        ///@minify true
        ///@mimetype TextHtml
        ///@modeltype ILayoutContentView*
        ///@include &quot;ILayoutContentView.h&quot;
        ///
        ///&lt;!DOCTYPE html&gt;
        ///&lt;html lang=&quot;en&quot;&gt;
        ///
        ///&lt;head&gt;
        ///    &lt;meta charset=&quot;utf-8&quot; /&gt;
        ///    &lt;meta name=&quot;viewport&quot; content=&quot;width=device-width, initial-scale=1.0&quot; /&gt;
        ///    &lt;title&gt;
        ///        &lt;% model-&gt;renderTitle(); %&gt; | $projectname$
        ///    &lt;/title&gt;
        ///	&lt;link href=&quot;/Content/normal.css&quot; rel=&quot;stylesheet&quot;&gt;
        ///    &lt;link href=&quot;/Content/ham-menu.css&quot; rel=&quot;stylesheet&quot;&gt;
        ///&lt;/head&gt;
        ///
        ///&lt;body&gt;
        ///    &lt;na [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string LayoutTemplateView_asp {
            get {
                return ResourceManager.GetString("LayoutTemplateView_asp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to #ifndef LAYOUTTEMPLATEVIEW_H
        ///#define LAYOUTTEMPLATEVIEW_H
        ///
        ///$headingtext$
        ///
        ///#include &quot;IView.h&quot;
        ///#include &quot;ILayoutContentView.h&quot;
        ///
        ///class LayoutTemplateView : public IView
        ///{
        ///public:
        ///	LayoutTemplateView(ILayoutContentView *model, HttpRequest *request = getDefaultRequest()) : IView(ViewIdNotSpecified, NULL, request)
        ///	{
        ///		this-&gt;model = model;
        ///		this-&gt;viewMimeType = TextHtml;
        ///	}
        ///	ILayoutContentView *getModel() const { return model; }
        ///
        ///protected:
        ///	ILayoutContentView *model;
        ///
        ///	void onRender()
        ///	{
        /// [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string LayoutTemplateView_h {
            get {
                return ResourceManager.GetString("LayoutTemplateView_h", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to #ifndef LOGINMODEL_H
        ///#define LOGINMODEL_H
        ///
        ///#include &quot;IBindableModel.h&quot;
        ///
        ///enum LoginStatus
        ///{
        ///    LSUnDefined,
        ///    LSLoginSuccess,
        ///    LSLoginFailed
        ///};
        ///
        ///class LoginModel : public IBindableModel
        ///{
        ///public:
        ///    char username[9];
        ///    char password[9];
        ///    bool loginPressed = false;
        ///    uint8_t status = LSUnDefined;
        ///
        ///    LoginModel()
        ///    {
        ///        username[0] = 0;
        ///        password[0] = 0;
        ///    };
        ///
        ///protected:
        ///    // Bind http post values to field variables.
        ///    void onBind(const char *form [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string LoginModel_h {
            get {
                return ResourceManager.GetString("LoginModel_h", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to $headingtext$
        ///@viewname Login
        ///@modeltype LoginModel*
        ///@minify true
        ///@mimetype TextHtml
        ///@viewbase ILayoutContentView
        ///@include &quot;LoginModel.h&quot;
        ///
        ///@content Title
        ///
        ///    Login
        ///
        ///@content Body
        ///
        ///    &lt;p&gt;Type in credentials and press Login.&lt;/p&gt;		
        ///
        ///    &lt;form method=&quot;post&quot; action=&quot;/Login&quot; id=&quot;form1&quot;&gt;
        ///        &lt;span&gt;Username&lt;/span&gt;&lt;br /&gt;
        ///        &lt;input type=&quot;text&quot; name=&quot;user&quot; value=&quot;&lt;%: model-&gt;username %&gt;&quot; /&gt;&lt;br /&gt;
        ///        &lt;span&gt;Password&lt;/span&gt;&lt;br /&gt;
        ///        &lt;input type=&quot;password&quot; name=&quot;pass&quot; value=&quot;&quot; /&gt;&lt;br [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string LoginView_asp {
            get {
                return ResourceManager.GetString("LoginView_asp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to #ifndef LOGINVIEW_H
        ///#define LOGINVIEW_H
        ///
        ///$headingtext$
        ///
        ///#include &quot;ILayoutContentView.h&quot;
        ///#include &quot;LoginModel.h&quot;
        ///
        ///class LoginView : public ILayoutContentView
        ///{
        ///public:
        ///	LoginView(LoginModel *model, HttpRequest *request = getDefaultRequest()) : ILayoutContentView(ViewIdNotSpecified, NULL, request)
        ///	{
        ///		this-&gt;model = model;
        ///		this-&gt;viewMimeType = TextHtml;
        ///	}
        ///	LoginModel *getModel() const { return model; }
        ///protected:
        ///	LoginModel *model;
        ///
        ///	void renderTitle()
        ///	{
        ///		writeStr(PSTR(&quot;Login&quot;));
        /// [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string LoginView_h {
            get {
                return ResourceManager.GetString("LoginView_h", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to #ifndef $viewname_upper$MODEL_H
        ///#define $viewname_upper$MODEL_H
        ///
        ///// ## HEADER
        ///
        ///class $viewname$Model
        ///{
        ///public:
        ///
        ///
        ///private:
        ///
        ///};
        ///
        ///#endif
        ///.
        /// </summary>
        internal static string Model_h {
            get {
                return ResourceManager.GetString("Model_h", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to // TODO Project header text.
        ///
        ///// SD card reader select pin
        ///#define SD_SELECT_PIN 4
        ///
        ///#if defined(ESP8266)                // Check if ESP8266 board
        ///    #include &lt;ESP8266WiFi.h&gt;
        ///
        ///    char ssid[] = &quot;guest&quot;;          // your network SSID (name)
        ///    char pass[] = &quot;guest123&quot;;       // your network password
        ///
        ///    WiFiServer server(80);          // Using DHCP for WiFi. Check your router to find IP.
        ///    WiFiClient client;
        ///
        ///#else                               // Otherwise assume AVR with Ethernet shield ( [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string Project_ino {
            get {
                return ResourceManager.GetString("Project_ino", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to $headingtext$
        ///@viewname $viewname$
        ///@minify true
        ///@mimetype TextHtml
        ///@viewbase ILayoutContentView
        ///@modeltype $viewname$Model*
        ///@include &quot;$viewname$Model.h&quot;
        ///
        ///@content Title
        ///
        ///    $viewname$ page
        ///
        ///@content Body
        ///
        ///    &lt;p&gt;This is the $viewname$ page&lt;/p&gt;
        ///.
        /// </summary>
        internal static string View_asp {
            get {
                return ResourceManager.GetString("View_asp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to #ifndef $viewname_upper$VIEW_H
        ///#define $viewname_upper$VIEW_H
        ///
        ///$headingtext$
        ///
        ///#include &quot;ILayoutContentView.h&quot;
        ///#include &quot;$viewname$Model.h&quot;
        ///
        ///class $viewname$View : public ILayoutContentView
        ///{
        ///public:
        ///	$viewname$View($viewname$Model *model, HttpRequest *request = getDefaultRequest())
        ///		: ILayoutContentView(ViewIdNotSpecified, NULL, request)
        ///	{
        ///		this-&gt;model = model;
        ///		this-&gt;viewMimeType = TextHtml;
        ///	}
        ///	$viewname$Model *getModel() const { return model; }
        ///
        ///protected:
        ///	$viewname$Model *model; [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string View_h {
            get {
                return ResourceManager.GetString("View_h", resourceCulture);
            }
        }
    }
}
