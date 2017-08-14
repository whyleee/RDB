<template>
  <v-layout>
    <v-flex xs12 lg9>
      <v-card>
        <v-card-title class="green white--text">
          <div class="headline">Counter</div>
        </v-card-title>
        <v-card-text>
          <p>This is a simple example of a Vue.js component.</p>
          <p>Current count: <strong>{{count}}</strong></p>
        </v-card-text>
        <v-card-actions>
          <v-btn @click="increment">Increment</v-btn>
        </v-card-actions>
      </v-card>
    </v-flex>
  </v-layout>
</template>

<script>
import { mapState, mapMutations } from 'vuex'

export default {
  computed: {
    ...mapState('counter', [
      'count'
    ])
  },
  methods: {
    ...mapMutations('counter', [
      'increment'
    ])
  }
}
</script>
