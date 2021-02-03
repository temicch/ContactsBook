<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication2._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server" >
    <%-- <div id="modal1" class="modal modal-fixed-footer">
      <div class="modal-title">
        <h2>Add a contact</h2>
      </div>
      <div class="modal-content">
        <p>A bunch of text</p>
      </div>
      <div class="modal-footer">
        <a href="#!" class="modal-close waves-effect btn-flat">Save</a>
      </div>
    </div> --%>
    <div id="main" class="wrapper">
        <div class="wrapper__content">
            <div class="wrapper__top">
                <div class="search">
                    <input type="text" placeholder="Search Contact" class="search__input">
                </div>
            </div>
            <div class="wrapper__middle">
                <div v-if="loading" class="wrapper__loader">
                    <div class="loader"><div></div><div></div><div></div><div></div></div>
                </div>
                <ul class="contacts" v-else-if="this.contacts && this.contacts.length">
                    <li class="contacts__item" v-for="(contact, idx) of contacts" :key="contact.id">
                        <a href="ya.ru" class="contact" v-on:click.stop.prevent="">
                            <div class="contact__photo">
                                <avatar 
                                :username="contact.Name" 
                                color="white" ></avatar>
                            </div>
                            <div class="contact__content">
                                <div class="contact__top">
                                    <div class="contact__left_side">
                                        <span class="contact__name">
                                            {{ contact.Name }}
                                        </span>
                                    </div>
                                    <div class="contact__right_side">
                                        <span class="contact__phone">
                                            {{ contact.PhoneNumber | phoneNumber }}
                                        </span>
                                        <button type="button" v-on:click="console.log('click')" class="contact__remove"></button>
                                    </div>
                                </div>
                                <div class="contact__bottom">
                                    <span class="contact__email">
                                        {{ contact.Email }}
                                    </span>
                                </div>
                            </div>
                        </a>
                    </li>
                </ul>
                <div v-else class="contacts--none">
                    There is no contacts.
                </div>
            </div>
            
<vue-final-modal
      v-model="showModal"
      classes="modal-container"
      content-class="modal-content"
    >
      <%-- <button class="modal__close" @click="showModal = false">
        <mdi-close></mdi-close>
      </button> --%>
      <h3 class="modal__title">Create a contact</h3>
      <div class="modal__content">
        <div class="contact_construct__header">
            <div class="contact_construct__avatar">
                <avatar :size="72" color="white" username='Артём Максимов'></avatar>
            </div>
            <div class="contact_construct__info">
                <h3>Артём Максимов</h3>
                <h4>Укажите основную информацию</h4>
            </div>
        </div>
        <form class="contact_construct__form">
            <div class="contact_construct__form_group">
                <label>
                    <h4>Имя</h4>
                    <input placeholder="Введите имя"/>
                </label>
                <label>
                    <h4>Email</h4>
                    <input placeholder="Введите email"/>
                </label>
            </div>
            <div class="contact_construct__form_group">
                <label>
                    <h4>Номер телефона</h4>
                    <input placeholder="Введите номер телефона"/>
                </label>
            </div>
            <div class="contact_construct__form_footer">
                <button class="contact_construct__button" @click="showModal = false">Сохранить</button>
            </div>
        </form>
      </div>
      <button class="modal__close_button" @click="showModal = true"></button>
    </vue-final-modal>
            <div class="wrapper__bottom">
                <div class="person_add" @click="showModal = !showModal">
                    <a href="#modal1">
                        +
                    </a>
                </div>
            </div>
        </div>
    </div>
    <script type="module" src="ClientApp/dist/main.js"></script>
</asp:Content>