Portal
======

Portal MVC is a **ASP.NET MVC Razor** view helper that allows you to send blocks of HTML, JavaScript, or CSS from a view or partial view to the layout view. This overcomes the current Razor view limitation of not being able to define sections in partial views. 

For example, you could register all your JavaScript blocks using Portal and output everything in your layout before the closing of the body tag. Also, from your views or partial views you could list your CSS or JavaScript dependencies, calls to .css or .js files, without having to worry about ending up with duplicate file registrations.

With Portal, from a view or partial view, you add something into the portal using **@Html.PortalInXXX**, where XXX is one of the available methods. You then call the matching method **@Html.PortalOutXXX** in your layout.

The table below lists the methods available and a description of what they do. The code in the In column should go into the view or partial view, while the code in the Out column should go in the layout view.

<table id="portal-doc">
    <thead>
        <tr>
            <th>In</th>
            <th>Out</th>
            <th>Description</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>
                @Html.PortalIn(text)
                <br /> or <br />
                @Html.PortalIn(template)
            </td>
            <td>@Html.PortalOut()</td>
            <td>
                Send any text (HTML, JavaScript, CSS, etc) through the default portal:
                <br />
                @Html.PortalIn("Hello World")
                <br />
                You can also send a Razor template:
                <br />
                @Html.PortalIn(@&lt;text&gt; $(function() { alert(&#39;Hi&#39;); }); &lt;/text&gt;)                
                <br />
                And then somewhere in the layout view:
                <br />
                @Html.PortalOut()
            </td>
        </tr>
        <tr>
            <td>
                @Html.PortalIn(key, text)
                <br /> or <br />
                @Html.PortalIn(key, template)
            </td>
            <td>@Html.PortalOut(key)</td>
            <td>
                Same as PortalIn(text / template) except that you can define a custom portal by specifying a key. 
                The same key (string) must be used for the In and Out methods.
            </td>
        </tr>        
        <tr>
            <td>
                @Html.PortalInUnique(key, text)
                <br /> or <br />
                @Html.PortalInUnique(key, template)
            </td>
            <td>
                @Html.PortalOut(key)
            </td>
            <td>Same as PortalIn(key, text / template) except that if you try to add the same content twice only the first addition is actually added.</td>
        </tr>
        <tr>
            <td>
                @Html.PortalInCss(path)
                <br /> or <br />
                @Html.PortalInCssAbsolute(path)
            </td>
            <td>@Html.PortalOutCss()</td>
            <td>Registers a path to a CSS file, for example:
                <br />
                @Html.PortalInCss("~/content/some.css")
                <br />
                The output in our example would be: 
                <br />
                &lt;link href="/content/some.css" rel="stylesheet" type="text/css" /&gt;
                <br />
                Duplicate paths are removed. In the layout file you should call PortalInCss for all the CSS files that may be added. 
                This way if the same CSS file is registered from a different view then that CSS file would only be linked once in the layout.
                <br />
                If you would rather Portal not modify the given path or you would like to specify the full path, you can use:
                <br />
                @Html.PortalInCssAbsolute("http://example.com/css/my.css")
                <br /> or <br />
                @Html.PortalInCssAbsolute("//example.com/css/my.css")
             </td>
        </tr>
        <tr>
            <td>
                @Html.PortalInJs(path)
                <br /> or <br />
                @Html.PortalInJsAbsolute(path)
            </td>
            <td>@Html.PortalOutJs()</td>
            <td>Registers a path to a JS file, for example:
                <br />
                @Html.PortalInJs("~/scripts/my.js")
                <br />
                The output in our example would be: 
                <br />
                &lt;script src="/scripts/my.js" type="text/javascript"&gt;&lt;/script&gt;
                <br />
                Duplicate paths are removed. If you don't want Portal to change the path you can use:
                <br />
                @Html.PortalInJsAbsolute("http://example.com/scripts/my.js")
             </td>
        </tr>
        <tr>
            <td>
                @Html.PortalInStyle(text)
                <br /> or <br />
                @Html.PortalInStyle(template)                
            </td>
            <td>@Html.PortalOutStyle()</td>
            <td>Registers a block of CSS in a view or partial view:
                <br />
                @Html.PortalInStyle(@&lt;style type=&quot;text/css&quot;&gt; #some-id { font-weight: bold; }&lt;/style&gt;)
                <br />
                The output is rendered without processing.
             </td>
        </tr>
        <tr>
            <td>
                @Html.PortalInScript(text)
                <br /> or <br />
                @Html.PortalInScript(template)
                <br /> or <br />
                @Html.PortalInScriptUnique(text)
                <br /> or <br />
                @Html.PortalInScriptUnique(template)
            </td>
            <td>@Html.PortalOutScript()</td>
            <td>Registers a JavaScript block in a view or partial view, for example:
                <br />
                @Html.PortalInScript(@&lt;script type=&quot;text/javascript&quot;&gt; $(function () { alert(&#39;Hi!&#39;); }) &lt;/script&gt;)
                <br />
                The output is rendered without processing. If PortalInScriptUnique is used, then duplicate blocks are not added.
             </td>
        </tr>
    </tbody>
</table>

Limitations
-----------

When Razor files are rendered the order is from sub partial views to views and then to the layout. For this reason you cannot have an In portal in the layout and an Out portal in a partial view; nothing gets called in the layout view until all the sub-views are rendered.

Within a layout, view, or partial view you can have an In and Out portal as long as the In portal is placed before the Out portal.

How to use
----------

The only file you need is Portal.cs. If you're using [NuGet to install Portal](http://nuget.org/packages/Portal), the installer copies Portal.cs to /Helpers/Portal.cs in your main / current project, and modifies your web.config under /Views.

For manual installations, you can just copy Portal.cs to whatever folder you prefer and register the namespace in /Views/web.config like this:

    <configuration>
    	<system.web.webPages.razor>
    		<pages>
    			<namespaces>
    				<add namespace="Portal"/>
    			</namespaces>
    		</pages>
    	</system.web.webPages.razor>
    </configuration>