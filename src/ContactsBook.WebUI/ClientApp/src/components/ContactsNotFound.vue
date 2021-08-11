<template>
  <div class="contacts--none">
    <NotFoundIcon class="contacts--none__icon" />
    <h3 class="contacts--none__title">Oops, we have nothing to show you!</h3>
    <p class="contacts--none__text">
      It seems like you haven't created no of contacts. Let's try!
    </p>
  </div>
</template>

<script lang="ts">
import Vue from "vue";

import NotFoundIcon from "../assets/not_found_icon.svg";

export default Vue.extend({
  components: {
    NotFoundIcon
  }
});
</script>
