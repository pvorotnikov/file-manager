<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="file_manager._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading">
                Displaying files in <strong><%= Dir.FullName %></strong>
            </div>
            <div class="panel-body">
                <div class="col-md-3" id="tree">
                    <asp:TreeView ID="DirectoryView" runat="server"></asp:TreeView>
                </div>
                <div class="col-md-9" id="contents">
                    <ul class="list-group">
                        <% foreach(var file in FileList) { %>
                        <li class="list-group-item">
                            <span class="glyphicon glyphicon-file" aria-hidden="true"></span>
                            <%= file.Name %>
                        </li>
                        <% } %>
                    </ul>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
