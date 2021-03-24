import Vue from 'vue';
import Vuex from 'vuex'
/// @ts-ignore
import Avatar from 'vue-avatar';
import axios, { AxiosResponse } from 'axios';
import VueFinalModal from 'vue-final-modal'
import 'normalize.css';

import './styles/index.scss';

Vue.use(VueFinalModal())
Vue.use(Vuex)

const store = new Vuex.Store({
    state: {
      contacts: []
    },
    mutations: {
      increment (state) {
        
      }
    }
  })

interface GetContactsResponse
{
    response: PaginatedList<Contact>;
}

interface PaginatedList<Type>
{
    items: Type[],
    pageIndex: number,
    pageSize: number,
    totalCount: number,
    totalPages: number,
    hasPreviousPage: boolean,
    hasNextPage: boolean
}

interface Contact 
{
    id: string,
    name: string,
    phoneNumber: number,
    email?: string
}

new Vue({
    el: "#main",
    store,
    data() {
        return {
            loading: true,
            contacts: Array<Contact>(),
            modalPlugin: null,
            showModal: false,
        }
    },
    components: {
        Avatar,
    },
    async mounted() {
        await axios.get('http://localhost:56544/api/Contacts')
            .then(response => {
                const data: GetContactsResponse = response.data;
                this.contacts = data.response.items;
            })
            .finally(() =>
                this.loading = false);
        this.loading = false
    },
    methods: {
        async removeContact(contactId: number)
        {
            console.log(contactId);
            // await axios.delete('/api/contacts', {id: value.Id});
        }
    },
    filters: {
        phoneNumber: function (inputPhoneNumber: number) {
            let i = 0;
            let phoneNumber: string = inputPhoneNumber.toString();
            phoneNumber = '+# (###) ### ## ##'.replace(/#/g, _ => phoneNumber[i++]);
            return phoneNumber;
        }
    },
    destroyed() {
        // if (this.modalPlugin && this.modalPlugin.destroy) {
        //   this.modalPlugin.destroy()
        // }
    }
});