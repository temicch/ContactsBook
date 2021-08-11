<template>
  <button
    type="button"
    @click.prevent.stop="onClick"
    title="Remove contact"
    class="contact__remove"
  >
    <CloseIcon />
  </button>
</template>

<script lang="ts">
import Vue from "vue";

import CloseIcon from "@/assets/close_icon.svg";

export default Vue.extend({
  components: {
    CloseIcon
  },
  methods: {
    onClick() {
      this.$emit("click");
    }
  }
});
</script>
