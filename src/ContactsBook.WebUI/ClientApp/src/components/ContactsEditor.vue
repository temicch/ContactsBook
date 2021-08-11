<template>
  <div>
    <slot :click="onItemClick"></slot>
    <ContactItemModal
      modalTitle="Edit contact"
      :visible="showModal"
      :disabled="isModalDisabled"
      :errors="errors"
      @submit="onSubmit"
      @close="onModalClose"
      :contact="contact"
    />
  </div>
</template>

<script lang="ts">
import ContactsModule from "@/store/modules/contacts";
import Vue from "vue";

import ContactItemModal from "./ContactItemModal.vue";

export default Vue.extend({
  components: {
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
      phoneNumber: 0
    } as Contact
  }),
  methods: {
    onItemClick({ ...contact }: Contact) {
      this.contact = contact;
      this.showModal = true;
    },
    onModalClose() {
      this.showModal = false;
      this.isModalDisabled = false;
      this.errors = {};
    },
    async onSubmit(contact: Contact) {
      this.isModalDisabled = true;
      await ContactsModule.UpdateContact(contact)
        .then(async response => {
          this.$notify({ title: "Contact edited", type: "success" });
          this.$emit("contact-edited", contact);
          this.onModalClose();
        })
        .catch(error => {
          this.errors = error.response.data.errors;
          this.$notify({
            title:
              "There was an error on editing contact. Please try again later",
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
