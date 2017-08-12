<template>
  <v-list-tile @click="editing = true">
    <v-list-tile-action>
      <v-icon>label</v-icon>
    </v-list-tile-action>
    <v-list-tile-title>
      <v-text-field v-if="editing"
        class="pr-4 value-edit-input"
        :autofocus="true"
        :value="value.text"
        @keyup.enter="doneEdit"
        @keyup.esc="cancelEdit"
        @blur="doneEdit"
      ></v-text-field>
      <template v-else>{{value.text}}</template>
    </v-list-tile-title>
    <v-list-tile-sub-title>ID: {{value.id}}</v-list-tile-sub-title>
  </v-list-tile>
</template>

<script>
import { mapActions } from 'vuex'

export default {
  props: [
    'value'
  ],
  data() {
    return {
      editing: false
    }
  },
  methods: {
    ...mapActions('values', [
      'updateText'
    ]),
    async doneEdit(e) {
      let text = e.target.value.trim()
      let { value } = this
      await this.updateText({ value, text })
      this.editing = false
    },
    cancelEdit(e) {
      e.target.value = this.value.text
      this.editing = false
    }
  }
}
</script>

<style lang="stylus" scoped>
  .value-edit-input
    padding-bottom 0 !important
    padding-top 20px !important
</style>
