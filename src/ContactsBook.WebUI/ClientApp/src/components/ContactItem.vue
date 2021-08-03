<template>
  <a class="contact" @click.prevent="onClick">
    <div class="contact__photo">
      <Avatar :username="contact.name" color="white" />
    </div>
    <div class="contact__content">
      <div class="contact__top">
        <div class="contact__left_side">
          <span class="contact__name">
            {{ contact.name }}
          </span>
        </div>
        <div class="contact__right_side">
          <span class="contact__phone">
            <PhoneIcon />
            {{ contact.phoneNumber | phoneNumber }}
          </span>
          <ContactItemRemoveButton
            class="infinity_list__item_remove"
            @click="onRemoveButtonClick(contact.id)"
          />
        </div>
      </div>
      <div class="contact__bottom">
        <span v-if="contact.email" class="contact__email">
          <EmailIcon />
          {{ contact.email }}
        </span>
      </div>
    </div>
  </a>
</template>

<script lang="ts">
import Vue, { PropType } from "vue";
/// @ts-ignore
import Avatar from "vue-avatar";

import ContactItemRemoveButton from "./ContactItemRemoveButton.vue";
import phoneNumberFilter from "../filters/PhoneNumberFilter";
import EmailIcon from "@/assets/email_icon.svg";
import PhoneIcon from "@/assets/phone_icon.svg";

export default Vue.extend({
  components: {
    Avatar,
    ContactItemRemoveButton,
    EmailIcon,
    PhoneIcon,
  },
  props: {
    contact: Object as PropType<Contact>,
  },
  methods: {
    onRemoveButtonClick(contactId: string) {
      this.$emit("remove", contactId);
    },
    onClick() {
      this.$emit("click", this.contact.id);
    },
  },
  filters: {
    phoneNumber: phoneNumberFilter,
  },
});
</script>