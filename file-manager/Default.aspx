<%@ Page Title="Files" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="file_manager._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="panel panel-default">
            <div class="panel-heading" runat="server">
                Displaying files in <strong><%= String.IsNullOrEmpty(CurrentPath) ? RelativePath(RootPath) : RelativePath(CurrentPath) %></strong>
            </div>
            <div class="panel-body">
                <div class="btn-group  btn-group-sm" role="group">
                    <asp:TextBox ID="CreateFolderName" runat="server"></asp:TextBox>
                    <asp:Button ID="CreateFolderBtn" runat="server" Text="Create Folder" OnClick="CreateFolderBtn_Click" />
                    <asp:Button ID="DeleteFolderBtn" runat="server" Text="Delete Folder" OnClick="DeleteFolderBtn_Click" />
                    <asp:Button ID="DeleteFiles" runat="server" Text="Delete Files" OnClick="DeleteFiles_Click" />
                </div>
            </div>
            <div class="panel-body">
                <div class="col-md-2" id="_files" runat="server">
                    <asp:TreeView ID="FilesTree"
                        CollapseImageToolTip="Close Folder" 
                        ExpandImageToolTip="Open Folder"
                        NodeWrap="true"
                        OnSelectedNodeChanged="FilesTree_SelectedNodeChanged"
                        runat="server"></asp:TreeView>
                </div>
                <div class="col-md-10" id="contents">
                    <ul class="list-group" runat="server">
                        
                        <asp:Repeater ID="FilesRepeater" runat="server">
                            <HeaderTemplate>
                                <ul class="list-group">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <li class="list-group-item">
                                    <asp:CheckBox runat="server" />
                                    <span class="glyphicon glyphicon-file" aria-hidden="true"></span>
                                    <strong title="<%# Eval("FilePath") %>"><%# Eval("FileName") %></strong> (<%# Eval("FileSize") %>)
                                </li>
                            </ItemTemplate>
                            <FooterTemplate>
                                </ul>
                            </FooterTemplate>
                        </asp:Repeater>

                    </ul>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
