Portal
======

Portal MVC is a Razor view helper that allows you to send blocks of HTML or JavaScript from a view or partial view to the layout view. 

This is helpful when you want to include various bits of JavaScript from a partial views right before closing of the HTML tag to optimize page rendering. You can also register requests for JavaScript / CSS files that should be included in the layout.

There are number of extension methods offered by Portal that are all called in this manner:

    @Html.Portal*In*XXXX
    @Html.Portal*Out*XXXX 

<table>
    <thead>
        <tr>
            <th>In</th>
            <th>Out</th>
            <th>Comments</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td></td>
            <td></td>
        </t>
    </tbody>
</table>