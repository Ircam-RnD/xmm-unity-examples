# xmm-unity-mouse-shapes-example

Simple Unity project demonstrating the use of the
[xmm-unity plugin](https://github.com/Ircam-RnD/xmm-unity) to recognize shapes
drawn with the mouse.

#### getting started

Clone the [xmm-unity plugin](https://github.com/Ircam-RnD/xmm-unity) repository
and follow the instructions in the README to build the plugin for Mac OSX.  
Drop the freshly compiled `XmmEngine.bundle` file into `Assets/Plugins/macos`
and copy the `XmmEngine.cs` wrapper file into `Assets/Scripts`
(create the folders if they don't already exist).  
Open the main folder from Unity and hit Run.  

#### how to use this example

`R` is a special key that enables / disables recording mouse gestures.
Any other character will be used to set the label of the next recorded gestures.  
When recording is enabled, any gesture from mouse pressed to mouse released will
be recorded and added to the training set with the current label.  
When recording is disabled, any gesture from mouse pressed to mouse released will
be processed and the likeliest label will be displayed on the main screen.
