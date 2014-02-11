This is a simple demonstration of why it is important to clear out static references to Unity objects when they are getting destroyed. If this is not done, resources referenced by such "static" objects may "leak" across scene changes.

In this small project, there are two scenes, both list out all the Sprite instances currently loaded.

* For the most reliable results, this should be run in a build. In the editor, sprites may be loaded if they have been selected, for instance.

* When first starting the application, there will be 0 loaded sprites. Note that in the lower-left corner, "Manager instanced: False" is printed.

* After going to the next scene via "Change scene", two sprites will be loaded, and it now says "Manager instanced: True" in the lower left. The two sprites are referenced by this manager, which has a static Instance property pointing to it. Note that there is a a button in the top-left corner saying "Clear instance", which is unchecked. If this is checked, the Instance property will be explicitly set to "null" in the Manager's OnDestroy, which is called on level change.

* Leaving the button unchecked, change scene again, going back to the first scene (red background). Now one would expect that there would be no sprites loaded, as the Manager is no longer present - but in fact there are still two loaded, even though "Manager instanced: False" is printed, indicating that it doesn't exist! But that is not completely true - even though the GameObject has been destroyed, the Mono-side representation of our Manager is still present, but Unity is overriding the equals-method for UnityEngine.Object, and there is a good discussion of it here: http://answers.unity3d.com/questions/524998/testing-for-null-bug-or-feature.html

* However, explicitly setting our static reference to "null" will fix the leaking resources issue, as it seems like Unity will hold on to resources if the Mono-side of the object is still alive. Now change the scene so it goes to the one with the blue background, and check the "Clear instance" toggle.

* Change scene to the one with the red background, and now it should say that zero sprites are loaded.

If you have complex managers that are referencing a significant number of memory-heavy assets, forgetting to plug leaks like this may have a very bad impact on the memory footprint on limited devices.

Tested on Unity 4.3.4
