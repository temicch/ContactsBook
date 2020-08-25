<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication2._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col s12">
            <div id="main" class="card">
                <div class="card-content">
                    <span class="card-title">Task List</span>
                    <div class="row">
                        <form id="task-form">
                            <div class="input-field col s12">
                                <input type="text" name="task" id="task">
                                <label for="task">New Task</label>
                            </div>
                            <input type="submit" value="Add Task" class="btn">
                        </form>
                    </div>
                </div>
                <div class="card-action">
                    <h5 id="task-title">Tasks</h5>
                    <div class="input-field col s12">
                        <input type="text" name="filter" id="filter">
                        <label for="filter">Filter</label>
                    </div>
                    <ul class="collection"></ul>
                    <a href="#" class="btn black">Clear</a>
                    <a class="btn-floating btn-large waves-effect waves-light red"><i class="material-icons">add</i></a>
                </div>
            </div>
        </div>
    </div>
    <script src="Scripts/contacts.js"></script>
</asp:Content>
