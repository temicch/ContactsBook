<template>
  <div>
    <ContactsAddButton @click="onAddButtonClick" />
    <ContactItemModal
      modalTitle="Create contact"
      :visible="showModal"
      :contact="contact"
      :disabled="isModalDisabled"
      :errors="errors"
      @submit="onSubmit"
      @close="onModalClose"
    />
  </div>
</template>

<script lang="ts">
import ContactsModule from "@/store/modules/contacts";
import Vue from "vue";

import ContactsAddButton from "./ContactsAddButton.vue";
import ContactItemModal from "./ContactItemModal.vue";

export default Vue.extend({
  components: {
    ContactsAddButton,
    ContactItemModal
  },
  data: () => ({
    showModal: false,
    isModalDisabled: false,
    errors: {},
    contact: {
      id: "",
      name: "",
      email: "",
      phoneNumber: 7
    } as Contact
  }),
  methods: {
    onAddButtonClick() {
      this.clearContact();
      this.showModal = true;
    },
    onModalClose() {
      this.showModal = false;
      this.isModalDisabled = false;
      this.errors = {};
    },
    clearContact() {
      this.contact = {
        id: "",
        name: "",
        email: "",
        phoneNumber: 7
      };
    },
    async onSubmit(contact: Contact) {
      this.isModalDisabled = true;
      await ContactsModule.CreateContact(contact)
        .then(async response => {
          this.$notify({ title: "Contact created", type: "success" });
          this.$emit("contact-created", contact);
          this.onModalClose();
        })
        .catch(error => {
          this.errors = error.response.data.errors;
          this.$notify({
            title:
              "There was an error on creating contact. Please try again later",
            type: "error"
          });
          if (this.errors == undefined) throw error;
        })
        .finally(() => {
          this.isModalDisabled = false;
        });
    }
  }
});
</script>
