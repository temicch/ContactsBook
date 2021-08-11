<template>
  <button class="person_add" title="Add contact" @click="onClick">
    <AddIcon />
  </button>
</template>

<script lang="ts">
import Vue from "vue";

import AddIcon from "@/assets/add_icon.svg";

export default Vue.extend({
  components: {
    AddIcon
  },
  methods: {
    onClick() {
      this.$emit("click");
    }
  }
});
</script>
