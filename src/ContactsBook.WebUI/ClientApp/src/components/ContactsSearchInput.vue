<template>
  <label class="search">
    <button
      class="search__execute_button"
      title="Search contact"
      @click="emitInputTrigger"
    >
      <SearchIcon />
    </button>
    <input
      type="text"
      v-model.trim="inputText"
      @keydown.enter="emitInputTrigger"
      @keydown.esc="onEscPress"
      placeholder="Search Contact"
      class="search__input"
    />
    <button
      v-if="isClearButtonVisible"
      class="search__clear_button"
      @click="onClick"
      title="Clear input"
    >
      <CloseIcon />
    </button>
  </label>
</template>

<script lang="ts">
import Vue from "vue";

import SearchIcon from "@/assets/search_icon.svg";
import CloseIcon from "@/assets/close_icon.svg";

export default Vue.extend({
  components: {
    SearchIcon,
    CloseIcon,
  },
  data: () => ({
    inputText: "",
  }),
  methods: {
    onClick() {
      this.inputText = "";
      this.$emit("input-trigger", this.inputText);
    },
    emitInputTrigger() {
      this.$emit("input-trigger", this.inputText);
    },
    onEscPress() {
      this.inputText = "";
      this.$emit("input-trigger", this.inputText);
    },
  },
  computed: {
    isClearButtonVisible() {
      return this.inputText.length > 0;
    },
  },
});
</script>