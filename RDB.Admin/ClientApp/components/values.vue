<template>
  <v-layout>
    <v-flex xs12 lg9>
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
            <li class="pl-3">
              <v-text-field
                label="Add value"
                v-model="newValue"
                @keyup.enter="addValueIfNotEmpty"
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
    return {
      newValue: ''
    }
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
    async addValueIfNotEmpty(e) {
      if (this.newValue.trim()) {
        await this.addValue(this.newValue)
      }
      this.newValue = ''
    }
  }
}
</script>
