import Vue from "vue";
import Notifications from "vue-notification";
/// @ts-ignore
import Avatar from "vue-avatar";
import VueFinalModal from "vue-final-modal";
import "normalize.css";

import "./styles/index.scss";
import store from "./store";
import ContactsModule from "./store/modules/contacts";
import InfinityList from "./components/InfinityList.vue";

import ContactsCreator from "./components/ContactsCreator.vue";
import ContactsEditor from "./components/ContactsEditor.vue";
import ContactsNotFound from "./components/ContactsNotFound.vue";
import ContactsSearch from "./components/ContactsSearch.vue";

import ContactItem from "./components/ContactItem.vue";
import ContactsSearchModule from "./store/modules/contactsSearch";

Vue.use(VueFinalModal());
Vue.use(Notifications);

new Vue({
  el: "#main",
  store,
  components: {
    Avatar,
    InfinityList,

    ContactsNotFound,
    ContactsSearch,
    ContactsCreator,
    ContactsEditor,

    ContactItem,
  },
  async mounted() {
    await ContactsModule.LoadContacts();
  },
  methods: {
    async removeContact(contactId: string) {
      await ContactsModule.RemoveContact(contactId)
        .then(async (response) => {
          await ContactsSearchModule.RemoveContact(contactId);
        })
        .catch((error) => {
          this.$notify({
            title:
              "There was an error on removing contact. Please try again later",
            type: "error",
          });
          throw error;
        });
    },

    async onEndReached() {
      let isEndReached = false;
      await ContactsModule.NextPage()
        .then((result) => {
          if (result) isEndReached = false;
          else isEndReached = true;
        })
        .catch((error) => {
          isEndReached = true;
          throw error;
        });
      return isEndReached;
    },

    async onContactEdited(contact: Contact) {
      ContactsSearchModule.UpdateContact(contact);
    },

    async onContactCreated(contact: Contact) {
      ContactsSearchModule.CreateContact(contact);
    },
  },
  computed: {
    loading(): boolean {
      return ContactsModule.loading;
    },
    contacts(): Contact[] {
      return ContactsModule.items;
    },
  },
});
