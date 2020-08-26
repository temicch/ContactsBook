<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication2._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col s12">
            <div id="main" class="card">
                <div class="card-action">
                    <h5 id="task-title">Contacts</h5>
                    <div v-if="loading" class="preloader-wrapper big active center ">
                        <div class="spinner-layer spinner-blue-only">
                          <div class="circle-clipper left">
                            <div class="circle"></div>
                          </div><div class="gap-patch">
                            <div class="circle"></div>
                          </div><div class="circle-clipper right">
                            <div class="circle"></div>
                          </div>
                        </div>
                    </div>
                    <div v-else-if="contacts.length">
                        <div v-for="(contact, idx) of contacts" :key="contact.id">
                            <div class="card-content ">
                                <span class="card-title">{{contact.Name}}</span>
                                <p>{{ contact.Email }}</p>
                            </div>
                            <div class="card-action">
                                <a class="btn-floating waves-effect waves-light red"><i class="material-icons">edit</i></a>
                                <a class="btn-floating waves-effect waves-light red"><i class="material-icons">delete</i></a>
                            </div>
                        </div>
                    </div>
                    <p class="center" v-else>
                      There is no contacts
                    </p>
                </div>
            </div>
        </div>
    </div>
    <script src="Scripts/contacts.js"></script>
</asp:Content>
