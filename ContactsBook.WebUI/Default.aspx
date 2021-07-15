<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master"
AutoEventWireup="true"
CodeBehind="Default.aspx.cs"
Inherits="ContactsBook.WebUI._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="main" class="wrapper">
        <notifications position="bottom right"></notifications>
        <div class="wrapper__content">
            <contacts-editor
                v-slot="scope"
                @contact-edited="onContactEdited">
                <contacts-search
                    class="wrapper__top"
                    @contact-click="scope.click"
                    @contact-remove="removeContact">
                </contacts-search>
                <div class="wrapper__middle">
                    <infinity-list
                        :async-func="onEndReached"
                        :loading="loading"
                        :items="contacts">
                        <template #item="{ item }">
                            <contact-item
                                :contact="item"
                                @remove="removeContact"
                                @click="scope.click(item)"/>
                        </template>
                        <template #nothing>
                            <contacts-not-found/>
                        </template>
                    </infinity-list>
                </div>
            </contacts-editor>
            <contacts-creator
                class="wrapper__bottom"
                @contact-created="onContactCreated"/>
        </div>
    </div>
    <script type="module" src="ClientApp/dist/main.js"></script>
</asp:Content>