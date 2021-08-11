<template>
  <VueFinalModal
    v-model="visible"
    classes="modal-container"
    content-class="modal-content"
    :clickToClose="false"
    @click-outside="onClose"
    @before-open="onModalOpening"
    @closed="onClosed"
  >
    <h3 class="modal__title">{{ modalTitle }}</h3>
    <div class="modal__content">
      <div class="contact_construct__header">
        <div class="contact_construct__avatar">
          <Avatar :size="72" color="white" :username="contact.name" />
        </div>
        <div class="contact_construct__info">
          <h3>{{ contact.name }}</h3>
          <h4>Provide your basic information</h4>
        </div>
      </div>
      <form class="contact_construct__form" @submit.prevent="onSubmit">
        <div class="contact_construct__form_group">
          <label>
            <h4>Name</h4>
            <input
              placeholder="Enter your name"
              v-model.trim.lazy="$v.contact.name.$model"
              :disabled="disabled"
            />
            <span class="contact_construct__form_group__error">
              {{ errorName }}</span
            >
          </label>
          <label>
            <h4>Email</h4>
            <input
              placeholder="Enter your Email"
              v-model.trim.lazy="$v.contact.email.$model"
              :disabled="disabled"
            />
            <span class="contact_construct__form_group__error">
              {{ errorEmail }}</span
            >
          </label>
        </div>
        <div class="contact_construct__form_group">
          <label>
            <h4>Phone number</h4>
            <input
              :value="phoneInput"
              type="text"
              v-imask="phoneMask"
              placeholder="Enter your phone number"
              @accept="onAccept"
              :disabled="disabled"
            />
            <span class="contact_construct__form_group__error">
              {{ errorPhone }}
            </span>
          </label>
        </div>
        <div class="contact_construct__form_footer">
          <button :disabled="disabled" class="contact_construct__button">
            Save
          </button>
        </div>
      </form>
    </div>
    <button class="modal__close_button" @click="onClose"><CloseIcon /></button>
  </VueFinalModal>
</template>
<script lang="ts">
import Vue from "vue";
/// @ts-ignore
import Avatar from "vue-avatar";
/// @ts-ignore
import { IMaskDirective } from "vue-imask";
import { validationMixin } from "vuelidate";
import { required, minLength, maxLength } from "vuelidate/lib/validators";

import { isCorrectEmail, isCorrectPhoneNumber } from "@/utils";
import CloseIcon from "@/assets/close_icon.svg";

export default Vue.extend({
  components: {
    Avatar,
    CloseIcon
  },
  data() {
    return {
      phoneMask: {
        mask: "+0 (000) 000 00 00",
        lazy: false
      },
      phoneNumberMasked: ""
    };
  },
  props: {
    contact: {
      type: Object as () => Contact,
      required: true
    },
    modalTitle: {
      type: String,
      required: true
    },
    visible: {
      type: Boolean,
      default: false
    },
    disabled: {
      type: Boolean,
      default: false
    },
    errors: {
      type: Object,
      default: {}
    }
  },
  mixins: [validationMixin],
  validations: {
    contact: {
      name: {
        required,
        minLength: minLength(3),
        maxLength: maxLength(28)
      },
      phoneNumber: {
        required,
        isCorrectPhoneNumber
      },
      email: {
        isCorrectEmail
      }
    }
  },
  methods: {
    onSubmit() {
      this.$v.$touch();

      if (!this.$v.$invalid) {
        this.$emit("submit", { ...this.contact });
      }
    },
    onClose() {
      this.$emit("close");
    },
    onClosed() {
      this.$emit("closed");
    },
    onAccept(e: CustomEvent) {
      const { unmaskedValue } = e.detail;

      if (this.$v.contact.phoneNumber != undefined)
        this.$v.contact.phoneNumber.$model = Number(unmaskedValue);
    },
    onModalOpening() {
      this.$v.$reset();
    }
  },
  computed: {
    phoneInput(): string {
      this.phoneNumberMasked = this.contact.phoneNumber.toString();
      return this.phoneNumberMasked;
    },
    errorName() {
      let error: String = "";
      if (this.errors.name?.length > 0) error = this.errors.name[0];
      else if (this.$v.contact.name?.$dirty) {
        if (!this.$v.contact.name.required) error = "Name is required";
        if (!this.$v.contact.name.minLength)
          error = "Name must be more than 3 symbols";
        if (!this.$v.contact.name.maxLength)
          error = "Name must be less than 28 symbols";
      }
      return error;
    },
    errorEmail() {
      let error: String = "";
      if (this.errors.email?.length > 0) error = this.errors.email[0];
      else if (this.$v.contact.email?.$dirty) {
        if (!this.$v.contact.email.isCorrectEmail)
          error = "Please enter a valid email";
      }
      return error;
    },
    errorPhone() {
      let error: String = "";
      if (this.errors.PhoneNumber?.length > 0)
        error = this.errors.PhoneNumber[0];
      else if (this.$v.contact.phoneNumber?.$dirty) {
        if (!this.$v.contact.phoneNumber.required)
          error = "Phone number is required";
        if (!this.$v.contact.phoneNumber.isCorrectPhoneNumber)
          error = "Please enter a valid phone number";
      }
      return error;
    }
  },
  directives: {
    imask: IMaskDirective
  }
});
</script>
