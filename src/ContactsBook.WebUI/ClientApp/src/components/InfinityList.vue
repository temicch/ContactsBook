<template>
  <div>
    <ul class="infinity_list">
      <BaseLoader v-if="loading" class="infinity_list__loader" />
      <template v-else-if="items.length > 0">
        <li
          class="infinity_list__item"
          v-for="item of this.items"
          :key="item.id"
        >
          <slot :item="item" name="item"></slot>
        </li>
        <InfiniteLoading @infinite="onInfinite" :identifier="loaderTrigger">
          <div slot="spinner"><base-loader /></div>
          <div slot="no-more"></div>
          <div slot="no-results"></div>
        </InfiniteLoading>
      </template>
      <slot v-else name="nothing">
        <div class="infinity_list--none">There is nothing to show</div>
      </slot>
    </ul>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import InfiniteLoading, { StateChanger } from "vue-infinite-loading";

import BaseLoader from "./BaseLoader.vue";

export default Vue.extend({
  components: {
    InfiniteLoading,
    BaseLoader
  },
  props: {
    items: {
      type: Array,
      required: true
    },
    loaderTrigger: {
      type: Number,
      required: false,
      default: +new Date()
    },
    loading: {
      type: Boolean,
      required: true,
      default: true
    },
    // Not a good idea, but 'vue-infinite-loading' has so ugly API, so...
    asyncFunc: {
      type: Function
    }
  },

  methods: {
    async onInfinite($state: StateChanger) {
      await this.asyncFunc()
        .then((res: Boolean) => {
          if (res == true) $state.complete();
          else $state.loaded();
        })
        .catch(() => $state.error());
    }
  }
});
</script>
