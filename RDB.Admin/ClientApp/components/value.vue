<template>
  <v-list-tile class="value-item"
    :ripple="false"
    @click="editing = true">
    <v-list-tile-avatar>
      <v-icon>label</v-icon>
    </v-list-tile-avatar>
    <v-list-tile-title>
      <v-text-field v-if="editing"
        class="pr-4 value-item-edit"
        autofocus
        :value="value.text"
        @keyup.enter="doneEdit"
        @keyup.esc="cancelEdit"
        @blur="doneEdit"
      ></v-text-field>
      <template v-else>{{value.text}}</template>
    </v-list-tile-title>
    <v-list-tile-sub-title>ID: {{value.id}}</v-list-tile-sub-title>
    <v-list-tile-action class="value-item-actions">
      <v-btn icon @click.stop="deleteValue(value.id)">
        <v-icon>delete</v-icon>
      </v-btn>
    </v-list-tile-action>
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
      editing: false,
      deleting: false
    }
  },
  methods: {
    ...mapActions('values', [
      'updateText',
      'deleteValue'
    ]),
    async doneEdit(e) {
      if (this.deleting) {
        return;
      }

      let text = e.target.value.trim()
      let { value } = this

      if (!text) {
        this.deleting = true;
        await this.deleteValue(value.id)
        this.deleting = false;
      } else if (this.editing && value.text != text) {
        await this.updateText({ value, text })
      }
      this.editing = false
    },
    cancelEdit(e) {
      e.target.value = this.value.text
      this.editing = false
    }
  }
}
</script>

<style lang="stylus">
  // disable list item hover
  .value-item a.list__tile:hover {
    background none
  }

  // align edit input with view text
  .value-item-edit
    padding-bottom 0 !important
    padding-top 20px !important

  // hover-visible item actions
  .value-item-actions
    visibility hidden

    *
      transition none !important

    .value-item:hover &
      visibility visible
</style>
