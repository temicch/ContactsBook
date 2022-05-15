<template>
  <div>
    <ContactsSearchInput @input-trigger="onInput" />
    <transition name="search__list">
      <InfinityList
        v-if="isListVisible"
        :async-func="onEndReached"
        :loading="loading"
        :items="contacts"
        :loaderTrigger="loaderTrigger"
        class="search__list"
      >
        <template #item="{ item }">
          <ContactItem
            :contact="item"
            @click="onContactClick(item)"
            @remove="onRemoveClick"
          />
        </template>
        <template #nothing>
          <ContactsSearchNotFound />
        </template>
      </InfinityList>
    </transition>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import { throttle } from "throttle-debounce";

import ContactsSearchModule from "@/store/modules/contactsSearch";

import ContactsSearchInput from "./ContactsSearchInput.vue";
import ContactsSearchNotFound from "./ContactsSearchNotFound.vue";
import ContactItem from "./ContactItem.vue";
import InfinityList from "./InfinityList.vue";

export default Vue.extend({
  components: {
    ContactsSearchInput,
    ContactsSearchNotFound,
    ContactItem,
    InfinityList,
  },
  data: () => ({
    isListVisible: false,
    loaderTrigger: 0,
  }),
  methods: {
    async onInput(value: string) {
      this.isListVisible = value.length > 0;

      if (value.length < 3) return;

      this.loaderTrigger = new Date().getTime();

      await this.getContacts(value);
    },
    getContacts: throttle(
      2000,
      async (value: string) => await ContactsSearchModule.ReloadContacts(value),
      { noTrailing: false, debounceMode: true }
    ),
    async onEndReached() {
      let isEndReached = false;
      await ContactsSearchModule.NextPage()
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
    onContactClick(contact: Contact) {
      this.$emit("contact-click", contact);
    },
    onRemoveClick(id: string) {
      this.$emit("contact-remove", id);
    },
  },
  computed: {
    loading(): boolean {
      return ContactsSearchModule.loading;
    },
    contacts(): Contact[] {
      return ContactsSearchModule.items;
    },
  },
});
</script>
