Portal
======

Portal MVC is a **ASP.NET MVC Razor** view helper that allows you to send blocks of HTML or JavaScript from a view or partial view to the layout view. 

This is helpful when you want to include various bits of JavaScript, CSS, or HTML from views or partial views into parent views or the layout view.

With Portal, from a view or partial view, you add something into the portal using **@Html.PortalInXXX**, where XXX is one of the available methods. You then call the matching method **@Html.PortalOutXXX**.

The table below lists the methods available and a description of what they do.

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
                Send any text (HTML, JavaScript, CSS, etc) through the default portal.
                You can also send a Razor template, for example:
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
                Send any text (HTML, JavaScript, CSS, etc) through a custom portal identified by a key. 
                You can also send a Razor template, for example:
                <br />
                @Html.PortalIn(&quot;somekey&quot;, @&lt;text&gt; $(function() { alert(&#39;Hi&#39;); }); &lt;/text&gt;)
                <br />
                The out portal must use the same key.
                <br />
                @Html.PortalOut("somekey")
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
                and then outputs a link tag. The output in our example would be: 
                <br />
                &lt;link href="/content/some.css" rel="stylesheet" type="text/css" /&gt;
                <br />
                Duplicate paths are removed. In the layout file you should call PortalInCss for all the CSS files that may be added. 
                This way if the same CSS file is registered from a view then that CSS file would only be linked once in the layout.
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
                and then outputs a script tag. The output in our example would be: 
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
            <td>Registers a block of CSS in a view or partial view, for example:
                <br />
                @Html.PortalInStyle(@&lt;style type=&quot;text/css&quot;&gt; #some-id { font-weight: bold; }&lt;/style&gt;)
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
                The output is the same as the input. If PortalInScriptUnique is used, then duplicate blocks are not added.
             </td>
        </tr>
    </tbody>
</table>