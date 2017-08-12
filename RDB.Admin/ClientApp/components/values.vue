<template>
  <v-layout>
    <v-flex xs12 md9>
      <v-card>
        <v-card-title class="green white--text">
          <div class="headline">Values</div>
        </v-card-title>
        <v-card-text>
          <p>This component fetches values from RDB api.</p>
          <v-subheader>GET /api/values</v-subheader>
          <p v-if="!values"><em>Loading...</em></p>
          <v-list v-else>
            <value
              v-for="value in values"
              :key="value.id"
              :value="value"
            ></value>
            <li>
              <v-text-field
                label="Add value"
                @keyup.enter="onAddValue"
              ></v-text-field>
            </li>
          </v-list>
        </v-card-text>
      </v-card>
    </v-flex>
  </v-layout>
</template>

<script>
import { mapState, mapActions } from 'vuex'
import Value from './value.vue'

export default {
  components: {
    Value
  },
  data() {
    return {}
  },
  created() {
    this.loadValues();
  },
  computed: {
    ...mapState('values', [
      'values'
    ])
  },
  methods: {
    ...mapActions('values', [
      'loadValues',
      'addValue'
    ]),
    async onAddValue(e) {
      let text = e.target.value
      if (text.trim()) {
        await this.addValue(text)
      }
      e.target.value = ''
    }
  }
}
</script>
