import Vue from 'vue';
/// @ts-ignore
import Avatar from 'vue-avatar';
import VueFinalModal from 'vue-final-modal';
import 'normalize.css';

import '@/styles/index.scss';
import { Contact } from 'api/types';
import store from '@/store';
import { ContactsModule } from '@/store/modules/contacts';

Vue.use(VueFinalModal());

new Vue({
    el: "#main",
    store,
    data() {
        return {
            showModal: false,
        }
    },
    components: {
        Avatar,
    },
    async mounted() {
        ContactsModule.LoadContacts();
    },
    methods: {
        async removeContact(contactId: string) {
            // await ContactsModule.RemoveContact(contactId);
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
    computed: {
        loading(): boolean {
            return ContactsModule.isLoading;
        },
        contacts(): Contact[] {
            return ContactsModule.contacts;
        }
    }
});

